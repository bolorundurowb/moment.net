using System;
using System.Globalization;

namespace MomentNet.Display;

public static class FormatExtensions
{
    /// <summary>
    /// Returns a formatted date string. If no format is specified it returns an ISO-8601 string with no fractional seconds.
    /// </summary>
    public static string Format(this DateTime dateTime, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTime.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a formatted date string for a <see cref="DateTimeOffset"/>. If no format is specified it returns an ISO-8601 string with offset.
    /// </summary>
    public static string Format(this DateTimeOffset dateTimeOffset, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTimeOffset.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Returns a formatted date string for a <see cref="DateOnly"/>. If no format is specified it returns an ISO-8601 date string.
    /// </summary>
    public static string Format(this DateOnly dateOnly, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-dd" : format;
        return dateOnly.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a formatted time string for a <see cref="TimeOnly"/>. If no format is specified it returns an ISO-8601 time string.
    /// </summary>
    public static string Format(this TimeOnly timeOnly, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "HH:mm:ss" : format;
        return timeOnly.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }
#endif
}
