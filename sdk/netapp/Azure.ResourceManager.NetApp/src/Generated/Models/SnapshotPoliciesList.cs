// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;
using Azure.ResourceManager.NetApp;

namespace Azure.ResourceManager.NetApp.Models
{
    /// <summary> List of Snapshot Policies. </summary>
    internal partial class SnapshotPoliciesList
    {
        /// <summary> Initializes a new instance of SnapshotPoliciesList. </summary>
        internal SnapshotPoliciesList()
        {
            Value = new ChangeTrackingList<SnapshotPolicyData>();
        }

        /// <summary> Initializes a new instance of SnapshotPoliciesList. </summary>
        /// <param name="value"> A list of snapshot policies. </param>
        internal SnapshotPoliciesList(IReadOnlyList<SnapshotPolicyData> value)
        {
            Value = value;
        }

        /// <summary> A list of snapshot policies. </summary>
        public IReadOnlyList<SnapshotPolicyData> Value { get; }
    }
}
