// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.SecurityDevOps.Models
{
    public partial class AzureDevOpsOrgProperties : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(ProvisioningState))
            {
                writer.WritePropertyName("provisioningState");
                writer.WriteStringValue(ProvisioningState.Value.ToString());
            }
            if (Optional.IsDefined(AutoDiscovery))
            {
                writer.WritePropertyName("autoDiscovery");
                writer.WriteStringValue(AutoDiscovery.Value.ToString());
            }
            writer.WriteEndObject();
        }

        internal static AzureDevOpsOrgProperties DeserializeAzureDevOpsOrgProperties(JsonElement element)
        {
            Optional<ProvisioningState> provisioningState = default;
            Optional<AutoDiscovery> autoDiscovery = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("provisioningState"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    provisioningState = new ProvisioningState(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("autoDiscovery"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    autoDiscovery = new AutoDiscovery(property.Value.GetString());
                    continue;
                }
            }
            return new AzureDevOpsOrgProperties(Optional.ToNullable(provisioningState), Optional.ToNullable(autoDiscovery));
        }
    }
}
