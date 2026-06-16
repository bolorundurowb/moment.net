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
    /// Returns a localised relative time string from the date to now.
    /// </summary>
    /// <param name="dateTime">The date to compare with now.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string, such as <c>5 minutes ago</c>.</returns>
    public static string FromNow(this DateTime dateTime, CultureInfo? ci = null) =>
        dateTime.FromNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to now.
    /// </summary>
    /// <param name="dateTime">The date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string FromNow(this DateTime dateTime, bool withoutSuffix, CultureInfo? ci = null) =>
        dateTime.Kind == DateTimeKind.Utc
            ? ParseFromPastTimeSpan(DateTime.UtcNow - dateTime, withoutSuffix, ci)
            : ParseFromPastTimeSpan(DateTime.Now - dateTime, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to another date.
    /// </summary>
    /// <param name="dateTime">The earlier date to compare.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string, such as <c>6 years ago</c>.</returns>
    public static string From(this DateTime dateTime, DateTime comparisonDateTime, CultureInfo? ci = null) =>
        dateTime.From(comparisonDateTime, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to another date.
    /// </summary>
    /// <param name="dateTime">The date to compare.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string From(this DateTime dateTime, DateTime comparisonDateTime, bool withoutSuffix, CultureInfo? ci = null)
    {
        var startDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        var endDate = comparisonDateTime.Kind == DateTimeKind.Utc ? comparisonDateTime : comparisonDateTime.ToUniversalTime();
        return ParseFromPastTimeSpan(endDate - startDate, withoutSuffix, ci);
    }

    /// <summary>
    /// Returns a localised relative time string from now to the date.
    /// </summary>
    /// <param name="dateTime">The future date to compare with now.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string, such as <c>in 3 days</c>.</returns>
    public static string ToNow(this DateTime dateTime, CultureInfo? ci = null) =>
        dateTime.ToNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from now to the date.
    /// </summary>
    /// <param name="dateTime">The future date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string ToNow(this DateTime dateTime, bool withoutSuffix, CultureInfo? ci = null) =>
        dateTime.Kind == DateTimeKind.Utc
            ? ParseFromFutureTimeSpan(dateTime - DateTime.UtcNow, withoutSuffix, ci)
            : ParseFromFutureTimeSpan(dateTime - DateTime.Now, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to a future date.
    /// </summary>
    /// <param name="dateTime">The start date.</param>
    /// <param name="comparisonDateTime">The future date to compare against.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string, such as <c>in 3 days</c>.</returns>
    public static string To(this DateTime dateTime, DateTime comparisonDateTime, CultureInfo? ci = null) =>
        dateTime.To(comparisonDateTime, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to a future date.
    /// </summary>
    /// <param name="dateTime">The start date.</param>
    /// <param name="comparisonDateTime">The future date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string To(this DateTime dateTime, DateTime comparisonDateTime, bool withoutSuffix, CultureInfo? ci = null)
    {
        var startDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        var endDate = comparisonDateTime.Kind == DateTimeKind.Utc ? comparisonDateTime : comparisonDateTime.ToUniversalTime();
        return ParseFromFutureTimeSpan(endDate - startDate, withoutSuffix, ci);
    }

    /// <summary>
    /// Returns a calendar-style formatted string relative to now.
    /// </summary>
    /// <param name="dateTime">The date to format.</param>
    /// <param name="formats">Optional calendar format strings.</param>
    /// <returns>A formatted calendar time string.</returns>
    public static string CalendarTime(this DateTime dateTime, CalendarTimeFormats? formats = null) =>
        CalendarTime(dateTime, DateTime.Now, formats);

    /// <summary>
    /// Returns a calendar-style formatted string relative to a reference date.
    /// </summary>
    /// <param name="dateTime">The date to format.</param>
    /// <param name="comparisonDateTime">The reference date used to choose the calendar format.</param>
    /// <param name="formats">Optional calendar format strings.</param>
    /// <param name="ci">The culture used for default calendar format strings.</param>
    /// <returns>A formatted calendar time string.</returns>
    public static string CalendarTime(this DateTime dateTime, DateTime comparisonDateTime,
        CalendarTimeFormats? formats = null, CultureInfo? ci = null)
    {
        formats ??= new CalendarTimeFormats(ci);
        var startDate = dateTime.Kind == DateTimeKind.Local ? dateTime : dateTime.ToLocalTime();
        var endDate = comparisonDateTime.Kind == DateTimeKind.Local ? comparisonDateTime : comparisonDateTime.ToLocalTime();

        if (startDate.Date == endDate.Date)
            return startDate.ToString(formats.SameDay);

        if (startDate.Date == endDate.AddDays(1).Date)
            return startDate.ToString(formats.NextDay);

        if (startDate.Date == endDate.AddDays(-1).Date)
            return startDate.ToString(formats.LastDay);

        if (startDate.Date >= endDate.AddDays(2).Date && startDate.Date <= endDate.AddDays(7).Date)
            return startDate.ToString(formats.NextWeek);

        if (startDate.Date <= endDate.AddDays(-2).Date && startDate.Date >= endDate.AddDays(-7).Date)
            return startDate.ToString(formats.LastWeek);

        return startDate.ToString(formats.EverythingElse);
    }

    /// <summary>
    /// Returns the number of seconds between the date and the Unix epoch.
    /// </summary>
    /// <param name="dateTime">The date to convert.</param>
    /// <returns>The Unix timestamp in seconds.</returns>
    public static double UnixTimestampInSeconds(this DateTime dateTime)
    {
        var dateInstance = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        var timeSpan = dateInstance - UnixEpoch;
        return timeSpan.TotalSeconds;
    }

    /// <summary>
    /// Returns the number of milliseconds between the date and the Unix epoch.
    /// </summary>
    /// <param name="dateTime">The date to convert.</param>
    /// <returns>The Unix timestamp in milliseconds.</returns>
    public static double UnixTimestampInMilliseconds(this DateTime dateTime)
    {
        var dateInstance = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        var timeSpan = dateInstance - UnixEpoch;
        return timeSpan.TotalMilliseconds;
    }

    /// <summary>
    /// Returns a formatted date string. If no format is specified it returns an ISO-8601 string with no fractional seconds.
    /// </summary>
    /// <param name="dateTime">The date to format.</param>
    /// <param name="format">An optional custom format string.</param>
    /// <param name="cultureInfo">The culture used for formatting.</param>
    /// <returns>The formatted date string.</returns>
    public static string Format(this DateTime dateTime, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTime.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to now.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare with now.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string FromNow(this DateTimeOffset dateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.FromNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to now.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string FromNow(this DateTimeOffset dateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(DateTimeOffset.UtcNow - dateTimeOffset, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to another offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string From(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.From(comparisonDateTimeOffset, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to another offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string From(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(comparisonDateTimeOffset - dateTimeOffset, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from now to the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The future date to compare with now.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string ToNow(this DateTimeOffset dateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.ToNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from now to the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The future date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string ToNow(this DateTimeOffset dateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromFutureTimeSpan(dateTimeOffset - DateTimeOffset.UtcNow, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to a future offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The start date.</param>
    /// <param name="comparisonDateTimeOffset">The future date to compare against.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string To(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.To(comparisonDateTimeOffset, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to a future offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The start date.</param>
    /// <param name="comparisonDateTimeOffset">The future date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation. Uses the configured default culture when omitted.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string To(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromFutureTimeSpan(comparisonDateTimeOffset - dateTimeOffset, withoutSuffix, ci);

    /// <summary>
    /// Returns a calendar-style formatted string relative to now.
    /// </summary>
    /// <param name="dateTimeOffset">The date to format.</param>
    /// <param name="formats">Optional calendar format strings.</param>
    /// <returns>A formatted calendar time string.</returns>
    public static string CalendarTime(this DateTimeOffset dateTimeOffset, CalendarTimeFormats? formats = null) =>
        CalendarTime(dateTimeOffset, DateTimeOffset.Now, formats);

    /// <summary>
    /// Returns a calendar-style formatted string relative to a reference date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to format.</param>
    /// <param name="comparisonDateTimeOffset">The reference date used to choose the calendar format.</param>
    /// <param name="formats">Optional calendar format strings.</param>
    /// <param name="ci">The culture used for default calendar format strings.</param>
    /// <returns>A formatted calendar time string.</returns>
    public static string CalendarTime(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset,
        CalendarTimeFormats? formats = null, CultureInfo? ci = null)
    {
        formats ??= new CalendarTimeFormats(ci);
        var startDate = dateTimeOffset.ToLocalTime();
        var endDate = comparisonDateTimeOffset.ToLocalTime();

        if (startDate.Date == endDate.Date)
            return startDate.ToString(formats.SameDay);

        if (startDate.Date == endDate.AddDays(1).Date)
            return startDate.ToString(formats.NextDay);

        if (startDate.Date == endDate.AddDays(-1).Date)
            return startDate.ToString(formats.LastDay);

        if (startDate.Date >= endDate.AddDays(2).Date && startDate.Date <= endDate.AddDays(7).Date)
            return startDate.ToString(formats.NextWeek);

        if (startDate.Date <= endDate.AddDays(-2).Date && startDate.Date >= endDate.AddDays(-7).Date)
            return startDate.ToString(formats.LastWeek);

        return startDate.ToString(formats.EverythingElse);
    }

    /// <summary>
    /// Returns the number of seconds between the offset-aware date and the Unix epoch.
    /// </summary>
    /// <param name="dateTimeOffset">The date to convert.</param>
    /// <returns>The Unix timestamp in seconds.</returns>
    public static double UnixTimestampInSeconds(this DateTimeOffset dateTimeOffset) =>
        (dateTimeOffset.UtcDateTime - UnixEpoch).TotalSeconds;

    /// <summary>
    /// Returns the number of milliseconds between the offset-aware date and the Unix epoch.
    /// </summary>
    /// <param name="dateTimeOffset">The date to convert.</param>
    /// <returns>The Unix timestamp in milliseconds.</returns>
    public static double UnixTimestampInMilliseconds(this DateTimeOffset dateTimeOffset) =>
        (dateTimeOffset.UtcDateTime - UnixEpoch).TotalMilliseconds;

    /// <summary>
    /// Returns a formatted date string for a <see cref="DateTimeOffset"/>. If no format is specified it returns an ISO-8601 string with offset.
    /// </summary>
    /// <param name="dateTimeOffset">The date to format.</param>
    /// <param name="format">An optional custom format string.</param>
    /// <param name="cultureInfo">The culture used for formatting.</param>
    /// <returns>The formatted date string.</returns>
    public static string Format(this DateTimeOffset dateTimeOffset, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTimeOffset.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
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