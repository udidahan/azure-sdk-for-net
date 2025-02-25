// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Nginx.Models
{
    public partial class NginxConfigurationFile : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(Content))
            {
                writer.WritePropertyName("content");
                writer.WriteStringValue(Content);
            }
            if (Optional.IsDefined(VirtualPath))
            {
                writer.WritePropertyName("virtualPath");
                writer.WriteStringValue(VirtualPath);
            }
            writer.WriteEndObject();
        }

        internal static NginxConfigurationFile DeserializeNginxConfigurationFile(JsonElement element)
        {
            Optional<string> content = default;
            Optional<string> virtualPath = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("content"))
                {
                    content = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("virtualPath"))
                {
                    virtualPath = property.Value.GetString();
                    continue;
                }
            }
            return new NginxConfigurationFile(content.Value, virtualPath.Value);
        }
    }
}
