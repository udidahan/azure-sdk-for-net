// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.Communication.JobRouter
{
    internal static partial class ScoringRuleParameterSelectorExtensions
    {
        public static string ToSerialString(this ScoringRuleParameterSelector value) => value switch
        {
            ScoringRuleParameterSelector.JobLabels => "jobLabels",
            ScoringRuleParameterSelector.WorkerSelectors => "workerSelectors",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ScoringRuleParameterSelector value.")
        };

        public static ScoringRuleParameterSelector ToScoringRuleParameterSelector(this string value)
        {
            if (string.Equals(value, "jobLabels", StringComparison.InvariantCultureIgnoreCase)) return ScoringRuleParameterSelector.JobLabels;
            if (string.Equals(value, "workerSelectors", StringComparison.InvariantCultureIgnoreCase)) return ScoringRuleParameterSelector.WorkerSelectors;
            throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ScoringRuleParameterSelector value.");
        }
    }
}
