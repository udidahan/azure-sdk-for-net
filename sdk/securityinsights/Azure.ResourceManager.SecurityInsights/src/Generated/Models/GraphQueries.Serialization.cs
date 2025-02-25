// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.SecurityInsights.Models
{
    public partial class GraphQueries : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(MetricName))
            {
                writer.WritePropertyName("metricName");
                writer.WriteStringValue(MetricName);
            }
            if (Optional.IsDefined(Legend))
            {
                writer.WritePropertyName("legend");
                writer.WriteStringValue(Legend);
            }
            if (Optional.IsDefined(BaseQuery))
            {
                writer.WritePropertyName("baseQuery");
                writer.WriteStringValue(BaseQuery);
            }
            writer.WriteEndObject();
        }

        internal static GraphQueries DeserializeGraphQueries(JsonElement element)
        {
            Optional<string> metricName = default;
            Optional<string> legend = default;
            Optional<string> baseQuery = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("metricName"))
                {
                    metricName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("legend"))
                {
                    legend = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("baseQuery"))
                {
                    baseQuery = property.Value.GetString();
                    continue;
                }
            }
            return new GraphQueries(metricName.Value, legend.Value, baseQuery.Value);
        }
    }
}
