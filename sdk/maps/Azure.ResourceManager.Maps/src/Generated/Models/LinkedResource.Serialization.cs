// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Maps.Models
{
    public partial class LinkedResource : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("uniqueName");
            writer.WriteStringValue(UniqueName);
            writer.WritePropertyName("id");
            writer.WriteStringValue(Id);
            writer.WriteEndObject();
        }

        internal static LinkedResource DeserializeLinkedResource(JsonElement element)
        {
            string uniqueName = default;
            string id = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("uniqueName"))
                {
                    uniqueName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    id = property.Value.GetString();
                    continue;
                }
            }
            return new LinkedResource(uniqueName, id);
        }
    }
}
