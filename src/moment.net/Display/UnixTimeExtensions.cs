using System;
using System.Globalization;

namespace MomentNet.Display;

public static class UnixTimeExtensions
{
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Returns the number of seconds between the date and the Unix epoch.
    /// </summary>
    public static double UnixTimestampInSeconds(this DateTime dateTime)
    {
        var dateInstance = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return (dateInstance - UnixEpoch).TotalSeconds;
    }

    /// <summary>
    /// Returns the number of milliseconds between the date and the Unix epoch.
    /// </summary>
    public static double UnixTimestampInMilliseconds(this DateTime dateTime)
    {
        var dateInstance = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return (dateInstance - UnixEpoch).TotalMilliseconds;
    }

    /// <summary>
    /// Returns the number of seconds between the offset-aware date and the Unix epoch.
    /// </summary>
    public static double UnixTimestampInSeconds(this DateTimeOffset dateTimeOffset) =>
        (dateTimeOffset.UtcDateTime - UnixEpoch).TotalSeconds;

    /// <summary>
    /// Returns the number of milliseconds between the offset-aware date and the Unix epoch.
    /// </summary>
    public static double UnixTimestampInMilliseconds(this DateTimeOffset dateTimeOffset) =>
        (dateTimeOffset.UtcDateTime - UnixEpoch).TotalMilliseconds;
}
