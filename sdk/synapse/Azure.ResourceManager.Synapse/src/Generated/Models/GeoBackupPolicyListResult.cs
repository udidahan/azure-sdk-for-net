// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;
using Azure.ResourceManager.Synapse;

namespace Azure.ResourceManager.Synapse.Models
{
    /// <summary> The response to a list geo backup policies request. </summary>
    internal partial class GeoBackupPolicyListResult
    {
        /// <summary> Initializes a new instance of GeoBackupPolicyListResult. </summary>
        internal GeoBackupPolicyListResult()
        {
            Value = new ChangeTrackingList<GeoBackupPolicyData>();
        }

        /// <summary> Initializes a new instance of GeoBackupPolicyListResult. </summary>
        /// <param name="value"> The list of geo backup policies. </param>
        internal GeoBackupPolicyListResult(IReadOnlyList<GeoBackupPolicyData> value)
        {
            Value = value;
        }

        /// <summary> The list of geo backup policies. </summary>
        public IReadOnlyList<GeoBackupPolicyData> Value { get; }
    }
}
