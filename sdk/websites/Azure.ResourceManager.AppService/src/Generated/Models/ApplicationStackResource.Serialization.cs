// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.Models;

namespace Azure.ResourceManager.AppService.Models
{
    public partial class ApplicationStackResource : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(Kind))
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(Kind);
            }
            writer.WritePropertyName("properties");
            writer.WriteStartObject();
            if (Optional.IsDefined(StackName))
            {
                writer.WritePropertyName("name");
                writer.WriteStringValue(StackName);
            }
            if (Optional.IsDefined(Display))
            {
                writer.WritePropertyName("display");
                writer.WriteStringValue(Display);
            }
            if (Optional.IsDefined(Dependency))
            {
                writer.WritePropertyName("dependency");
                writer.WriteStringValue(Dependency);
            }
            if (Optional.IsCollectionDefined(MajorVersions))
            {
                writer.WritePropertyName("majorVersions");
                writer.WriteStartArray();
                foreach (var item in MajorVersions)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsCollectionDefined(Frameworks))
            {
                writer.WritePropertyName("frameworks");
                writer.WriteStartArray();
                foreach (var item in Frameworks)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            if (Optional.IsCollectionDefined(IsDeprecated))
            {
                writer.WritePropertyName("isDeprecated");
                writer.WriteStartArray();
                foreach (var item in IsDeprecated)
                {
                    writer.WriteObjectValue(item);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }

        internal static ApplicationStackResource DeserializeApplicationStackResource(JsonElement element)
        {
            Optional<string> kind = default;
            ResourceIdentifier id = default;
            string name = default;
            ResourceType type = default;
            Optional<SystemData> systemData = default;
            Optional<string> name0 = default;
            Optional<string> display = default;
            Optional<string> dependency = default;
            Optional<IList<StackMajorVersion>> majorVersions = default;
            Optional<IList<ApplicationStack>> frameworks = default;
            Optional<IList<ApplicationStack>> isDeprecated = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("kind"))
                {
                    kind = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    id = new ResourceIdentifier(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    type = new ResourceType(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("systemData"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    systemData = JsonSerializer.Deserialize<SystemData>(property.Value.ToString());
                    continue;
                }
                if (property.NameEquals("properties"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        if (property0.NameEquals("name"))
                        {
                            name0 = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("display"))
                        {
                            display = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("dependency"))
                        {
                            dependency = property0.Value.GetString();
                            continue;
                        }
                        if (property0.NameEquals("majorVersions"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<StackMajorVersion> array = new List<StackMajorVersion>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(StackMajorVersion.DeserializeStackMajorVersion(item));
                            }
                            majorVersions = array;
                            continue;
                        }
                        if (property0.NameEquals("frameworks"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<ApplicationStack> array = new List<ApplicationStack>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(ApplicationStack.DeserializeApplicationStack(item));
                            }
                            frameworks = array;
                            continue;
                        }
                        if (property0.NameEquals("isDeprecated"))
                        {
                            if (property0.Value.ValueKind == JsonValueKind.Null)
                            {
                                property0.ThrowNonNullablePropertyIsNull();
                                continue;
                            }
                            List<ApplicationStack> array = new List<ApplicationStack>();
                            foreach (var item in property0.Value.EnumerateArray())
                            {
                                array.Add(ApplicationStack.DeserializeApplicationStack(item));
                            }
                            isDeprecated = array;
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new ApplicationStackResource(id, name, type, systemData.Value, name0.Value, display.Value, dependency.Value, Optional.ToList(majorVersions), Optional.ToList(frameworks), Optional.ToList(isDeprecated), kind.Value);
        }
    }
}
