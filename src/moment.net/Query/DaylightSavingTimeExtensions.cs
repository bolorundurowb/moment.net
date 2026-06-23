using System;

namespace MomentNet.Query;

public static class DaylightSavingTimeExtensions
{
    /// <summary>
    /// Returns whether the date falls in daylight saving time for the local time zone.
    /// </summary>
    public static bool IsDaylightSavingTime(this DateTime dateTime) =>
        dateTime.IsDaylightSavingTime(TimeZoneInfo.Local);

    /// <summary>
    /// Returns whether the date falls in daylight saving time for the supplied time zone.
    /// </summary>
    public static bool IsDaylightSavingTime(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
    {
        if (timeZoneInfo is null)
            throw new ArgumentNullException(nameof(timeZoneInfo));

        return timeZoneInfo.IsDaylightSavingTime(dateTime);
    }

    /// <summary>
    /// Returns whether the date falls in daylight saving time for the local time zone.
    /// </summary>
    public static bool IsDaylightSavingTime(this DateTimeOffset dateTime) =>
        dateTime.IsDaylightSavingTime(TimeZoneInfo.Local);

    /// <summary>
    /// Returns whether the date falls in daylight saving time for the supplied time zone.
    /// </summary>
    public static bool IsDaylightSavingTime(this DateTimeOffset dateTime, TimeZoneInfo timeZoneInfo)
    {
        if (timeZoneInfo is null)
            throw new ArgumentNullException(nameof(timeZoneInfo));

        var converted = TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
        return timeZoneInfo.IsDaylightSavingTime(converted.DateTime);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Returns whether the <see cref="DateOnly"/> falls in daylight saving time for the local time zone.
    /// </summary>
    public static bool IsDaylightSavingTime(this DateOnly dateOnly) =>
        dateOnly.IsDaylightSavingTime(TimeZoneInfo.Local);

    /// <summary>
    /// Returns whether the <see cref="DateOnly"/> falls in daylight saving time for the supplied time zone.
    /// </summary>
    public static bool IsDaylightSavingTime(this DateOnly dateOnly, TimeZoneInfo timeZoneInfo)
    {
        if (timeZoneInfo is null)
            throw new ArgumentNullException(nameof(timeZoneInfo));

        return timeZoneInfo.IsDaylightSavingTime(dateOnly.ToDateTime(default));
    }
#endif
}
