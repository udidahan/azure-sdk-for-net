// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.HybridData.Models
{
    /// <summary> Schedule for the job run. </summary>
    public partial class HybridDataJobRunSchedule
    {
        /// <summary> Initializes a new instance of HybridDataJobRunSchedule. </summary>
        public HybridDataJobRunSchedule()
        {
            PolicyList = new ChangeTrackingList<string>();
        }

        /// <summary> Initializes a new instance of HybridDataJobRunSchedule. </summary>
        /// <param name="name"> Name of the schedule. </param>
        /// <param name="policyList"> A list of repetition intervals in ISO 8601 format. </param>
        internal HybridDataJobRunSchedule(string name, IList<string> policyList)
        {
            Name = name;
            PolicyList = policyList;
        }

        /// <summary> Name of the schedule. </summary>
        public string Name { get; set; }
        /// <summary> A list of repetition intervals in ISO 8601 format. </summary>
        public IList<string> PolicyList { get; }
    }
}
