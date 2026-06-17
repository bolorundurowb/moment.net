using System;
using System.Globalization;
using MomentNet.I18n;

namespace MomentNet.Display;

public static class RelativeTime
{
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private const double DaysInAYear = 365.2425;
    private const double DaysInAMonth = DaysInAYear / 12;

    private static string GetString(string key, CultureInfo ci) =>
        Strings.ResourceManager.GetString(key, ci) ?? $"[missing:{key}]";

    /// <summary>
    /// Returns a localised relative time string from the date to now.
    /// </summary>
    /// <param name="dateTime">The date to compare with now.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string, such as <c>5 minutes ago</c>.</returns>
    public static string FromNow(this DateTime dateTime, CultureInfo? ci = null) =>
        dateTime.FromNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to now.
    /// </summary>
    /// <param name="dateTime">The date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
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
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string, such as <c>6 years ago</c>.</returns>
    public static string From(this DateTime dateTime, DateTime comparisonDateTime, CultureInfo? ci = null) =>
        dateTime.From(comparisonDateTime, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to another date.
    /// </summary>
    /// <param name="dateTime">The date to compare.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
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
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string, such as <c>in 3 days</c>.</returns>
    public static string ToNow(this DateTime dateTime, CultureInfo? ci = null) =>
        dateTime.ToNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from now to the date.
    /// </summary>
    /// <param name="dateTime">The future date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
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
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string, such as <c>in 3 days</c>.</returns>
    public static string To(this DateTime dateTime, DateTime comparisonDateTime, CultureInfo? ci = null) =>
        dateTime.To(comparisonDateTime, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the date to a future date.
    /// </summary>
    /// <param name="dateTime">The start date.</param>
    /// <param name="comparisonDateTime">The future date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string To(this DateTime dateTime, DateTime comparisonDateTime, bool withoutSuffix, CultureInfo? ci = null)
    {
        var startDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        var endDate = comparisonDateTime.Kind == DateTimeKind.Utc ? comparisonDateTime : comparisonDateTime.ToUniversalTime();
        return ParseFromFutureTimeSpan(endDate - startDate, withoutSuffix, ci);
    }

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to now.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare with now.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string FromNow(this DateTimeOffset dateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.FromNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to now.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string FromNow(this DateTimeOffset dateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(DateTimeOffset.UtcNow - dateTimeOffset, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to another offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string From(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.From(comparisonDateTimeOffset, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to another offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>ago</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string From(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(comparisonDateTimeOffset - dateTimeOffset, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from now to the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The future date to compare with now.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string ToNow(this DateTimeOffset dateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.ToNow(false, ci);

    /// <summary>
    /// Returns a localised relative time string from now to the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The future date to compare with now.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string ToNow(this DateTimeOffset dateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromFutureTimeSpan(dateTimeOffset - DateTimeOffset.UtcNow, withoutSuffix, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to a future offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The start date.</param>
    /// <param name="comparisonDateTimeOffset">The future date to compare against.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string To(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, CultureInfo? ci = null) =>
        dateTimeOffset.To(comparisonDateTimeOffset, false, ci);

    /// <summary>
    /// Returns a localised relative time string from the offset-aware date to a future offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The start date.</param>
    /// <param name="comparisonDateTimeOffset">The future date to compare against.</param>
    /// <param name="withoutSuffix">When true, omits words such as <c>in</c>.</param>
    /// <param name="ci">The culture used for localisation.</param>
    /// <returns>A human-readable relative time string.</returns>
    public static string To(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset, bool withoutSuffix, CultureInfo? ci = null) =>
        ParseFromFutureTimeSpan(comparisonDateTimeOffset - dateTimeOffset, withoutSuffix, ci);

    private static string ParseFromPastTimeSpan(TimeSpan timeSpan, bool withoutSuffix = false, CultureInfo? ci = null)
    {
        ci ??= CultureInfo.CurrentCulture;

        if (withoutSuffix)
            return ParseTimeDifference(timeSpan, ci);

        return $"{ParseTimeDifference(timeSpan, ci)} {GetString("TIME_AGO", ci)}";
    }

    private static string ParseFromFutureTimeSpan(TimeSpan timeSpan, bool withoutSuffix = false, CultureInfo? ci = null)
    {
        ci ??= CultureInfo.CurrentCulture;

        if (withoutSuffix)
            return ParseTimeDifference(timeSpan, ci);

        return $"{GetString("TIME_IN", ci)} {ParseTimeDifference(timeSpan, ci)}";
    }

    private static string ParseTimeDifference(TimeSpan timeSpan, CultureInfo ci)
    {
        var totalTimeInSeconds = Math.Abs(timeSpan.TotalSeconds);

        if (totalTimeInSeconds <= 44.0)
            return GetString("TIME_FEW_SECONDS", ci);

        if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            return GetString("TIME_ONE_MINUTE", ci);

        var totalTimeInMinutes = Math.Abs(timeSpan.TotalMinutes);

        if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            return $"{Math.Round(totalTimeInMinutes)} {GetString("TIME_MINUTES", ci)}";

        if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            return GetString("TIME_ONE_HOUR", ci);

        var totalTimeInHours = Math.Abs(timeSpan.TotalHours);

        if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            return $"{Math.Round(totalTimeInHours)} {GetString("TIME_HOURS", ci)}";

        if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            return GetString("TIME_ONE_DAY", ci);

        var totalTimeInDays = Math.Abs(timeSpan.TotalDays);

        if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            return $"{Math.Round(totalTimeInDays)} {GetString("TIME_DAYS", ci)}";

        return totalTimeInDays switch
        {
            > 25 and <= 45 => GetString("TIME_ONE_MONTH", ci),
            > 45 and <= 319 => $"{Math.Round(totalTimeInDays / DaysInAMonth)} {GetString("TIME_MONTHS", ci)}",
            > 319 and <= 547 => GetString("TIME_ONE_YEAR", ci),
            > 547 => $"{Math.Round(totalTimeInDays / DaysInAYear)} {GetString("TIME_YEARS", ci)}",
            _ => throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
                "in the time span sent could not be parsed.")
        };
    }
}
