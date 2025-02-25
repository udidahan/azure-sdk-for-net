// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;

namespace Azure.ResourceManager.RecoveryServicesBackup
{
    /// <summary>
    /// A class representing a collection of <see cref="ProtectionIntentResource" /> and their operations.
    /// Each <see cref="ProtectionIntentResource" /> in the collection will belong to the same instance of <see cref="ResourceGroupResource" />.
    /// To get a <see cref="ProtectionIntentResourceCollection" /> instance call the GetProtectionIntentResources method from an instance of <see cref="ResourceGroupResource" />.
    /// </summary>
    public partial class ProtectionIntentResourceCollection : ArmCollection
    {
        private readonly ClientDiagnostics _protectionIntentResourceProtectionIntentClientDiagnostics;
        private readonly ProtectionIntentRestOperations _protectionIntentResourceProtectionIntentRestClient;

        /// <summary> Initializes a new instance of the <see cref="ProtectionIntentResourceCollection"/> class for mocking. </summary>
        protected ProtectionIntentResourceCollection()
        {
        }

        /// <summary> Initializes a new instance of the <see cref="ProtectionIntentResourceCollection"/> class. </summary>
        /// <param name="client"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the parent resource that is the target of operations. </param>
        internal ProtectionIntentResourceCollection(ArmClient client, ResourceIdentifier id) : base(client, id)
        {
            _protectionIntentResourceProtectionIntentClientDiagnostics = new ClientDiagnostics("Azure.ResourceManager.RecoveryServicesBackup", ProtectionIntentResource.ResourceType.Namespace, Diagnostics);
            TryGetApiVersion(ProtectionIntentResource.ResourceType, out string protectionIntentResourceProtectionIntentApiVersion);
            _protectionIntentResourceProtectionIntentRestClient = new ProtectionIntentRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, protectionIntentResourceProtectionIntentApiVersion);
#if DEBUG
			ValidateResourceId(Id);
#endif
        }

        internal static void ValidateResourceId(ResourceIdentifier id)
        {
            if (id.ResourceType != ResourceGroupResource.ResourceType)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid resource type {0} expected {1}", id.ResourceType, ResourceGroupResource.ResourceType), nameof(id));
        }

        /// <summary>
        /// Create Intent for Enabling backup of an item. This is a synchronous operation.
        /// Request Path: /Subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/backupProtectionIntent/{intentObjectName}
        /// Operation Id: ProtectionIntent_CreateOrUpdate
        /// </summary>
        /// <param name="waitUntil"> <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service; <see cref="WaitUntil.Started"/> if it should return after starting the operation. For more information on long-running operations, please see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/LongRunningOperations.md"> Azure.Core Long-Running Operation samples</see>. </param>
        /// <param name="vaultName"> The name of the recovery services vault. </param>
        /// <param name="fabricName"> Fabric name associated with the backup item. </param>
        /// <param name="intentObjectName"> Intent object name. </param>
        /// <param name="data"> resource backed up item. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="vaultName"/>, <paramref name="fabricName"/>, <paramref name="intentObjectName"/> or <paramref name="data"/> is null. </exception>
        public virtual async Task<ArmOperation<ProtectionIntentResource>> CreateOrUpdateAsync(WaitUntil waitUntil, string vaultName, string fabricName, string intentObjectName, ProtectionIntentResourceData data, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(vaultName, nameof(vaultName));
            Argument.AssertNotNullOrEmpty(fabricName, nameof(fabricName));
            Argument.AssertNotNullOrEmpty(intentObjectName, nameof(intentObjectName));
            Argument.AssertNotNull(data, nameof(data));

            using var scope = _protectionIntentResourceProtectionIntentClientDiagnostics.CreateScope("ProtectionIntentResourceCollection.CreateOrUpdate");
            scope.Start();
            try
            {
                var response = await _protectionIntentResourceProtectionIntentRestClient.CreateOrUpdateAsync(Id.SubscriptionId, Id.ResourceGroupName, vaultName, fabricName, intentObjectName, data, cancellationToken).ConfigureAwait(false);
                var operation = new RecoveryServicesBackupArmOperation<ProtectionIntentResource>(Response.FromValue(new ProtectionIntentResource(Client, response), response.GetRawResponse()));
                if (waitUntil == WaitUntil.Completed)
                    await operation.WaitForCompletionAsync(cancellationToken).ConfigureAwait(false);
                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Create Intent for Enabling backup of an item. This is a synchronous operation.
        /// Request Path: /Subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/backupProtectionIntent/{intentObjectName}
        /// Operation Id: ProtectionIntent_CreateOrUpdate
        /// </summary>
        /// <param name="waitUntil"> <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service; <see cref="WaitUntil.Started"/> if it should return after starting the operation. For more information on long-running operations, please see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/LongRunningOperations.md"> Azure.Core Long-Running Operation samples</see>. </param>
        /// <param name="vaultName"> The name of the recovery services vault. </param>
        /// <param name="fabricName"> Fabric name associated with the backup item. </param>
        /// <param name="intentObjectName"> Intent object name. </param>
        /// <param name="data"> resource backed up item. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="vaultName"/>, <paramref name="fabricName"/>, <paramref name="intentObjectName"/> or <paramref name="data"/> is null. </exception>
        public virtual ArmOperation<ProtectionIntentResource> CreateOrUpdate(WaitUntil waitUntil, string vaultName, string fabricName, string intentObjectName, ProtectionIntentResourceData data, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(vaultName, nameof(vaultName));
            Argument.AssertNotNullOrEmpty(fabricName, nameof(fabricName));
            Argument.AssertNotNullOrEmpty(intentObjectName, nameof(intentObjectName));
            Argument.AssertNotNull(data, nameof(data));

            using var scope = _protectionIntentResourceProtectionIntentClientDiagnostics.CreateScope("ProtectionIntentResourceCollection.CreateOrUpdate");
            scope.Start();
            try
            {
                var response = _protectionIntentResourceProtectionIntentRestClient.CreateOrUpdate(Id.SubscriptionId, Id.ResourceGroupName, vaultName, fabricName, intentObjectName, data, cancellationToken);
                var operation = new RecoveryServicesBackupArmOperation<ProtectionIntentResource>(Response.FromValue(new ProtectionIntentResource(Client, response), response.GetRawResponse()));
                if (waitUntil == WaitUntil.Completed)
                    operation.WaitForCompletion(cancellationToken);
                return operation;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Provides the details of the protection intent up item. This is an asynchronous operation. To know the status of the operation,
        /// call the GetItemOperationResult API.
        /// Request Path: /Subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/backupProtectionIntent/{intentObjectName}
        /// Operation Id: ProtectionIntent_Get
        /// </summary>
        /// <param name="vaultName"> The name of the recovery services vault. </param>
        /// <param name="fabricName"> Fabric name associated with the backed up item. </param>
        /// <param name="intentObjectName"> Backed up item name whose details are to be fetched. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is null. </exception>
        public virtual async Task<Response<ProtectionIntentResource>> GetAsync(string vaultName, string fabricName, string intentObjectName, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(vaultName, nameof(vaultName));
            Argument.AssertNotNullOrEmpty(fabricName, nameof(fabricName));
            Argument.AssertNotNullOrEmpty(intentObjectName, nameof(intentObjectName));

            using var scope = _protectionIntentResourceProtectionIntentClientDiagnostics.CreateScope("ProtectionIntentResourceCollection.Get");
            scope.Start();
            try
            {
                var response = await _protectionIntentResourceProtectionIntentRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, vaultName, fabricName, intentObjectName, cancellationToken).ConfigureAwait(false);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new ProtectionIntentResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Provides the details of the protection intent up item. This is an asynchronous operation. To know the status of the operation,
        /// call the GetItemOperationResult API.
        /// Request Path: /Subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/backupProtectionIntent/{intentObjectName}
        /// Operation Id: ProtectionIntent_Get
        /// </summary>
        /// <param name="vaultName"> The name of the recovery services vault. </param>
        /// <param name="fabricName"> Fabric name associated with the backed up item. </param>
        /// <param name="intentObjectName"> Backed up item name whose details are to be fetched. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is null. </exception>
        public virtual Response<ProtectionIntentResource> Get(string vaultName, string fabricName, string intentObjectName, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(vaultName, nameof(vaultName));
            Argument.AssertNotNullOrEmpty(fabricName, nameof(fabricName));
            Argument.AssertNotNullOrEmpty(intentObjectName, nameof(intentObjectName));

            using var scope = _protectionIntentResourceProtectionIntentClientDiagnostics.CreateScope("ProtectionIntentResourceCollection.Get");
            scope.Start();
            try
            {
                var response = _protectionIntentResourceProtectionIntentRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, vaultName, fabricName, intentObjectName, cancellationToken);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new ProtectionIntentResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the resource exists in azure.
        /// Request Path: /Subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/backupProtectionIntent/{intentObjectName}
        /// Operation Id: ProtectionIntent_Get
        /// </summary>
        /// <param name="vaultName"> The name of the recovery services vault. </param>
        /// <param name="fabricName"> Fabric name associated with the backed up item. </param>
        /// <param name="intentObjectName"> Backed up item name whose details are to be fetched. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is null. </exception>
        public virtual async Task<Response<bool>> ExistsAsync(string vaultName, string fabricName, string intentObjectName, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(vaultName, nameof(vaultName));
            Argument.AssertNotNullOrEmpty(fabricName, nameof(fabricName));
            Argument.AssertNotNullOrEmpty(intentObjectName, nameof(intentObjectName));

            using var scope = _protectionIntentResourceProtectionIntentClientDiagnostics.CreateScope("ProtectionIntentResourceCollection.Exists");
            scope.Start();
            try
            {
                var response = await _protectionIntentResourceProtectionIntentRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, vaultName, fabricName, intentObjectName, cancellationToken: cancellationToken).ConfigureAwait(false);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the resource exists in azure.
        /// Request Path: /Subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.RecoveryServices/vaults/{vaultName}/backupFabrics/{fabricName}/backupProtectionIntent/{intentObjectName}
        /// Operation Id: ProtectionIntent_Get
        /// </summary>
        /// <param name="vaultName"> The name of the recovery services vault. </param>
        /// <param name="fabricName"> Fabric name associated with the backed up item. </param>
        /// <param name="intentObjectName"> Backed up item name whose details are to be fetched. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is an empty string, and was expected to be non-empty. </exception>
        /// <exception cref="ArgumentNullException"> <paramref name="vaultName"/>, <paramref name="fabricName"/> or <paramref name="intentObjectName"/> is null. </exception>
        public virtual Response<bool> Exists(string vaultName, string fabricName, string intentObjectName, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNullOrEmpty(vaultName, nameof(vaultName));
            Argument.AssertNotNullOrEmpty(fabricName, nameof(fabricName));
            Argument.AssertNotNullOrEmpty(intentObjectName, nameof(intentObjectName));

            using var scope = _protectionIntentResourceProtectionIntentClientDiagnostics.CreateScope("ProtectionIntentResourceCollection.Exists");
            scope.Start();
            try
            {
                var response = _protectionIntentResourceProtectionIntentRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, vaultName, fabricName, intentObjectName, cancellationToken: cancellationToken);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
