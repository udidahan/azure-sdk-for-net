// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.ResourceManager.SignalR.Models
{
    /// <summary> Parameters describes the request to regenerate access keys. </summary>
    public partial class RegenerateKeyContent
    {
        /// <summary> Initializes a new instance of RegenerateKeyContent. </summary>
        public RegenerateKeyContent()
        {
        }

        /// <summary> The type of access key. </summary>
        public KeyType? KeyType { get; set; }
    }
}
