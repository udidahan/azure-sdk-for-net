// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Collections.Generic;
using Azure.Core;

namespace Azure.ResourceManager.RecoveryServicesBackup.Models
{
    /// <summary> Azure storage specific job. </summary>
    public partial class AzureWorkloadJob : Job
    {
        /// <summary> Initializes a new instance of AzureWorkloadJob. </summary>
        public AzureWorkloadJob()
        {
            ActionsInfo = new ChangeTrackingList<JobSupportedAction>();
            ErrorDetails = new ChangeTrackingList<AzureWorkloadErrorInfo>();
            JobType = "AzureWorkloadJob";
        }

        /// <summary> Initializes a new instance of AzureWorkloadJob. </summary>
        /// <param name="entityFriendlyName"> Friendly name of the entity on which the current job is executing. </param>
        /// <param name="backupManagementType"> Backup management type to execute the current job. </param>
        /// <param name="operation"> The operation name. </param>
        /// <param name="status"> Job status. </param>
        /// <param name="startOn"> The start time. </param>
        /// <param name="endOn"> The end time. </param>
        /// <param name="activityId"> ActivityId of job. </param>
        /// <param name="jobType"> This property will be used as the discriminator for deciding the specific types in the polymorphic chain of types. </param>
        /// <param name="workloadType"> Workload type of the job. </param>
        /// <param name="duration"> Time elapsed during the execution of this job. </param>
        /// <param name="actionsInfo"> Gets or sets the state/actions applicable on this job like cancel/retry. </param>
        /// <param name="errorDetails"> Error details on execution of this job. </param>
        /// <param name="extendedInfo"> Additional information about the job. </param>
        internal AzureWorkloadJob(string entityFriendlyName, BackupManagementType? backupManagementType, string operation, string status, DateTimeOffset? startOn, DateTimeOffset? endOn, string activityId, string jobType, string workloadType, TimeSpan? duration, IList<JobSupportedAction> actionsInfo, IList<AzureWorkloadErrorInfo> errorDetails, AzureWorkloadJobExtendedInfo extendedInfo) : base(entityFriendlyName, backupManagementType, operation, status, startOn, endOn, activityId, jobType)
        {
            WorkloadType = workloadType;
            Duration = duration;
            ActionsInfo = actionsInfo;
            ErrorDetails = errorDetails;
            ExtendedInfo = extendedInfo;
            JobType = jobType ?? "AzureWorkloadJob";
        }

        /// <summary> Workload type of the job. </summary>
        public string WorkloadType { get; set; }
        /// <summary> Time elapsed during the execution of this job. </summary>
        public TimeSpan? Duration { get; set; }
        /// <summary> Gets or sets the state/actions applicable on this job like cancel/retry. </summary>
        public IList<JobSupportedAction> ActionsInfo { get; }
        /// <summary> Error details on execution of this job. </summary>
        public IList<AzureWorkloadErrorInfo> ErrorDetails { get; }
        /// <summary> Additional information about the job. </summary>
        public AzureWorkloadJobExtendedInfo ExtendedInfo { get; set; }
    }
}
