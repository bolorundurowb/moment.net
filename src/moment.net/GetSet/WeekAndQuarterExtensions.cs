using System;
using System.Globalization;

namespace MomentNet.GetSet;

public static class WeekAndQuarterExtensions
{
    /// <summary>
    /// Returns the calendar quarter for the date as a value from 1 to 4.
    /// </summary>
    /// <param name="dateTime">The date whose quarter should be returned.</param>
    /// <returns>The calendar quarter, from 1 through 4.</returns>
    public static int Quarter(this DateTime dateTime) => GetQuarter(dateTime.Month);

    /// <summary>
    /// Returns the culture-specific week of year for the date using the current culture.
    /// </summary>
    /// <param name="dateTime">The date whose week number should be returned.</param>
    /// <returns>The culture-specific week of year.</returns>
    public static int Week(this DateTime dateTime) => dateTime.Week(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the culture-specific week of year for the date.
    /// </summary>
    /// <param name="dateTime">The date whose week number should be returned.</param>
    /// <param name="cultureInfo">The culture whose calendar week rule and first day of week should be used.</param>
    /// <returns>The culture-specific week of year.</returns>
    public static int Week(this DateTime dateTime, CultureInfo cultureInfo) =>
        cultureInfo.Calendar.GetWeekOfYear(dateTime, cultureInfo.DateTimeFormat.CalendarWeekRule,
            cultureInfo.DateTimeFormat.FirstDayOfWeek);

    /// <summary>
    /// Returns the ISO-8601 week number for the date.
    /// </summary>
    /// <param name="dateTime">The date whose ISO week number should be returned.</param>
    /// <returns>The ISO-8601 week number.</returns>
    public static int IsoWeek(this DateTime dateTime)
    {
        var thursday = dateTime.Date.AddDays(4 - GetIsoDayOfWeek(dateTime));
        return ((thursday.DayOfYear - 1) / 7) + 1;
    }

    /// <summary>
    /// Returns the ISO-8601 week-numbering year for the date.
    /// </summary>
    /// <param name="dateTime">The date whose ISO week-numbering year should be returned.</param>
    /// <returns>The ISO-8601 week-numbering year.</returns>
    public static int IsoWeekYear(this DateTime dateTime) =>
        dateTime.Date.AddDays(4 - GetIsoDayOfWeek(dateTime)).Year;

    /// <summary>
    /// Returns the first date in the week that contains the date using the current culture.
    /// </summary>
    /// <param name="dateTime">A date in the week to inspect.</param>
    /// <returns>The first date in the week.</returns>
    public static DateTime FirstDateInWeek(this DateTime dateTime) => dateTime.FirstDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the first date in the week that contains the date using the supplied culture.
    /// </summary>
    /// <param name="dateTime">A date in the week to inspect.</param>
    /// <param name="cultureInfo">The culture whose first day of week should be used.</param>
    /// <returns>The first date in the week.</returns>
    public static DateTime FirstDateInWeek(this DateTime dateTime, CultureInfo cultureInfo)
    {
        ValidateDateTime(dateTime, nameof(dateTime));
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dateTime.Date;
        var daysSinceFirstDayOfWeek = GetDaysSinceFirstDayOfWeek(firstDateInWeek.DayOfWeek, firstDayOfWeek);
        return AddDaysChecked(firstDateInWeek, -daysSinceFirstDayOfWeek, nameof(dateTime));
    }

    /// <summary>
    /// Returns the last date in the week that contains the date using the current culture.
    /// </summary>
    /// <param name="dateTime">A date in the week to inspect.</param>
    /// <returns>The last date in the week.</returns>
    public static DateTime LastDateInWeek(this DateTime dateTime) => dateTime.LastDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the last date in the week that contains the date using the supplied culture.
    /// </summary>
    /// <param name="dateTime">A date in the week to inspect.</param>
    /// <param name="cultureInfo">The culture whose first day of week should be used.</param>
    /// <returns>The last date in the week.</returns>
    public static DateTime LastDateInWeek(this DateTime dateTime, CultureInfo cultureInfo)
    {
        ValidateDateTime(dateTime, nameof(dateTime));
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var date = dateTime.Date;
        var daysSinceFirstDayOfWeek = GetDaysSinceFirstDayOfWeek(date.DayOfWeek, firstDayOfWeek);
        return AddDaysChecked(date, 6 - daysSinceFirstDayOfWeek, nameof(dateTime));
    }

    /// <summary>
    /// Returns the calendar quarter for the offset-aware date as a value from 1 to 4.
    /// </summary>
    /// <param name="dateTimeOffset">The date whose quarter should be returned.</param>
    /// <returns>The calendar quarter, from 1 through 4.</returns>
    public static int Quarter(this DateTimeOffset dateTimeOffset) => GetQuarter(dateTimeOffset.Month);

    /// <summary>
    /// Returns the culture-specific week of year for the offset-aware date using the current culture.
    /// </summary>
    /// <param name="dateTimeOffset">The date whose week number should be returned.</param>
    /// <returns>The culture-specific week of year.</returns>
    public static int Week(this DateTimeOffset dateTimeOffset) => dateTimeOffset.Week(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the culture-specific week of year for the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date whose week number should be returned.</param>
    /// <param name="cultureInfo">The culture whose calendar week rule and first day of week should be used.</param>
    /// <returns>The culture-specific week of year.</returns>
    public static int Week(this DateTimeOffset dateTimeOffset, CultureInfo cultureInfo) =>
        cultureInfo.Calendar.GetWeekOfYear(dateTimeOffset.DateTime, cultureInfo.DateTimeFormat.CalendarWeekRule,
            cultureInfo.DateTimeFormat.FirstDayOfWeek);

    /// <summary>
    /// Returns the ISO-8601 week number for the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date whose ISO week number should be returned.</param>
    /// <returns>The ISO-8601 week number.</returns>
    public static int IsoWeek(this DateTimeOffset dateTimeOffset) => dateTimeOffset.DateTime.IsoWeek();

    /// <summary>
    /// Returns the ISO-8601 week-numbering year for the offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date whose ISO week-numbering year should be returned.</param>
    /// <returns>The ISO-8601 week-numbering year.</returns>
    public static int IsoWeekYear(this DateTimeOffset dateTimeOffset) => dateTimeOffset.DateTime.IsoWeekYear();

    /// <summary>
    /// Returns the first date in the week that contains the offset-aware date using the current culture.
    /// </summary>
    /// <param name="dateTimeOffset">A date in the week to inspect.</param>
    /// <returns>The first date in the week, preserving the original offset.</returns>
    public static DateTimeOffset FirstDateInWeek(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.FirstDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the first date in the week that contains the offset-aware date using the supplied culture.
    /// </summary>
    /// <param name="dateTimeOffset">A date in the week to inspect.</param>
    /// <param name="cultureInfo">The culture whose first day of week should be used.</param>
    /// <returns>The first date in the week, preserving the original offset.</returns>
    public static DateTimeOffset FirstDateInWeek(this DateTimeOffset dateTimeOffset, CultureInfo cultureInfo)
    {
        ValidateDateTimeOffset(dateTimeOffset, nameof(dateTimeOffset));
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var localDate = dateTimeOffset.Date;
        var daysSinceFirstDayOfWeek = GetDaysSinceFirstDayOfWeek(localDate.DayOfWeek, firstDayOfWeek);
        var firstDay = AddDaysChecked(localDate, -daysSinceFirstDayOfWeek, nameof(dateTimeOffset));
        return new DateTimeOffset(firstDay.Year, firstDay.Month, firstDay.Day,
            0, 0, 0, 0, dateTimeOffset.Offset);
    }

    /// <summary>
    /// Returns the last date in the week that contains the offset-aware date using the current culture.
    /// </summary>
    /// <param name="dateTimeOffset">A date in the week to inspect.</param>
    /// <returns>The last date in the week, preserving the original offset.</returns>
    public static DateTimeOffset LastDateInWeek(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.LastDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the last date in the week that contains the offset-aware date using the supplied culture.
    /// </summary>
    /// <param name="dateTimeOffset">A date in the week to inspect.</param>
    /// <param name="cultureInfo">The culture whose first day of week should be used.</param>
    /// <returns>The last date in the week, preserving the original offset.</returns>
    public static DateTimeOffset LastDateInWeek(this DateTimeOffset dateTimeOffset, CultureInfo cultureInfo)
    {
        ValidateDateTimeOffset(dateTimeOffset, nameof(dateTimeOffset));
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var localDate = dateTimeOffset.Date;
        var daysSinceFirstDayOfWeek = GetDaysSinceFirstDayOfWeek(localDate.DayOfWeek, firstDayOfWeek);
        var lastDay = AddDaysChecked(localDate, 6 - daysSinceFirstDayOfWeek, nameof(dateTimeOffset));
        return new DateTimeOffset(lastDay.Year, lastDay.Month, lastDay.Day,
            0, 0, 0, 0, dateTimeOffset.Offset);
    }

    private static int GetQuarter(int month) => ((month - 1) / 3) + 1;

    private static int GetDaysSinceFirstDayOfWeek(DayOfWeek dayOfWeek, DayOfWeek firstDayOfWeek) =>
        ((int)dayOfWeek - (int)firstDayOfWeek + 7) % 7;

    private static void ValidateDateTime(DateTime dateTime, string parameterName)
    {
        if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, "DateTime value is out of range.");
        }
    }

    private static void ValidateDateTimeOffset(DateTimeOffset dateTimeOffset, string parameterName)
    {
        if (dateTimeOffset == DateTimeOffset.MinValue || dateTimeOffset == DateTimeOffset.MaxValue)
        {
            throw new ArgumentOutOfRangeException(parameterName, "DateTimeOffset value is out of range.");
        }
    }

    private static DateTime AddDaysChecked(DateTime dateTime, int days, string parameterName)
    {
        if (days < 0)
        {
            var minDate = GetMinDate(dateTime.Kind);

            if (dateTime < minDate.AddDays(-days))
            {
                throw new ArgumentOutOfRangeException(parameterName, "The first date in the week is outside the supported range.");
            }
        }

        if (days > 0)
        {
            var maxDate = GetMaxDate(dateTime.Kind);

            if (dateTime > maxDate.AddDays(-days))
            {
                throw new ArgumentOutOfRangeException(parameterName, "The last date in the week is outside the supported range.");
            }
        }

        return dateTime.AddDays(days);
    }

    private static DateTime GetMinDate(DateTimeKind kind) =>
        new(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 0, 0, 0, 0, kind);

    private static DateTime GetMaxDate(DateTimeKind kind) =>
        new(DateTime.MaxValue.Year, DateTime.MaxValue.Month, DateTime.MaxValue.Day, 0, 0, 0, 0, kind);

    private static int GetIsoDayOfWeek(DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)dateTime.DayOfWeek;
}
