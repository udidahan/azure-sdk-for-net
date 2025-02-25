// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using Azure.Core;

namespace Azure.ResourceManager.SecurityInsights.Models
{
    /// <summary> GetInsights Query Errors. </summary>
    internal partial class GetInsightsErrorKind
    {
        /// <summary> Initializes a new instance of GetInsightsErrorKind. </summary>
        /// <param name="kind"> the query kind. </param>
        /// <param name="errorMessage"> the error message. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="errorMessage"/> is null. </exception>
        internal GetInsightsErrorKind(GetInsightsError kind, string errorMessage)
        {
            Argument.AssertNotNull(errorMessage, nameof(errorMessage));

            Kind = kind;
            ErrorMessage = errorMessage;
        }

        /// <summary> Initializes a new instance of GetInsightsErrorKind. </summary>
        /// <param name="kind"> the query kind. </param>
        /// <param name="queryId"> the query id. </param>
        /// <param name="errorMessage"> the error message. </param>
        internal GetInsightsErrorKind(GetInsightsError kind, string queryId, string errorMessage)
        {
            Kind = kind;
            QueryId = queryId;
            ErrorMessage = errorMessage;
        }

        /// <summary> the query kind. </summary>
        public GetInsightsError Kind { get; }
        /// <summary> the query id. </summary>
        public string QueryId { get; }
        /// <summary> the error message. </summary>
        public string ErrorMessage { get; }
    }
}
