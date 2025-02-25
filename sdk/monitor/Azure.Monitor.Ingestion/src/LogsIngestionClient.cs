// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Monitor.Ingestion
{
    /// <summary> The IngestionUsingDataCollectionRules service client. </summary>
    [CodeGenClient("IngestionUsingDataCollectionRulesClient")]
    [CodeGenSuppress("Upload", typeof(string), typeof(string), typeof(RequestContent), typeof(string), typeof(RequestContext))]
    [CodeGenSuppress("UploadAsync", typeof(string), typeof(string), typeof(RequestContent), typeof(string), typeof(RequestContext))]
    public partial class LogsIngestionClient
    {
        /// <summary> Initializes a new instance of LogsIngestionClient for mocking. </summary>
        protected LogsIngestionClient()
        {
        }

        // The size we use to determine whether to upload as a single PUT BLOB
        // request or stage as multiple blocks.
        // 1 Mb in byte format
        internal static int SingleUploadThreshold = 1000000;

        // For test purposes only
        // If Compression wants to be turned off (hard to generate 1 Mb data gzipped) set Compression to null
        internal static string Compression = "gzip";

        // If no concurrency count is provided for a parallel upload, default to 5 workers.
        private const int DefaultParallelWorkerCount = 5;

        internal readonly struct BatchedLogs <T>
        {
            public BatchedLogs(List<T> logsList, BinaryData logsData)
            {
                LogsList = logsList;
                LogsData = logsData;
            }

            public List<T> LogsList { get; }
            public BinaryData LogsData { get; }
        }

        internal HttpMessage CreateUploadRequest(string ruleId, string streamName, RequestContent content, string contentEncoding = "gzip", RequestContext context = null)
        {
            var message = _pipeline.CreateMessage(context, ResponseClassifier204);
            var request = message.Request;
            request.Method = RequestMethod.Post;
            var uri = new RawRequestUriBuilder();
            uri.Reset(_endpoint);
            uri.AppendPath("/dataCollectionRules/", false);
            uri.AppendPath(ruleId, true);
            uri.AppendPath("/streams/", false);
            uri.AppendPath(streamName, true);
            uri.AppendQuery("api-version", _apiVersion, true);
            request.Uri = uri;
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Content-Type", "application/json");
            if (contentEncoding != null)
            {
                request.Headers.Add("Content-Encoding", contentEncoding);
            }
            if (contentEncoding == "gzip")
            {
                GZipUtf8JsonRequestContent gzContent = new(content);
                request.Content = gzContent;
            }
            else
            {
                request.Content = content;
            }
            return message;
        }

        /// <summary>
        /// Hidden method for batching data - serializing into arrays of JSON no more than SingleUploadThreshold each
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="logEntries"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        internal static IEnumerable<BatchedLogs<T>> Batch<T>(IEnumerable<T> logEntries, UploadLogsOptions options = null)
        {
            //TODO: use Array pool instead
            MemoryStream stream = new MemoryStream(SingleUploadThreshold);
            WriteMemory(stream, BinaryData.FromString("[").ToMemory());
            int entryCount = 0;
            List<T> currentLogList = new List<T>();
            foreach (var log in logEntries)
            {
                BinaryData entry;
                // If log is already BinaryData, no need to serialize it
                if (log is BinaryData d)
                    entry = d;
                // If log is not BinaryData, serialize it. Default Serializer is System.Text.Json
                else if (options == null || options.Serializer == null)
                    entry = BinaryData.FromObjectAsJson(log);
                // Otherwise use Serializer specified in options
                else
                    entry = options.Serializer.Serialize(log);

                var memory = entry.ToMemory();
                if (memory.Length > SingleUploadThreshold) // if single log is > 1 Mb send to be gzipped by itself
                {
                    MemoryStream tempStream = new MemoryStream(); // create tempStream for individual log
                    WriteMemory(tempStream, BinaryData.FromString("[").ToMemory());
                    WriteMemory(tempStream, memory);
                    WriteMemory(tempStream, BinaryData.FromString("]").ToMemory());
                    tempStream.Position = 0;
                    yield return new BatchedLogs<T>(new List<T>{log}, BinaryData.FromStream(tempStream));
                }
                else if ((stream.Length + memory.Length + 1) >= SingleUploadThreshold) // if adding this entry makes stream > 1 Mb send current stream now
                {
                    WriteMemory(stream, BinaryData.FromString("]").ToMemory());
                    stream.Position = 0; // set Position to 0 to return everything from beginning of stream
                    yield return new BatchedLogs<T>(currentLogList, BinaryData.FromStream(stream));

                    // reset stream and currentLogList
                    stream = new MemoryStream(SingleUploadThreshold); // reset stream
                    currentLogList = new List<T>(); // reset log list
                    WriteMemory(stream, memory); // add log to memory and currentLogList
                    currentLogList.Add(log);
                }
                else
                {
                    WriteMemory(stream, memory);
                    if ((entryCount + 1) == logEntries.Count())
                    {
                        // reached end of logs and we haven't returned yet
                        WriteMemory(stream, BinaryData.FromString("]").ToMemory());
                        stream.Position = 0;
                        currentLogList.Add(log);
                        yield return new BatchedLogs<T>(currentLogList, BinaryData.FromStream(stream));
                    }
                    else
                    {
                        WriteMemory(stream, BinaryData.FromString(",").ToMemory());
                        currentLogList.Add(log);
                    }
                }
                entryCount++;
            }
        }

        private static void WriteMemory(MemoryStream stream, ReadOnlyMemory<byte> memory)
        {
            stream.Write(memory.ToArray(), 0, memory.Length); //TODO: fix ToArray
        }

        /// <summary> Ingestion API used to directly ingest data using Data Collection Rules. </summary>
        /// <param name="ruleId"> The immutable Id of the Data Collection Rule resource. </param>
        /// <param name="streamName"> The streamDeclaration name as defined in the Data Collection Rule. </param>
        /// <param name="logs"> The content to send as the body of the request. Details of the request body schema are in the Remarks section below. </param>
        /// <param name="options"> The options model to configure the request to upload logs to Azure Monitor. </param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="ruleId"/>, <paramref name="streamName"/> or <paramref name="logs"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="ruleId"/> or <paramref name="streamName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="RequestFailedException"> Service returned a non-Success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        /// <example>
        /// This sample shows how to call Upload with required parameters and request content.
        /// <code><![CDATA[
        /// var credential = new DefaultAzureCredential();
        /// var endpoint = new Uri("<https://my-account-name.azure.com>");
        /// var client = new LogsIngestionClient(endpoint, credential);
        ///
        /// var data = new[] {
        ///     new {}
        /// };
        ///
        /// Response response = client.Upload("<ruleId>", "<streamName>", data);
        /// Console.WriteLine(response.Status);
        /// ]]></code>
        /// </example>
        /// <remarks> See error response code and error response message for more detail. </remarks>
        public virtual Response Upload<T>(string ruleId, string streamName, IEnumerable<T> logs, UploadLogsOptions options = null, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(ruleId, nameof(ruleId));
            Argument.AssertNotNullOrEmpty(streamName, nameof(streamName));
            Argument.AssertNotNullOrEmpty(logs, nameof(logs));

            using var scope = ClientDiagnostics.CreateScope("LogsIngestionClient.Upload");

            Response response = null;
            List<Exception> exceptions = null;

            scope.Start();
            // Keep track of the number of failed logs across batches
            int logsFailed = 0;

            // Partition the stream into individual blocks
            foreach (BatchedLogs<T> batch in Batch(logs, options))
            {
                try
                {
                    // Because we are uploading in sequence, wait for each batch to upload before starting the next batch
                    response = UploadBatchListSyncOrAsync(
                        batch,
                        ruleId,
                        streamName,
                        async: false,
                        cancellationToken).EnsureCompleted();

                    if (response.Status != 204)
                    {
                        throw new RequestFailedException(response);
                    }
                }
                catch (Exception ex)
                {
                    logsFailed += batch.LogsList.Count;
                    // If we have an error, add Exception from response into exceptions list without throwing
                    AddException(
                        ref exceptions,
                        ex);
                }
            }
            if (exceptions?.Count > 0)
            {
                var ex = new AggregateException($"{logsFailed} out of the {logs.Count()} logs failed to upload. Please check the InnerExceptions for more details.", exceptions);
                scope.Failed(ex);
                throw ex;
            }

            // If no exceptions return response
            return response; //204 - response of last batch with header
        }

        /// <summary> Ingestion API used to directly ingest data using Data Collection Rules. </summary>
        /// <param name="ruleId"> The immutable Id of the Data Collection Rule resource. </param>
        /// <param name="streamName"> The streamDeclaration name as defined in the Data Collection Rule. </param>
        /// <param name="logs"> The content to send as the body of the request. Details of the request body schema are in the Remarks section below. </param>
        /// <param name="options">  The options model to configure the request to upload logs to Azure Monitor. </param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException"> <paramref name="ruleId"/>, <paramref name="streamName"/> or <paramref name="logs"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="ruleId"/> or <paramref name="streamName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="RequestFailedException"> Service returned a non-Success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        /// <example>
        /// This sample shows how to call Upload with required parameters and request content.
        /// <code><![CDATA[
        /// var credential = new DefaultAzureCredential();
        /// var endpoint = new Uri("<https://my-account-name.azure.com>");
        /// var client = new LogsIngestionClient(endpoint, credential);
        ///
        /// var data = new[] {
        ///     new {}
        /// };
        ///
        /// Response response = client.Upload("<ruleId>", "<streamName>", data);
        /// Console.WriteLine(response.Status);
        /// ]]></code>
        /// </example>
        /// <remarks> See error response code and error response message for more detail. </remarks>
        public virtual async Task<Response> UploadAsync<T>(string ruleId, string streamName, IEnumerable<T> logs, UploadLogsOptions options = null, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(ruleId, nameof(ruleId));
            Argument.AssertNotNullOrEmpty(streamName, nameof(streamName));
            Argument.AssertNotNullOrEmpty(logs, nameof(logs));

            // Calculate the number of threads to use.
            // If there are 0 workers or an UploadLogsOptions object was not provided, method will run with 5 workers. Otherwise will run in parallel with number of workers given.
            int _maxWorkerCount = (options == null || options.MaxConcurrency <= 0) ? DefaultParallelWorkerCount : options.MaxConcurrency;
            using var scope = ClientDiagnostics.CreateScope("LogsIngestionClient.Upload");

            List<Exception> exceptions = null;
            scope.Start();

            // A list of tasks that are currently executing which will
            // always be smaller than or equal to MaxWorkerCount
            var runningTasks = new List<(Task<Response> CurrentTask, int LogsCount)>();
            // Keep track of the number of failed logs across batches
            int logsFailed = 0;

            // Partition the stream into individual blocks
            foreach (BatchedLogs<T> batch in Batch(logs, options))
            {
                try
                {
                    // Start staging the next batch (but don't await the Task!)
                    Task<Response> task = UploadBatchListSyncOrAsync(
                        batch,
                        ruleId,
                        streamName,
                        async: true,
                        cancellationToken);

                    // Add the block to our task and commit lists
                    runningTasks.Add((task, batch.LogsList.Count));

                    // If we run out of workers
                    if (runningTasks.Count >= _maxWorkerCount)
                    {
                        // Wait for at least one of them to finish
                        await Task.WhenAny(runningTasks.Select(_ => _.CurrentTask)).ConfigureAwait(false);
                        // Clear any completed blocks from the task list
                        for (int i = 0; i < runningTasks.Count; i++)
                        {
                            Task<Response> runningTask = runningTasks[i].CurrentTask;
                            if (!runningTask.IsCompleted)
                            {
                                continue;
                            }
                            // Check completed task for Exception/RequestFailedException and increase logsFailed count
                            ProcessCompletedTask(runningTasks[i], ref exceptions, ref logsFailed);
                            // Remove completed task from task list
                            runningTasks.RemoveAt(i);
                            i--;
                        }
                    }

                    // Wait for all the remaining blocks to finish uploading
                    await Task.WhenAll(runningTasks.Select(_ => _.CurrentTask)).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    // We do not want to log exceptions here as we will loop through all the tasks later
                }
            }

            // At this point, all tasks have completed. Examine tasks to see if they have exceptions. If Status code != 204, add RequestFailedException to list of exceptions. Increment logsFailed accordingly
            foreach (var task in runningTasks)
            {
                ProcessCompletedTask(task, ref exceptions, ref logsFailed);
            }
            if (exceptions?.Count > 0)
            {
                var ex = new AggregateException($"{logsFailed} out of the {logs.Count()} logs failed to upload. Please check the InnerExceptions for more details.", exceptions);
                scope.Failed(ex);
                throw ex;
            }

            // If no exceptions return response
            return runningTasks.Select(_ => _.CurrentTask).Last().Result; //204 - response of last batch with header
        }

        private static void ProcessCompletedTask((Task<Response> CurrentTask, int LogsCount) runningTask, ref List<Exception> exceptions, ref int logsFailed)
        {
            // If current task has an exception, log the exception and add number of logs in this task to failed logs
            if (runningTask.CurrentTask.Exception != null)
            {
                AddException(
                    ref exceptions,
                    runningTask.CurrentTask.Exception);
                logsFailed += runningTask.LogsCount;
            }
            // If current task returned a response that was not a success, log the exception and add number of logs in this task to failed logs
            else if (runningTask.CurrentTask.Result.Status != 204)
            {
                AddException(
                    ref exceptions,
                    new RequestFailedException(runningTask.CurrentTask.Result));
                logsFailed += runningTask.LogsCount;
            }
        }

        private async Task<Response> UploadBatchListSyncOrAsync<T>(BatchedLogs<T> batch, string ruleId, string streamName, bool async, CancellationToken cancellationToken)
        {
            using HttpMessage message = CreateUploadRequest(ruleId, streamName, batch.LogsData, Compression, null);

            if (async)
            {
                await _pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                _pipeline.Send(message, cancellationToken);
            }

            return message.Response;
        }

        private static void AddException(ref List<Exception> exceptions, Exception ex)
        {
            exceptions ??= new List<Exception>();
            exceptions.Add(ex);
        }

        /// <summary> Ingestion API used to directly ingest data using Data Collection Rules. </summary>
        /// <param name="ruleId"> The immutable Id of the Data Collection Rule resource. </param>
        /// <param name="streamName"> The streamDeclaration name as defined in the Data Collection Rule. </param>
        /// <param name="content"> The content to send as the body of the request. Details of the request body schema are in the Remarks section below. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="ruleId"/>, <paramref name="streamName"/> or <paramref name="content"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="ruleId"/> or <paramref name="streamName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="RequestFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        /// <example>
        /// This sample shows how to call UploadAsync with required parameters and request content.
        /// <code><![CDATA[
        /// var credential = new DefaultAzureCredential();
        /// var endpoint = new Uri("<https://my-service.azure.com>");
        /// var client = new LogsIngestionClient(endpoint, credential);
        ///
        /// var data = new[] {
        ///     new {}
        /// };
        ///
        /// Response response = await client.UploadAsync("<ruleId>", "<streamName>", RequestContent.Create(data));
        /// Console.WriteLine(response.Status);
        /// ]]></code>
        /// This sample shows how to call UploadAsync with all parameters and request content.
        /// <code><![CDATA[
        /// var credential = new DefaultAzureCredential();
        /// var endpoint = new Uri("<https://my-service.azure.com>");
        /// var client = new LogsIngestionClient(endpoint, credential);
        ///
        /// var data = new[] {
        ///     new {}
        /// };
        ///
        /// Response response = await client.UploadAsync("<ruleId>", "<streamName>", RequestContent.Create(data), <gzip>);
        /// Console.WriteLine(response.Status);
        /// ]]></code>
        /// </example>
        /// <remarks> See error response code and error response message for more detail. </remarks>
        public virtual async Task<Response> UploadAsync(string ruleId, string streamName, RequestContent content, RequestContext context = null)
        {
            return await UploadRequestContentAsync(ruleId, streamName, content, true, context).ConfigureAwait(false);
        }

        /// <summary> Ingestion API used to directly ingest data using Data Collection Rules. </summary>
        /// <param name="ruleId"> The immutable Id of the Data Collection Rule resource. </param>
        /// <param name="streamName"> The streamDeclaration name as defined in the Data Collection Rule. </param>
        /// <param name="content"> The content to send as the body of the request. Details of the request body schema are in the Remarks section below. </param>
        /// <param name="context"> The request context, which can override default behaviors of the client pipeline on a per-call basis. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="ruleId"/>, <paramref name="streamName"/> or <paramref name="content"/> is null. </exception>
        /// <exception cref="ArgumentException"> <paramref name="ruleId"/> or <paramref name="streamName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="RequestFailedException"> Service returned a non-success status code. </exception>
        /// <returns> The response returned from the service. </returns>
        /// <example>
        /// This sample shows how to call Upload with required parameters and request content.
        /// <code><![CDATA[
        /// var credential = new DefaultAzureCredential();
        /// var endpoint = new Uri("<https://my-service.azure.com>");
        /// var client = new LogsIngestionClient(endpoint, credential);
        ///
        /// var data = new[] {
        ///     new {}
        /// };
        ///
        /// Response response = client.Upload("<ruleId>", "<streamName>", RequestContent.Create(data));
        /// Console.WriteLine(response.Status);
        /// ]]></code>
        /// This sample shows how to call Upload with all parameters and request content.
        /// <code><![CDATA[
        /// var credential = new DefaultAzureCredential();
        /// var endpoint = new Uri("<https://my-service.azure.com>");
        /// var client = new LogsIngestionClient(endpoint, credential);
        ///
        /// var data = new[] {
        ///     new {}
        /// };
        ///
        /// Response response = client.Upload("<ruleId>", "<streamName>", RequestContent.Create(data), <gzip>);
        /// Console.WriteLine(response.Status);
        /// ]]></code>
        /// </example>
        /// <remarks> See error response code and error response message for more detail. </remarks>
        public virtual Response Upload(string ruleId, string streamName, RequestContent content, RequestContext context = null)
        {
            return UploadRequestContentAsync(ruleId, streamName, content, false, context).EnsureCompleted();
        }

        internal virtual async Task<Response> UploadRequestContentAsync(string ruleId, string streamName, RequestContent content, bool async, RequestContext context = null)
        {
            Argument.AssertNotNullOrEmpty(ruleId, nameof(ruleId));
            Argument.AssertNotNullOrEmpty(streamName, nameof(streamName));
            Argument.AssertNotNull(content, nameof(content));

            using var scope = ClientDiagnostics.CreateScope("LogsIngestionClient.Upload");
            scope.Start();
            try
            {
                using HttpMessage message = CreateUploadRequest(ruleId, streamName, content, "gzip", context);
                if (async)
                {
                    return await _pipeline.ProcessMessageAsync(message, context).ConfigureAwait(false);
                }
                else
                {
                    return _pipeline.ProcessMessage(message, context);
                }
            }
            catch (Exception ex)
            {
                scope.Failed(ex);
                throw;
            }
        }
    }
}
