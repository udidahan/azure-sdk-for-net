﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#if NETCOREAPP3_1_OR_GREATER || SNIPPET
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.WebPubSub.Clients;
using Xunit;

namespace Azure.Messaging.WebPubSub.Client.Tests
{
    public class WebPubSubClientSample
    {
        public void ConstructWebPubSubClient()
        {
#region Snippet:WebPubSubClient_Construct
            var client = new WebPubSubClient(new Uri("<client-access-uri>"));
#endregion
        }

        public void ConstructWebPubSubClient2()
        {
#region Snippet:WebPubSubClient_Construct2
            var client = new WebPubSubClient(new WebPubSubClientCredential(token =>
            {
                // In common practice, you will have a negotiation server for generating token. Client should fetch token from it.
                return FetchClientAccessTokenFromServerAsync(token);
            }));
#endregion
        }

        public async ValueTask<Uri> FetchClientAccessTokenFromServerAsync(CancellationToken token)
        {
#region Snippet:WebPubSubClient_GenerateClientAccessUri
            var serviceClient = new WebPubSubServiceClient("<< Connection String >>", "hub");
            return await serviceClient.GetClientAccessUriAsync();
#endregion
        }

        public void WebPubSubClientSubscribeConnected(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_Subscribe_Connected
            client.Connected += eventArgs =>
            {
                Console.WriteLine($"Connection {eventArgs.ConnectionId} is connected");
                return Task.CompletedTask;
            };
#endregion
        }

        public void WebPubSubClientSubscribeDisconnected(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_Subscribe_Disconnected
            client.Disconnected += eventArgs =>
            {
                Console.WriteLine($"Connection is disconnected");
                return Task.CompletedTask;
            };
#endregion
        }

        public void WebPubSubClientSubscribeStopped(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_Subscribe_Stopped
            client.Stopped += eventArgs =>
            {
                Console.WriteLine($"Client is stopped");
                return Task.CompletedTask;
            };
#endregion
        }

        public void WebPubSubClientSubscribeServerMessage(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_Subscribe_ServerMessage
            client.ServerMessageReceived += eventArgs =>
            {
                Console.WriteLine($"Receive message: {eventArgs.Message.Data}");
                return Task.CompletedTask;
            };
#endregion
        }

        public void WebPubSubClientSubscribeGroupMessage(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_Subscribe_GroupMessage
            client.GroupMessageReceived += eventArgs =>
            {
                Console.WriteLine($"Receive group message from {eventArgs.Message.Group}: {eventArgs.Message.Data}");
                return Task.CompletedTask;
            };
#endregion
        }

        public void WebPubSubClientSubscribeRestoreFailed(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_Subscribe_RestoreFailed
            client.RejoinGroupFailed += eventArgs =>
            {
                Console.WriteLine($"Restore group failed");
                return Task.CompletedTask;
            };
#endregion
        }

        public async Task WebPubSubClientSendToGroup(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_SendToGroup
            // Send message to group "testGroup"
            await client.SendToGroupAsync("testGroup", BinaryData.FromString("hello world"), WebPubSubDataType.Text);
#endregion
        }

        public async Task WebPubSubClientSendEvent(WebPubSubClient client)
        {
#region Snippet:WebPubSubClient_SendEvent
            // Send custom event to server
            await client.SendEventAsync("testEvent", BinaryData.FromString("hello world"), WebPubSubDataType.Text);
#endregion
        }
    }
}
#endif
