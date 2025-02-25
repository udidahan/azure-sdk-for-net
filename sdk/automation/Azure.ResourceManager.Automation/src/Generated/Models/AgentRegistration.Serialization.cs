// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Automation.Models
{
    public partial class AgentRegistration
    {
        internal static AgentRegistration DeserializeAgentRegistration(JsonElement element)
        {
            Optional<string> dscMetaConfiguration = default;
            Optional<Uri> endpoint = default;
            Optional<AgentRegistrationKeys> keys = default;
            Optional<ResourceIdentifier> id = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("dscMetaConfiguration"))
                {
                    dscMetaConfiguration = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("endpoint"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        endpoint = null;
                        continue;
                    }
                    endpoint = new Uri(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("keys"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    keys = AgentRegistrationKeys.DeserializeAgentRegistrationKeys(property.Value);
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    id = new ResourceIdentifier(property.Value.GetString());
                    continue;
                }
            }
            return new AgentRegistration(dscMetaConfiguration.Value, endpoint.Value, keys.Value, id.Value);
        }
    }
}
