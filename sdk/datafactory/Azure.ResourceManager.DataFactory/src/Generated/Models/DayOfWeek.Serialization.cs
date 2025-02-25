// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.DataFactory.Models
{
    internal static partial class DayOfWeekExtensions
    {
        public static string ToSerialString(this DayOfWeek value) => value switch
        {
            DayOfWeek.Sunday => "Sunday",
            DayOfWeek.Monday => "Monday",
            DayOfWeek.Tuesday => "Tuesday",
            DayOfWeek.Wednesday => "Wednesday",
            DayOfWeek.Thursday => "Thursday",
            DayOfWeek.Friday => "Friday",
            DayOfWeek.Saturday => "Saturday",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown DayOfWeek value.")
        };

        public static DayOfWeek ToDayOfWeek(this string value)
        {
            if (string.Equals(value, "Sunday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Sunday;
            if (string.Equals(value, "Monday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Monday;
            if (string.Equals(value, "Tuesday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Tuesday;
            if (string.Equals(value, "Wednesday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Wednesday;
            if (string.Equals(value, "Thursday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Thursday;
            if (string.Equals(value, "Friday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Friday;
            if (string.Equals(value, "Saturday", StringComparison.InvariantCultureIgnoreCase)) return DayOfWeek.Saturday;
            throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown DayOfWeek value.");
        }
    }
}
