using System;
using moment.net.Models;
using System.Globalization;
using moment.net.Enums;

namespace moment.net;

public static class RelativeTime
{
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
    private const double DaysInAMonth = DaysInAYear / 12;

    /// <summary>
    /// Return the <see cref="DateTime"/> for the next <see cref="DayOfWeek"/> supplied
    /// </summary>
    /// <param name="dayOfWeek">The day of the week whose date is to be returned</param>
    /// <returns>The <see cref="DateTime"/> for the next <see cref="DayOfWeek"/> supplied</returns>
    public static DateTime Next(this DateTime This, DayOfWeek dayOfWeek)
    {
        if (This.DayOfWeek == dayOfWeek)
            This = This.AddDays(1);

        while (This.DayOfWeek != dayOfWeek)
        {
            This = This.AddDays(1);
        }

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTime"/> for the nth next <see cref="DayOfWeek"/> supplied
    /// </summary>
    /// <param name="dayOfWeek">The day of the week whose date is to be returned</param>
    /// <param name="count">A number indicating the nth next</param>
    /// <returns>The <see cref
    public static DateTime Next(this DateTime This, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
        {
            This = This.Next(dayOfWeek);
        }

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTime"/> for the previous <see cref="DayOfWeek"/> supplied
    /// </summary>
    /// <param name="dayOfWeek">The <see cref="DayOfWeek"/> whose date is to be returned</param>
    /// <returns>The <see cref="DateTime"/> for the previous <see cref="DayOfWeek"/> supplied</returns>
    public static DateTime Last(this DateTime This, DayOfWeek dayOfWeek)
    {
        if (This.DayOfWeek == dayOfWeek)
            This = This.AddDays(-1);

        while (This.DayOfWeek != dayOfWeek)
        {
            This = This.AddDays(-1);
        }

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTime"/> for the previous <see cref="DayOfWeek"/> supplied
    /// </summary>
    /// <param name="dayOfWeek">The <see cref="DayOfWeek"/> whose date is to be returned</param>
    /// <param name="count">A number indicating the nth previous day of the week</param>
    /// <returns>The <see cref="DateTime"/> for the previous <see cref="DayOfWeek"/> supplied</returns>
    public static DateTime Last(this DateTime This, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
        {
            This = This.Last(dayOfWeek);
        }

        return This;
    }


    public static FinalDays Final(this DateTime This)
    {
        return new FinalDays(This);
    }

    /// <summary>
    /// Returns the start of the year, month, week, day or hour for the given  <see cref="DateTime"/>
    /// This implementation uses the current culture
    /// </summary>
    /// <param name="timeAnchor">Anchors the returned value to a starting point (year/month/week/day/hour)</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when an invalid  value is casted to timeAnchor</exception>
    public static DateTime StartOf(this DateTime This, DateTimeAnchor timeAnchor)
    {
        return This.StartOf(timeAnchor, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns the start of the year, month, week, day or hour for the given  <see cref="DateTime"/>
    /// This implementation requires <see cref="CultureInfo"/> to be provided
    /// </summary>
    /// <param name="timeAnchor">Anchors the returned value to a starting point (year/month/week/day/hour)</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when an invalid  value is casted to <see cref="DateTimeAnchor"/>r</exception>
    public static DateTime StartOf(this DateTime This, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTime(This.Year, This.Month, This.Day, This.Hour, This.Minute, 0, 0, This.Kind);
            case DateTimeAnchor.Hour:
                return new DateTime(This.Year, This.Month, This.Day, This.Hour, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Day:
                return new DateTime(This.Year, This.Month, This.Day, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Week:
                var tmp = This.FirstDateInWeek(cultureInfo);
                return new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Month:
                return new DateTime(This.Year, This.Month, 1, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Year:
                return new DateTime(This.Year, 1, 1, 0, 0, 0, 0, This.Kind);
            default:
                throw new ArgumentException();
        }
    }

    /// <summary>
    /// Returns the end of a year, month, week, day or hour for the given  <see cref="DateTime"/>
    /// This implementation uses the current culture
    /// </summary>
    /// <param name="timeAnchor">Anchors the returned value to a starting point (year/month/week/day/hour)</param>
    /// <returns></returns>
    /// <exception cref="InvalidCastException">Thrown when an invalid value is casted to <see cref="DateTimeAnchor"/></exception>
    public static DateTime EndOf(this DateTime This, DateTimeAnchor timeAnchor)
    {
        return This.EndOf(timeAnchor, CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns the end of a year, month, week, day or hour for the given  <see cref="DateTime"/>
    /// This implementation requires a <see cref="CultureInfo"/> to be provided
    /// </summary>
    /// <param name="timeAnchor">Anchors the returned value to a starting point (year/month/week/day/hour)</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown when an invalid value is casted to timeAnchor</exception>
    public static DateTime EndOf(this DateTime This, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTime(This.Year, This.Month, This.Day, This.Hour, This.Minute, 59, 999, This.Kind);
            case DateTimeAnchor.Hour:
                return new DateTime(This.Year, This.Month, This.Day, This.Hour, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Day:
                return new DateTime(This.Year, This.Month, This.Day, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Week:
                var tmp = This.LastDateInWeek(cultureInfo);
                return new DateTime(tmp.Year, tmp.Month, tmp.Day, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Month:
                var days = DateTime.DaysInMonth(This.Year, This.Month);
                return new DateTime(This.Year, This.Month, days, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Year:
                return new DateTime(This.Year, 12, DateTime.DaysInMonth(This.Year, 12), 23, 59, 59, 999, This.Kind);
            default:
                throw new ArgumentException();
        }
    }

    /// <summary>
    /// Get the relative time from a given date time to the current time
    /// </summary>
    /// <param name="This">A time frame in the past</param>
    /// <returns>A string representing the time span in human readable format</returns>
    public static string FromNow(this DateTime This)
    {
        return This.Kind == DateTimeKind.Utc
            ? ParseFromPastTimeSpan(DateTime.UtcNow - This)
            : ParseFromPastTimeSpan(DateTime.Now - This);
    }

    /// <summary>
    /// Get the relative time from a given date time to another date time instance
    /// </summary>
    /// <param name="This">A time frame in the past</param>
    /// <param name="dateTime">A time frame sometime after the one being compared to</param>
    /// <returns>A string representing the time span in human readable format</returns>
    public static string From(this DateTime This, DateTime dateTime)
    {
        var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return ParseFromPastTimeSpan(endDate - startDate);
    }

    /// <summary>
    /// Get the relative time from the current date time instance to a time frame in the future
    /// </summary>
    /// <param name="This">A time frame in the future</param>
    /// <returns>A string representing the time span in human readable format</returns>
    public static string ToNow(this DateTime This)
    {
        return This.Kind == DateTimeKind.Utc
            ? ParseFromFutureTimeSpan(This - DateTime.UtcNow)
            : ParseFromFutureTimeSpan(This - DateTime.Now);
    }

    /// <summary>
    /// Get the relative time from the a date time instance to a time frame in the future
    /// </summary>
    /// <param name="This">A time frame to be compared to</param>
    /// <param name="dateTime">A time frame in the future</param>
    /// <returns>A string representing the time span in human readable format</returns>
    public static string To(this DateTime This, DateTime dateTime)
    {
        var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
        return ParseFromFutureTimeSpan(endDate - startDate);
    }

    /// <summary>
    /// Get the calendar time description from this DateTime instance to the current time
    /// </summary>
    /// <param name="This">The date instance which to compare with the current date</param>
    /// <param name="formats">An object describing how the output string should be displayed</param>
    /// <returns></returns>
    public static string CalendarTime(this DateTime This, CalendarTimeFormats formats = null)
    {
        return CalendarTime(This, DateTime.Now, formats);
    }

    /// <summary>
    /// Get the calendar time description from this DateTime instance to a specified DateTime instance
    /// </summary>
    /// <param name="This">The date instance which to start comparison from</param>
    /// <param name="dateTime">The date instance to compare to</param>
    /// <param name="formats">An object describing how the output string should be displayed</param>
    /// <returns></returns>
    public static string CalendarTime(this DateTime This, DateTime dateTime,
        CalendarTimeFormats formats = null)
    {
        formats = formats ?? new CalendarTimeFormats();
        var startDate = This.Kind == DateTimeKind.Local ? This : This.ToLocalTime();
        var endDate = dateTime.Kind == DateTimeKind.Local ? dateTime : dateTime.ToLocalTime();
        var timeDiff = endDate - startDate;

        if (startDate.Date == endDate.Date)
        {
            return endDate.ToString(formats.SameDay);
        }

        if (startDate.AddDays(1).Date == endDate.Date)
        {
            return endDate.ToString(formats.NextDay);
        }

        if (startDate.AddDays(-1).Date == endDate.Date)
        {
            return endDate.ToString(formats.LastDay);
        }

        if (timeDiff.TotalDays > 1 && timeDiff.TotalDays < 7)
        {
            return endDate.ToString(formats.NextWeek);
        }

        if (timeDiff.TotalDays >= -6 && timeDiff.TotalDays < -1)
        {
            return endDate.ToString(formats.LastWeek);
        }

        return endDate.ToString(formats.EverythingElse);
    }

    /// <summary>
    /// Get the total number of seconds since the unix epoch
    /// </summary>
    /// <param name="This">DateTime instance to compare the unix epoch to</param>
    /// <returns>A double value indicating the number of seconds</returns>
    public static double UnixTimestampInSeconds(this DateTime This)
    {
        var dateInstance = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var timeSpan = dateInstance - UnixEpoch;
        return timeSpan.TotalSeconds;
    }

    /// <summary>
    /// Get the total number of milliseconds since the unix epoch
    /// </summary>
    /// <param name="This">DateTime instance to compare the unix epoch to</param>
    /// <returns>A double value indicating the number of milliseconds</returns>
    public static double UnixTimestampInMilliseconds(this DateTime This)
    {
        var dateInstance = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
        var timeSpan = dateInstance - UnixEpoch;
        return timeSpan.TotalMilliseconds;
    }

    private static string ParseFromPastTimeSpan(TimeSpan timeSpan)
    {
        return $"{ParseTimeDifference(timeSpan)} ago";
    }

    private static string ParseFromFutureTimeSpan(TimeSpan timeSpan)
    {
        return $"in {ParseTimeDifference(timeSpan)}";
    }

    private static string ParseTimeDifference(TimeSpan timeSpan)
    {
        var totalTimeInSeconds = timeSpan.TotalSeconds;

        if (totalTimeInSeconds <= 44.0)
        {
            return "few seconds";
        }

        if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
        {
            return "one minute";
        }

        var totalTimeInMinutes = timeSpan.TotalMinutes;

        if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
        {
            return $"{Math.Round(totalTimeInMinutes)} minutes";
        }

        if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
        {
            return "one hour";
        }

        var totalTimeInHours = timeSpan.TotalHours;

        if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
        {
            return $"{Math.Round(totalTimeInHours)} hours";
        }

        if (totalTimeInHours > 21 && totalTimeInHours <= 35)
        {
            return "one day";
        }

        var totalTimeInDays = timeSpan.TotalDays;

        if (totalTimeInHours > 35 && totalTimeInDays <= 25)
        {
            return $"{Math.Round(totalTimeInDays)} days";
        }

        if (totalTimeInDays > 25 && totalTimeInDays <= 45)
        {
            return "one month";
        }

        if (totalTimeInDays > 45 && totalTimeInDays <= 319)
        {
            return $"{Math.Round(totalTimeInDays / DaysInAMonth)} months";
        }

        if (totalTimeInDays > 319 && totalTimeInDays <= 547)
        {
            return "one year";
        }

        if (totalTimeInDays > 547)
        {
            return $"{Math.Round(totalTimeInDays / DaysInAYear)} years";
        }

        throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
            "in The time span sent could not be parsed.");
    }

    /// <summary>
    /// Returns the first day of the week for the given date and the current <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="dayInWeek">A day in the week of interest</param>
    /// <returns></returns>
    public static DateTime FirstDateInWeek(this DateTime dayInWeek)
    {
        return dayInWeek.FirstDateInWeek(CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns the first day of the week for the given date and <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="dayInWeek">A day in the week of interest</param>
    /// <param name="cultureInfo">The culture information</param>
    /// <returns></returns>
    public static DateTime FirstDateInWeek(this DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dayInWeek.Date;
        var diff = (int) firstDateInWeek.DayOfWeek - (int) firstDayOfWeek;
        var value = firstDateInWeek.AddDays(-(Math.Abs(diff)));
        return value;
    }

    /// <summary>
    /// Returns the last day of the week for the given date and current <see cref="CultureInfo"/>
    /// </summary>
    /// <param name="dayInWeek">A day in the week of interest</param>
    /// <returns>The date of the last day in a week</returns>
    public static DateTime LastDateInWeek(this DateTime dayInWeek)
    {
        return dayInWeek.LastDateInWeek(CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Returns the last day of the week for the given date and culture info
    /// The returned last day of the week will vary based on the supplied culture info
    /// </summary>
    /// <param name="dayInWeek">A day in the week of interest</param>
    /// <param name="cultureInfo">The culture information to be formatted with</param>
    /// <returns>The date of the last day in a week</returns>
    public static DateTime LastDateInWeek(this DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayInWeek = FirstDateInWeek(dayInWeek, cultureInfo);
        return firstDayInWeek.AddDays(6);
    }

    /// <summary>
    /// Returns a formatted date string
    /// If no format is specified it returns an ISO-8601 string with no fractional seconds
    /// </summary>
    /// <param name="dateTime">DateTime instance</param>
    /// <param name="format">Optional format string</param>
    /// <param name="cultureInfo">The culture information to be formatted with</param>
    /// <returns>The formatted date string</returns>
    public static string Format(this DateTime dateTime, string format = null, CultureInfo cultureInfo = null)
    {
        format = string.IsNullOrEmpty(format) ? "yyyy-MM-ddTHH:mm:sszzz" : format;
        return dateTime.ToString(format, cultureInfo ?? CultureInfo.CurrentCulture);
    }
}