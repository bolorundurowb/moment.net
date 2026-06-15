using System;
using System.Globalization;
using MomentNet.Display.Models;
using MomentNet.I18n;

namespace MomentNet.Display;

public static class RelativeTimeExtensions
{
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
    private const double DaysInAMonth = DaysInAYear / 12;

    /// <summary>
    /// Get the relative time from a given date time to the current time
    /// </summary>
    public static string FromNow(this DateTime This, CultureInfo? ci = null) =>
        This.FromNow(false, ci);

    /// <summary>
    /// Get the relative time from a given date time to the current time, optionally suppressing the suffix.
    /// </summary>
    public static string FromNow(this DateTime This, bool withoutSuffix, CultureInfo? ci = null) =>
        This.Kind == DateTimeKind.Utc
            ? ParseFromPastTimeSpan(DateTime.UtcNow - This, withoutSuffix, ci)
            : ParseFromPastTimeSpan(DateTime.Now - This, withoutSuffix, ci);

    /// <summary>
    /// Get the relative time from a given date time to another date time instance
    /// </summary>
    public static string From(this DateTime This, DateTime dateTime, CultureInfo? ci = null) =>
        This.From(dateTime, false, ci);

    /// <summary>
    /// Get the relative time from a given date time to another date time instance, optionally suppressing the suffix.
    /// </summary>
    public static string From(this DateTime This, DateTime dateTime, bool withoutSuffix, CultureInfo? ci = null)
    {
        var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return ParseFromPastTimeSpan(endDate - startDate, withoutSuffix, ci);
    }

    /// <summary>
    /// Get the relative time from the current date time instance to a time frame in the future
    /// </summary>
    public static string ToNow(this DateTime This, CultureInfo? ci = null) =>
        This.ToNow(false, ci);

    /// <summary>
    /// Get the relative time from the current date time instance to a time frame in the future, optionally suppressing the suffix.
    /// </summary>
    public static string ToNow(this DateTime This, bool withoutSuffix, CultureInfo? ci = null) =>
        This.Kind == DateTimeKind.Utc
            ? ParseFromFutureTimeSpan(This - DateTime.UtcNow, withoutSuffix, ci)
            : ParseFromFutureTimeSpan(This - DateTime.Now, withoutSuffix, ci);

    /// <summary>
    /// Get the relative time from the a date time instance to a time frame in the future
    /// </summary>
    public static string To(this DateTime This, DateTime dateTime, CultureInfo? ci = null) =>
        This.To(dateTime, false, ci);

    /// <summary>
    /// Get the relative time from the a date time instance to a time frame in the future, optionally suppressing the suffix.
    /// </summary>
    public static string To(this DateTime This, DateTime dateTime, bool withoutSuffix, CultureInfo? ci = null)
    {
        var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return ParseFromFutureTimeSpan(endDate - startDate, withoutSuffix, ci);
    }

    /// <summary>
    /// Get the calendar time description from this DateTime instance to the current time
    /// </summary>
    public static string CalendarTime(this DateTime This, CalendarTimeFormats? formats = null) =>
        CalendarTime(This, DateTime.Now, formats);

    /// <summary>
    /// Get the calendar time description from this DateTime instance to a specified DateTime instance
    /// </summary>
    public static string CalendarTime(this DateTime This, DateTime dateTime,
        CalendarTimeFormats? formats = null, CultureInfo? ci = null)
    {
        formats ??= new CalendarTimeFormats(ci);
        var startDate = This.Kind == DateTimeKind.Local ? This : This.ToLocalTime();
        var endDate = dateTime.Kind == DateTimeKind.Local ? dateTime : dateTime.ToLocalTime();
        var timeDiff = endDate - startDate;

        if (startDate.Date == endDate.Date)
            return endDate.ToString(formats.SameDay);

        if (startDate.AddDays(1).Date == endDate.Date)
            return endDate.ToString(formats.NextDay);

        if (startDate.AddDays(-1).Date == endDate.Date)
            return endDate.ToString(formats.LastDay);

        if (timeDiff.TotalDays > 1 && timeDiff.TotalDays < 7)
            return endDate.ToString(formats.NextWeek);

        if (timeDiff.TotalDays >= -6 && timeDiff.TotalDays < -1)
            return endDate.ToString(formats.LastWeek);

        return endDate.ToString(formats.EverythingElse);
    }

    /// <summary>
    /// Get the total number of seconds since the unix epoch
    /// </summary>
    public static double UnixTimestampInSeconds(this DateTime This)
    {
        var dateInstance = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var timeSpan = dateInstance - UnixEpoch;
        return timeSpan.TotalSeconds;
    }

    /// <summary>
    /// Get the total number of milliseconds since the unix epoch
    /// </summary>
    public static double UnixTimestampInMilliseconds(this DateTime This)
    {
        var dateInstance = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var timeSpan = dateInstance - UnixEpoch;
        return timeSpan.TotalMilliseconds;
    }

    /// <summary>
    /// Returns a formatted date string. If no format is specified it returns an ISO-8601 string with no fractional seconds.
    /// </summary>
    public static string Format(this DateTime dateTime, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTime.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Get the relative time from a given <see cref="DateTimeOffset"/> to now
    /// </summary>
    public static string FromNow(this DateTimeOffset This, CultureInfo? ci = null) =>
        This.FromNow(false, ci);

    /// <summary>
    /// Get the relative time from a given <see cref="DateTimeOffset"/> to now, optionally suppressing the suffix.
    /// </summary>
    public static string FromNow(this DateTimeOffset This, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(DateTimeOffset.UtcNow - This, withoutSuffix, ci);

    /// <summary>
    /// Get the relative time from a given <see cref="DateTimeOffset"/> to another <see cref="DateTimeOffset"/>
    /// </summary>
    public static string From(this DateTimeOffset This, DateTimeOffset dateTime, CultureInfo? ci = null) =>
        This.From(dateTime, false, ci);

    /// <summary>
    /// Get the relative time from a given <see cref="DateTimeOffset"/> to another <see cref="DateTimeOffset"/>, optionally suppressing the suffix.
    /// </summary>
    public static string From(this DateTimeOffset This, DateTimeOffset dateTime, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(dateTime - This, withoutSuffix, ci);

    /// <summary>
    /// Get the relative time from now to a future <see cref="DateTimeOffset"/>
    /// </summary>
    public static string ToNow(this DateTimeOffset This, CultureInfo? ci = null) =>
        This.ToNow(false, ci);

    /// <summary>
    /// Get the relative time from now to a future <see cref="DateTimeOffset"/>, optionally suppressing the suffix.
    /// </summary>
    public static string ToNow(this DateTimeOffset This, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromFutureTimeSpan(This - DateTimeOffset.UtcNow, withoutSuffix, ci);

    /// <summary>
    /// Get the relative time from a <see cref="DateTimeOffset"/> to a future <see cref="DateTimeOffset"/>
    /// </summary>
    public static string To(this DateTimeOffset This, DateTimeOffset dateTime, CultureInfo? ci = null) =>
        This.To(dateTime, false, ci);

    /// <summary>
    /// Get the relative time from a <see cref="DateTimeOffset"/> to a future <see cref="DateTimeOffset"/>, optionally suppressing the suffix.
    /// </summary>
    public static string To(this DateTimeOffset This, DateTimeOffset dateTime, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromFutureTimeSpan(dateTime - This, withoutSuffix, ci);

    /// <summary>
    /// Get the calendar time description from this <see cref="DateTimeOffset"/> to now
    /// </summary>
    public static string CalendarTime(this DateTimeOffset This, CalendarTimeFormats? formats = null) =>
        CalendarTime(This, DateTimeOffset.Now, formats);

    /// <summary>
    /// Get the calendar time description from this <see cref="DateTimeOffset"/> to a specified <see cref="DateTimeOffset"/>
    /// </summary>
    public static string CalendarTime(this DateTimeOffset This, DateTimeOffset dateTime,
        CalendarTimeFormats? formats = null, CultureInfo? ci = null)
    {
        formats ??= new CalendarTimeFormats(ci);
        var startDate = This.ToLocalTime();
        var endDate = dateTime.ToLocalTime();
        var timeDiff = endDate - startDate;

        if (startDate.Date == endDate.Date)
            return endDate.ToString(formats.SameDay);

        if (startDate.AddDays(1).Date == endDate.Date)
            return endDate.ToString(formats.NextDay);

        if (startDate.AddDays(-1).Date == endDate.Date)
            return endDate.ToString(formats.LastDay);

        if (timeDiff.TotalDays > 1 && timeDiff.TotalDays < 7)
            return endDate.ToString(formats.NextWeek);

        if (timeDiff.TotalDays >= -6 && timeDiff.TotalDays < -1)
            return endDate.ToString(formats.LastWeek);

        return endDate.ToString(formats.EverythingElse);
    }

    /// <summary>
    /// Get the total number of seconds since the Unix epoch for a <see cref="DateTimeOffset"/>
    /// </summary>
    public static double UnixTimestampInSeconds(this DateTimeOffset This) =>
        (This.UtcDateTime - UnixEpoch).TotalSeconds;

    /// <summary>
    /// Get the total number of milliseconds since the Unix epoch for a <see cref="DateTimeOffset"/>
    /// </summary>
    public static double UnixTimestampInMilliseconds(this DateTimeOffset This) =>
        (This.UtcDateTime - UnixEpoch).TotalMilliseconds;

    /// <summary>
    /// Returns a formatted date string for a <see cref="DateTimeOffset"/>. If no format is specified it returns an ISO-8601 string with offset.
    /// </summary>
    public static string Format(this DateTimeOffset dateTime, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTime.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

    private static string ParseFromPastTimeSpan(TimeSpan timeSpan, bool withoutSuffix = false, CultureInfo? ci = null)
    {
        if (withoutSuffix)
            return ParseTimeDifference(timeSpan, ci);

        ci ??= CultureWrapper.GetDefaultCulture();
        using var lm = new LocalizationManager(ci);
        return $"{ParseTimeDifference(timeSpan, ci)} {lm.GetString("TIME_AGO")}";
    }

    private static string ParseFromFutureTimeSpan(TimeSpan timeSpan, bool withoutSuffix = false, CultureInfo? ci = null)
    {
        if (withoutSuffix)
            return ParseTimeDifference(timeSpan, ci);

        ci ??= CultureWrapper.GetDefaultCulture();
        using var lm = new LocalizationManager(ci);
        return $"{lm.GetString("TIME_IN")} {ParseTimeDifference(timeSpan, ci)}";
    }

    private static string ParseTimeDifference(TimeSpan timeSpan, CultureInfo? ci = null)
    {
        ci ??= CultureWrapper.GetDefaultCulture();
        using var lm = new LocalizationManager(ci);

        var totalTimeInSeconds = Math.Abs(timeSpan.TotalSeconds);

        if (totalTimeInSeconds <= 44.0)
            return lm.GetString("TIME_FEW_SECONDS");

        if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            return lm.GetString("TIME_ONE_MINUTE");

        var totalTimeInMinutes = Math.Abs(timeSpan.TotalMinutes);

        if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            return $"{Math.Round(totalTimeInMinutes)} {lm.GetString("TIME_MINUTES")}";

        if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            return lm.GetString("TIME_ONE_HOUR");

        var totalTimeInHours = Math.Abs(timeSpan.TotalHours);

        if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            return $"{Math.Round(totalTimeInHours)} {lm.GetString("TIME_HOURS")}";

        if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            return lm.GetString("TIME_ONE_DAY");

        var totalTimeInDays = Math.Abs(timeSpan.TotalDays);

        if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            return $"{Math.Round(totalTimeInDays)} {lm.GetString("TIME_DAYS")}";

        return totalTimeInDays switch
        {
            > 25 and <= 45 => lm.GetString("TIME_ONE_MONTH"),
            > 45 and <= 319 => $"{Math.Round(totalTimeInDays / DaysInAMonth)} {lm.GetString("TIME_MONTHS")}",
            > 319 and <= 547 => lm.GetString("TIME_ONE_YEAR"),
            > 547 => $"{Math.Round(totalTimeInDays / DaysInAYear)} {lm.GetString("TIME_YEARS")}",
            _ => throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
                "in the time span sent could not be parsed.")
        };
    }
}