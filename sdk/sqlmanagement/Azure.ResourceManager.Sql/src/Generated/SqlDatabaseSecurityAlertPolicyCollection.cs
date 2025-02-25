// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.ResourceManager;
using Azure.ResourceManager.Sql.Models;

namespace Azure.ResourceManager.Sql
{
    /// <summary>
    /// A class representing a collection of <see cref="SqlDatabaseSecurityAlertPolicyResource" /> and their operations.
    /// Each <see cref="SqlDatabaseSecurityAlertPolicyResource" /> in the collection will belong to the same instance of <see cref="SqlDatabaseResource" />.
    /// To get a <see cref="SqlDatabaseSecurityAlertPolicyCollection" /> instance call the GetSqlDatabaseSecurityAlertPolicies method from an instance of <see cref="SqlDatabaseResource" />.
    /// </summary>
    public partial class SqlDatabaseSecurityAlertPolicyCollection : ArmCollection, IEnumerable<SqlDatabaseSecurityAlertPolicyResource>, IAsyncEnumerable<SqlDatabaseSecurityAlertPolicyResource>
    {
        private readonly ClientDiagnostics _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics;
        private readonly DatabaseSecurityAlertPoliciesRestOperations _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient;

        /// <summary> Initializes a new instance of the <see cref="SqlDatabaseSecurityAlertPolicyCollection"/> class for mocking. </summary>
        protected SqlDatabaseSecurityAlertPolicyCollection()
        {
        }

        /// <summary> Initializes a new instance of the <see cref="SqlDatabaseSecurityAlertPolicyCollection"/> class. </summary>
        /// <param name="client"> The client parameters to use in these operations. </param>
        /// <param name="id"> The identifier of the parent resource that is the target of operations. </param>
        internal SqlDatabaseSecurityAlertPolicyCollection(ArmClient client, ResourceIdentifier id) : base(client, id)
        {
            _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics = new ClientDiagnostics("Azure.ResourceManager.Sql", SqlDatabaseSecurityAlertPolicyResource.ResourceType.Namespace, Diagnostics);
            TryGetApiVersion(SqlDatabaseSecurityAlertPolicyResource.ResourceType, out string sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesApiVersion);
            _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient = new DatabaseSecurityAlertPoliciesRestOperations(Pipeline, Diagnostics.ApplicationId, Endpoint, sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesApiVersion);
#if DEBUG
			ValidateResourceId(Id);
#endif
        }

        internal static void ValidateResourceId(ResourceIdentifier id)
        {
            if (id.ResourceType != SqlDatabaseResource.ResourceType)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Invalid resource type {0} expected {1}", id.ResourceType, SqlDatabaseResource.ResourceType), nameof(id));
        }

        /// <summary>
        /// Creates or updates a database&apos;s security alert policy.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies/{securityAlertPolicyName}
        /// Operation Id: DatabaseSecurityAlertPolicies_CreateOrUpdate
        /// </summary>
        /// <param name="waitUntil"> <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service; <see cref="WaitUntil.Started"/> if it should return after starting the operation. For more information on long-running operations, please see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/LongRunningOperations.md"> Azure.Core Long-Running Operation samples</see>. </param>
        /// <param name="securityAlertPolicyName"> The name of the security alert policy. </param>
        /// <param name="data"> The database security alert policy. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="data"/> is null. </exception>
        public virtual async Task<ArmOperation<SqlDatabaseSecurityAlertPolicyResource>> CreateOrUpdateAsync(WaitUntil waitUntil, SqlSecurityAlertPolicyName securityAlertPolicyName, SqlDatabaseSecurityAlertPolicyData data, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(data, nameof(data));

            using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.CreateOrUpdate");
            scope.Start();
            try
            {
                var response = await _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.CreateOrUpdateAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, securityAlertPolicyName, data, cancellationToken).ConfigureAwait(false);
                var operation = new SqlArmOperation<SqlDatabaseSecurityAlertPolicyResource>(Response.FromValue(new SqlDatabaseSecurityAlertPolicyResource(Client, response), response.GetRawResponse()));
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
        /// Creates or updates a database&apos;s security alert policy.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies/{securityAlertPolicyName}
        /// Operation Id: DatabaseSecurityAlertPolicies_CreateOrUpdate
        /// </summary>
        /// <param name="waitUntil"> <see cref="WaitUntil.Completed"/> if the method should wait to return until the long-running operation has completed on the service; <see cref="WaitUntil.Started"/> if it should return after starting the operation. For more information on long-running operations, please see <see href="https://github.com/Azure/azure-sdk-for-net/blob/main/sdk/core/Azure.Core/samples/LongRunningOperations.md"> Azure.Core Long-Running Operation samples</see>. </param>
        /// <param name="securityAlertPolicyName"> The name of the security alert policy. </param>
        /// <param name="data"> The database security alert policy. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="data"/> is null. </exception>
        public virtual ArmOperation<SqlDatabaseSecurityAlertPolicyResource> CreateOrUpdate(WaitUntil waitUntil, SqlSecurityAlertPolicyName securityAlertPolicyName, SqlDatabaseSecurityAlertPolicyData data, CancellationToken cancellationToken = default)
        {
            Argument.AssertNotNull(data, nameof(data));

            using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.CreateOrUpdate");
            scope.Start();
            try
            {
                var response = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.CreateOrUpdate(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, securityAlertPolicyName, data, cancellationToken);
                var operation = new SqlArmOperation<SqlDatabaseSecurityAlertPolicyResource>(Response.FromValue(new SqlDatabaseSecurityAlertPolicyResource(Client, response), response.GetRawResponse()));
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
        /// Gets a database&apos;s security alert policy.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies/{securityAlertPolicyName}
        /// Operation Id: DatabaseSecurityAlertPolicies_Get
        /// </summary>
        /// <param name="securityAlertPolicyName"> The name of the security alert policy. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<SqlDatabaseSecurityAlertPolicyResource>> GetAsync(SqlSecurityAlertPolicyName securityAlertPolicyName, CancellationToken cancellationToken = default)
        {
            using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.Get");
            scope.Start();
            try
            {
                var response = await _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, securityAlertPolicyName, cancellationToken).ConfigureAwait(false);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new SqlDatabaseSecurityAlertPolicyResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets a database&apos;s security alert policy.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies/{securityAlertPolicyName}
        /// Operation Id: DatabaseSecurityAlertPolicies_Get
        /// </summary>
        /// <param name="securityAlertPolicyName"> The name of the security alert policy. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<SqlDatabaseSecurityAlertPolicyResource> Get(SqlSecurityAlertPolicyName securityAlertPolicyName, CancellationToken cancellationToken = default)
        {
            using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.Get");
            scope.Start();
            try
            {
                var response = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, securityAlertPolicyName, cancellationToken);
                if (response.Value == null)
                    throw new RequestFailedException(response.GetRawResponse());
                return Response.FromValue(new SqlDatabaseSecurityAlertPolicyResource(Client, response.Value), response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        /// <summary>
        /// Gets a list of database&apos;s security alert policies.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies
        /// Operation Id: DatabaseSecurityAlertPolicies_ListByDatabase
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> An async collection of <see cref="SqlDatabaseSecurityAlertPolicyResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual AsyncPageable<SqlDatabaseSecurityAlertPolicyResource> GetAllAsync(CancellationToken cancellationToken = default)
        {
            async Task<Page<SqlDatabaseSecurityAlertPolicyResource>> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.GetAll");
                scope.Start();
                try
                {
                    var response = await _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.ListByDatabaseAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new SqlDatabaseSecurityAlertPolicyResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            async Task<Page<SqlDatabaseSecurityAlertPolicyResource>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.GetAll");
                scope.Start();
                try
                {
                    var response = await _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.ListByDatabaseNextPageAsync(nextLink, Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, cancellationToken: cancellationToken).ConfigureAwait(false);
                    return Page.FromValues(response.Value.Value.Select(value => new SqlDatabaseSecurityAlertPolicyResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Gets a list of database&apos;s security alert policies.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies
        /// Operation Id: DatabaseSecurityAlertPolicies_ListByDatabase
        /// </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        /// <returns> A collection of <see cref="SqlDatabaseSecurityAlertPolicyResource" /> that may take multiple service requests to iterate over. </returns>
        public virtual Pageable<SqlDatabaseSecurityAlertPolicyResource> GetAll(CancellationToken cancellationToken = default)
        {
            Page<SqlDatabaseSecurityAlertPolicyResource> FirstPageFunc(int? pageSizeHint)
            {
                using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.GetAll");
                scope.Start();
                try
                {
                    var response = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.ListByDatabase(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new SqlDatabaseSecurityAlertPolicyResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            Page<SqlDatabaseSecurityAlertPolicyResource> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.GetAll");
                scope.Start();
                try
                {
                    var response = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.ListByDatabaseNextPage(nextLink, Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, cancellationToken: cancellationToken);
                    return Page.FromValues(response.Value.Value.Select(value => new SqlDatabaseSecurityAlertPolicyResource(Client, value)), response.Value.NextLink, response.GetRawResponse());
                }
                catch (Exception e)
                {
                    scope.Failed(e);
                    throw;
                }
            }
            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }

        /// <summary>
        /// Checks to see if the resource exists in azure.
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies/{securityAlertPolicyName}
        /// Operation Id: DatabaseSecurityAlertPolicies_Get
        /// </summary>
        /// <param name="securityAlertPolicyName"> The name of the security alert policy. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<bool>> ExistsAsync(SqlSecurityAlertPolicyName securityAlertPolicyName, CancellationToken cancellationToken = default)
        {
            using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.Exists");
            scope.Start();
            try
            {
                var response = await _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.GetAsync(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, securityAlertPolicyName, cancellationToken: cancellationToken).ConfigureAwait(false);
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
        /// Request Path: /subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Sql/servers/{serverName}/databases/{databaseName}/securityAlertPolicies/{securityAlertPolicyName}
        /// Operation Id: DatabaseSecurityAlertPolicies_Get
        /// </summary>
        /// <param name="securityAlertPolicyName"> The name of the security alert policy. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<bool> Exists(SqlSecurityAlertPolicyName securityAlertPolicyName, CancellationToken cancellationToken = default)
        {
            using var scope = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesClientDiagnostics.CreateScope("SqlDatabaseSecurityAlertPolicyCollection.Exists");
            scope.Start();
            try
            {
                var response = _sqlDatabaseSecurityAlertPolicyDatabaseSecurityAlertPoliciesRestClient.Get(Id.SubscriptionId, Id.ResourceGroupName, Id.Parent.Name, Id.Name, securityAlertPolicyName, cancellationToken: cancellationToken);
                return Response.FromValue(response.Value != null, response.GetRawResponse());
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        IEnumerator<SqlDatabaseSecurityAlertPolicyResource> IEnumerable<SqlDatabaseSecurityAlertPolicyResource>.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAll().GetEnumerator();
        }

        IAsyncEnumerator<SqlDatabaseSecurityAlertPolicyResource> IAsyncEnumerable<SqlDatabaseSecurityAlertPolicyResource>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            return GetAllAsync(cancellationToken: cancellationToken).GetAsyncEnumerator(cancellationToken);
        }
    }
}
