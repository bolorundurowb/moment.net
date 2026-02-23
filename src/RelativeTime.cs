using System;
using moment.net.Models;
using System.Globalization;
using moment.net.Enums;
using moment.net.Localization;

namespace moment.net;

public static class RelativeTime
{
    private static readonly DateTimeOffset UnixEpoch = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
    private const double DaysInAMonth = DaysInAYear / 12;

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime Next(this DateTime dateTime, DayOfWeek dayOfWeek) =>
        new DateTimeOffset(dateTime).Next(dayOfWeek).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the <see cref="DateTimeOffset"/> for the next specified <see cref="DayOfWeek"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="dayOfWeek">The day of the week to locate.</param>
    /// <returns>The <see cref="DateTimeOffset"/> of the next matching day.</returns>
    public static DateTimeOffset Next(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek)
    {
        if (dateTimeOffset.DayOfWeek == dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(1);

        while (dateTimeOffset.DayOfWeek != dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(1);

        return dateTimeOffset;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime Next(this DateTime dateTime, DayOfWeek dayOfWeek, int count) =>
        new DateTimeOffset(dateTime).Next(dayOfWeek, count).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the <see cref="DateTimeOffset"/> for the nth next specified <see cref="DayOfWeek"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="dayOfWeek">The day of the week to locate.</param>
    /// <param name="count">The frequency or nth occurrence of the day to locate.</param>
    /// <returns>The <see cref="DateTimeOffset"/> of the nth next matching day.</returns>
    public static DateTimeOffset Next(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
            dateTimeOffset = dateTimeOffset.Next(dayOfWeek);

        return dateTimeOffset;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime Last(this DateTime dateTime, DayOfWeek dayOfWeek) =>
        new DateTimeOffset(dateTime).Last(dayOfWeek).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the <see cref="DateTimeOffset"/> for the previous specified <see cref="DayOfWeek"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="dayOfWeek">The day of the week to locate.</param>
    /// <returns>The <see cref="DateTimeOffset"/> of the previous matching day.</returns>
    public static DateTimeOffset Last(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek)
    {
        if (dateTimeOffset.DayOfWeek == dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(-1);

        while (dateTimeOffset.DayOfWeek != dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(-1);

        return dateTimeOffset;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime Last(this DateTime dateTime, DayOfWeek dayOfWeek, int count) =>
        new DateTimeOffset(dateTime).Last(dayOfWeek, count).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the <see cref="DateTimeOffset"/> for the nth previous specified <see cref="DayOfWeek"/>.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="dayOfWeek">The day of the week to locate.</param>
    /// <param name="count">The frequency or nth occurrence of the previous day to locate.</param>
    /// <returns>The <see cref="DateTimeOffset"/> of the nth previous matching day.</returns>
    public static DateTimeOffset Last(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
            dateTimeOffset = dateTimeOffset.Last(dayOfWeek);

        return dateTimeOffset;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static FinalDays Final(this DateTime dateTime) => new(new DateTimeOffset(dateTime));

    /// <summary>
    /// Provides a fluent interface to evaluate the final occurrences of days within a month or year.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <returns>A <see cref="FinalDays"/> instance for evaluating final days.</returns>
    public static FinalDays Final(this DateTimeOffset dateTimeOffset) => new(dateTimeOffset);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime StartOf(this DateTime dateTime, DateTimeAnchor timeAnchor) =>
        new DateTimeOffset(dateTime).StartOf(timeAnchor).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the start of the year, month, week, day, or hour for the given <see cref="DateTimeOffset"/> using the current culture.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="timeAnchor">The anchor point (year, month, week, day, or hour).</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the start of the anchored period.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid value is passed to the timeAnchor.</exception>
    public static DateTimeOffset StartOf(this DateTimeOffset dateTimeOffset, DateTimeAnchor timeAnchor) =>
        dateTimeOffset.StartOf(timeAnchor, CultureInfo.CurrentCulture);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime StartOf(this DateTime dateTime, DateTimeAnchor timeAnchor, CultureInfo cultureInfo) =>
        new DateTimeOffset(dateTime).StartOf(timeAnchor, cultureInfo).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the start of the year, month, week, day, or hour for the given <see cref="DateTimeOffset"/> using a specified culture.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="timeAnchor">The anchor point (year, month, week, day, or hour).</param>
    /// <param name="cultureInfo">The culture information used to determine boundaries, such as the first day of the week.</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the start of the anchored period.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid value is passed to the timeAnchor.</exception>
    public static DateTimeOffset StartOf(this DateTimeOffset dateTimeOffset, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Hour:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Day:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Week:
                var startOfWeek = dateTimeOffset.FirstDateInWeek(cultureInfo);
                return new DateTimeOffset(startOfWeek.Year, startOfWeek.Month, startOfWeek.Day, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Month:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Year:
                return new DateTimeOffset(dateTimeOffset.Year, 1, 1, 0, 0, 0, 0, dateTimeOffset.Offset);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.", nameof(timeAnchor));
        }
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime EndOf(this DateTime dateTime, DateTimeAnchor timeAnchor) =>
        new DateTimeOffset(dateTime).EndOf(timeAnchor).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the end of a year, month, week, day, or hour for the given <see cref="DateTimeOffset"/> using the current culture.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="timeAnchor">The anchor point (year, month, week, day, or hour).</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the end of the anchored period.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid value is passed to the timeAnchor.</exception>
    public static DateTimeOffset EndOf(this DateTimeOffset dateTimeOffset, DateTimeAnchor timeAnchor) =>
        dateTimeOffset.EndOf(timeAnchor, CultureInfo.CurrentCulture);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime EndOf(this DateTime dateTime, DateTimeAnchor timeAnchor, CultureInfo cultureInfo) =>
        new DateTimeOffset(dateTime).EndOf(timeAnchor, cultureInfo).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the end of a year, month, week, day, or hour for the given <see cref="DateTimeOffset"/> using a specified culture.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="timeAnchor">The anchor point (year, month, week, day, or hour).</param>
    /// <param name="cultureInfo">The culture information used to determine boundaries.</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the end of the anchored period.</returns>
    /// <exception cref="ArgumentException">Thrown when an invalid value is passed to the timeAnchor.</exception>
    public static DateTimeOffset EndOf(this DateTimeOffset dateTimeOffset, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Hour:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Day:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Week:
                var endOfWeek = dateTimeOffset.LastDateInWeek(cultureInfo);
                return new DateTimeOffset(endOfWeek.Year, endOfWeek.Month, endOfWeek.Day, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Month:
                var daysInMonth = DateTime.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month);
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, daysInMonth, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Year:
                return new DateTimeOffset(dateTimeOffset.Year, 12, DateTime.DaysInMonth(dateTimeOffset.Year, 12), 23, 59, 59, 999, dateTimeOffset.Offset);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.", nameof(timeAnchor));
        }
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string FromNow(this DateTime dateTime, CultureInfo? ci = null) =>
        new DateTimeOffset(dateTime).FromNow(ci);

    /// <summary>
    /// Gets the relative time duration from a given date and time to the current time.
    /// </summary>
    /// <param name="dateTimeOffset">A timeframe in the past.</param>
    /// <param name="ci">The optional culture info for localisation.</param>
    /// <returns>A string characterising the time span in a human-readable format.</returns>
    public static string FromNow(this DateTimeOffset dateTimeOffset, CultureInfo? ci = null) =>
        ParseFromPastTimeSpan(DateTimeOffset.Now - dateTimeOffset, ci);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string From(this DateTime dateTime, DateTime targetDateTime, CultureInfo? ci = null) =>
        new DateTimeOffset(dateTime).From(new DateTimeOffset(targetDateTime), ci);

    /// <summary>
    /// Gets the relative time duration from a given date and time to another date and time instance.
    /// </summary>
    /// <param name="dateTimeOffset">A timeframe in the past.</param>
    /// <param name="targetDateTime">A timeframe after the one being compared against.</param>
    /// <param name="ci">The optional culture info for localisation.</param>
    /// <returns>A string characterising the time span in a human-readable format.</returns>
    public static string From(this DateTimeOffset dateTimeOffset, DateTimeOffset targetDateTime, CultureInfo? ci = null)
    {
        var startDate = dateTimeOffset.ToUniversalTime();
        var endDate = targetDateTime.ToUniversalTime();
        return ParseFromPastTimeSpan(endDate - startDate, ci);
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string ToNow(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).ToNow();

    /// <summary>
    /// Gets the relative time duration from the current date and time instance to a timeframe in the future.
    /// </summary>
    /// <param name="dateTimeOffset">A timeframe in the future.</param>
    /// <returns>A string characterising the time span in a human-readable format.</returns>
    public static string ToNow(this DateTimeOffset dateTimeOffset) =>
        ParseFromFutureTimeSpan(dateTimeOffset - DateTimeOffset.Now);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string To(this DateTime dateTime, DateTime targetDateTime) =>
        new DateTimeOffset(dateTime).To(new DateTimeOffset(targetDateTime));

    /// <summary>
    /// Gets the relative time duration from a date and time instance to a timeframe in the future.
    /// </summary>
    /// <param name="dateTimeOffset">The reference timeframe being compared against.</param>
    /// <param name="targetDateTime">A timeframe in the future.</param>
    /// <returns>A string characterising the time span in a human-readable format.</returns>
    public static string To(this DateTimeOffset dateTimeOffset, DateTimeOffset targetDateTime)
    {
        var startDate = dateTimeOffset.ToUniversalTime();
        var endDate = targetDateTime.ToUniversalTime();
        return ParseFromFutureTimeSpan(endDate - startDate);
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string CalendarTime(this DateTime dateTime, CalendarTimeFormats? formats = null) =>
        new DateTimeOffset(dateTime).CalendarTime(formats);

    /// <summary>
    /// Gets the calendar time description from this DateTimeOffset instance to the current time.
    /// </summary>
    /// <param name="dateTimeOffset">The date instance to compare with the current date.</param>
    /// <param name="formats">An object dictating how the output string should be formatted.</param>
    /// <returns>A string characterising the calendar time difference.</returns>
    public static string CalendarTime(this DateTimeOffset dateTimeOffset, CalendarTimeFormats? formats = null) =>
        CalendarTime(dateTimeOffset, DateTimeOffset.Now, formats);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string CalendarTime(this DateTime dateTime, DateTime targetDateTime, CalendarTimeFormats? formats = null, CultureInfo? ci = null) =>
        new DateTimeOffset(dateTime).CalendarTime(new DateTimeOffset(targetDateTime), formats, ci);

    /// <summary>
    /// Gets the calendar time description from this DateTimeOffset instance to a specified DateTimeOffset instance.
    /// </summary>
    /// <param name="dateTimeOffset">The date instance to begin the comparison from.</param>
    /// <param name="targetDateTime">The date instance to compare against.</param>
    /// <param name="formats">An object dictating how the output string should be formatted.</param>
    /// <param name="ci">The culture information for localisation.</param>
    /// <returns>A string characterising the calendar time difference.</returns>
    public static string CalendarTime(this DateTimeOffset dateTimeOffset, DateTimeOffset targetDateTime,
        CalendarTimeFormats? formats = null, CultureInfo? ci = null)
    {
        formats ??= new CalendarTimeFormats(ci);
        var startDate = dateTimeOffset.ToLocalTime();
        var endDate = targetDateTime.ToLocalTime();
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

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static double UnixTimestampInSeconds(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).UnixTimestampInSeconds();

    /// <summary>
    /// Gets the total number of seconds elapsed since the UNIX epoch.
    /// </summary>
    /// <param name="dateTimeOffset">The DateTimeOffset instance to measure from the UNIX epoch.</param>
    /// <returns>A double value representing the total seconds.</returns>
    public static double UnixTimestampInSeconds(this DateTimeOffset dateTimeOffset)
    {
        var timeSpan = dateTimeOffset.ToUniversalTime() - UnixEpoch;
        return timeSpan.TotalSeconds;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static double UnixTimestampInMilliseconds(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).UnixTimestampInMilliseconds();

    /// <summary>
    /// Gets the total number of milliseconds elapsed since the UNIX epoch.
    /// </summary>
    /// <param name="dateTimeOffset">The DateTimeOffset instance to measure from the UNIX epoch.</param>
    /// <returns>A double value representing the total milliseconds.</returns>
    public static double UnixTimestampInMilliseconds(this DateTimeOffset dateTimeOffset)
    {
        var timeSpan = dateTimeOffset.ToUniversalTime() - UnixEpoch;
        return timeSpan.TotalMilliseconds;
    }

    private static string ParseFromPastTimeSpan(TimeSpan timeSpan, CultureInfo? ci = null)
    {
        ci ??= CultureWrapper.GetDefaultCulture();
        using var lm = new LocalizationManager(ci);
        return $"{ParseTimeDifference(timeSpan, ci)} {lm.GetString("TIME_AGO")}";
    }

    private static string ParseFromFutureTimeSpan(TimeSpan timeSpan, CultureInfo? ci = null)
    {
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
            _ => throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan, "The time span provided could not be parsed.")
        };
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime FirstDateInWeek(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).FirstDateInWeek().DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the first day of the week for the given date using the current <see cref="CultureInfo"/>.
    /// </summary>
    /// <param name="dateTimeOffset">A day in the week of interest.</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the first date in the week.</returns>
    public static DateTimeOffset FirstDateInWeek(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.FirstDateInWeek(CultureInfo.CurrentCulture);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime FirstDateInWeek(this DateTime dateTime, CultureInfo cultureInfo) =>
        new DateTimeOffset(dateTime).FirstDateInWeek(cultureInfo).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the first day of the week for the given date and <see cref="CultureInfo"/>.
    /// </summary>
    /// <param name="dateTimeOffset">A day in the week of interest.</param>
    /// <param name="cultureInfo">The culture information applied to determine the first day.</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the first date in the week.</returns>
    public static DateTimeOffset FirstDateInWeek(this DateTimeOffset dateTimeOffset, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dateTimeOffset.Date;
        var diff = (int)firstDateInWeek.DayOfWeek - (int)firstDayOfWeek;
        var value = firstDateInWeek.AddDays(-(Math.Abs(diff)));
        return new DateTimeOffset(value, dateTimeOffset.Offset);
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime LastDateInWeek(this DateTime dateTime) =>
        new DateTimeOffset(dateTime).LastDateInWeek().DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the last day of the week for the given date using the current <see cref="CultureInfo"/>.
    /// </summary>
    /// <param name="dateTimeOffset">A day in the week of interest.</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the last date in the week.</returns>
    public static DateTimeOffset LastDateInWeek(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.LastDateInWeek(CultureInfo.CurrentCulture);

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static DateTime LastDateInWeek(this DateTime dateTime, CultureInfo cultureInfo) =>
        new DateTimeOffset(dateTime).LastDateInWeek(cultureInfo).DateTime.ToUniversalTime();

    /// <summary>
    /// Returns the last day of the week for the given date and culture info.
    /// </summary>
    /// <param name="dateTimeOffset">A day in the week of interest.</param>
    /// <param name="cultureInfo">The culture information applied to determine the weekly boundaries.</param>
    /// <returns>The <see cref="DateTimeOffset"/> representing the last date in the week.</returns>
    public static DateTimeOffset LastDateInWeek(this DateTimeOffset dateTimeOffset, CultureInfo cultureInfo)
    {
        var firstDayInWeek = FirstDateInWeek(dateTimeOffset, cultureInfo);
        return firstDayInWeek.AddDays(6);
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static string Format(this DateTime dateTime, string? format = null, CultureInfo? cultureInfo = null) =>
        new DateTimeOffset(dateTime).Format(format, cultureInfo);

    /// <summary>
    /// Returns a formatted date string. 
    /// If no format is specified, it returns an ISO-8601 string with no fractional seconds.
    /// </summary>
    /// <param name="dateTimeOffset">The DateTimeOffset instance.</param>
    /// <param name="format">An optional format string.</param>
    /// <param name="cultureInfo">The culture information for formatting.</param>
    /// <returns>The formatted date string.</returns>
    public static string Format(this DateTimeOffset dateTimeOffset, string? format = null, CultureInfo? cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTimeOffset.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Adds the specified number of business days (Monday-Friday) to the date.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date and time.</param>
    /// <param name="businessDays">The number of business days to add (can be negative).</param>
    /// <returns>A <see cref="DateTimeOffset"/> adjusted by the number of business days.</returns>
    public static DateTimeOffset AddBusinessDays(this DateTimeOffset dateTimeOffset, int businessDays)
    {
        var direction = businessDays < 0 ? -1 : 1;
        var absBusinessDays = Math.Abs(businessDays);
        var result = dateTimeOffset;

        while (absBusinessDays > 0)
        {
            result = result.AddDays(direction);
            if (!result.IsWeekend())
            {
                absBusinessDays--;
            }
        }

        return result;
    }
}