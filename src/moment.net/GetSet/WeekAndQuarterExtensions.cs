using System;
using System.Globalization;

namespace MomentNet.GetSet;

public static class WeekAndQuarterExtensions
{
    /// <summary>
    /// Returns the calendar quarter for the given <see cref="DateTime"/> as a value from 1 to 4.
    /// </summary>
    public static int Quarter(this DateTime This) => GetQuarter(This.Month);

    /// <summary>
    /// Returns the culture-specific week of year for the given <see cref="DateTime"/>.
    /// </summary>
    public static int Week(this DateTime This) => This.Week(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the culture-specific week of year for the given <see cref="DateTime"/>.
    /// </summary>
    public static int Week(this DateTime This, CultureInfo cultureInfo) =>
        cultureInfo.Calendar.GetWeekOfYear(This, cultureInfo.DateTimeFormat.CalendarWeekRule,
            cultureInfo.DateTimeFormat.FirstDayOfWeek);

    /// <summary>
    /// Returns the ISO-8601 week number for the given <see cref="DateTime"/>.
    /// </summary>
    public static int IsoWeek(this DateTime This)
    {
        var thursday = This.Date.AddDays(4 - GetIsoDayOfWeek(This));
        return ((thursday.DayOfYear - 1) / 7) + 1;
    }

    /// <summary>
    /// Returns the ISO-8601 week-numbering year for the given <see cref="DateTime"/>.
    /// </summary>
    public static int IsoWeekYear(this DateTime This) =>
        This.Date.AddDays(4 - GetIsoDayOfWeek(This)).Year;

    /// <summary>
    /// Returns the first day of the week for the given date and the current <see cref="CultureInfo"/>
    /// </summary>
    public static DateTime FirstDateInWeek(this DateTime dayInWeek) => dayInWeek.FirstDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the first day of the week for the given date and <see cref="CultureInfo"/>
    /// </summary>
    public static DateTime FirstDateInWeek(this DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dayInWeek.Date;
        var diff = (int)firstDateInWeek.DayOfWeek - (int)firstDayOfWeek;
        var value = firstDateInWeek.AddDays(-(Math.Abs(diff)));
        return value;
    }

    /// <summary>
    /// Returns the last day of the week for the given date and current <see cref="CultureInfo"/>
    /// </summary>
    public static DateTime LastDateInWeek(this DateTime dayInWeek) => dayInWeek.LastDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the last day of the week for the given date and culture info.
    /// </summary>
    public static DateTime LastDateInWeek(this DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayInWeek = FirstDateInWeek(dayInWeek, cultureInfo);
        return firstDayInWeek.AddDays(6);
    }

    /// <summary>
    /// Returns the calendar quarter for the given <see cref="DateTimeOffset"/> as a value from 1 to 4.
    /// </summary>
    public static int Quarter(this DateTimeOffset This) => GetQuarter(This.Month);

    /// <summary>
    /// Returns the culture-specific week of year for the given <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Week(this DateTimeOffset This) => This.Week(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the culture-specific week of year for the given <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int Week(this DateTimeOffset This, CultureInfo cultureInfo) =>
        cultureInfo.Calendar.GetWeekOfYear(This.DateTime, cultureInfo.DateTimeFormat.CalendarWeekRule,
            cultureInfo.DateTimeFormat.FirstDayOfWeek);

    /// <summary>
    /// Returns the ISO-8601 week number for the given <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int IsoWeek(this DateTimeOffset This) => This.DateTime.IsoWeek();

    /// <summary>
    /// Returns the ISO-8601 week-numbering year for the given <see cref="DateTimeOffset"/>.
    /// </summary>
    public static int IsoWeekYear(this DateTimeOffset This) => This.DateTime.IsoWeekYear();

    /// <summary>
    /// Returns the first day of the week for the given <see cref="DateTimeOffset"/> using the current culture.
    /// The original UTC offset is preserved.
    /// </summary>
    public static DateTimeOffset FirstDateInWeek(this DateTimeOffset dayInWeek) =>
        dayInWeek.FirstDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the first day of the week for the given <see cref="DateTimeOffset"/> and <see cref="CultureInfo"/>.
    /// The original UTC offset is preserved.
    /// </summary>
    public static DateTimeOffset FirstDateInWeek(this DateTimeOffset dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var localDate = dayInWeek.Date;
        var diff = (int)localDate.DayOfWeek - (int)firstDayOfWeek;
        var firstDay = localDate.AddDays(-(Math.Abs(diff)));
        return new DateTimeOffset(firstDay.Year, firstDay.Month, firstDay.Day,
            0, 0, 0, 0, dayInWeek.Offset);
    }

    /// <summary>
    /// Returns the last day of the week for the given <see cref="DateTimeOffset"/> using the current culture.
    /// The original UTC offset is preserved.
    /// </summary>
    public static DateTimeOffset LastDateInWeek(this DateTimeOffset dayInWeek) =>
        dayInWeek.LastDateInWeek(CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the last day of the week for the given <see cref="DateTimeOffset"/> and <see cref="CultureInfo"/>.
    /// The original UTC offset is preserved.
    /// </summary>
    public static DateTimeOffset LastDateInWeek(this DateTimeOffset dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayInWeek = FirstDateInWeek(dayInWeek, cultureInfo);
        return firstDayInWeek.AddDays(6);
    }

    private static int GetQuarter(int month) => ((month - 1) / 3) + 1;

    private static int GetIsoDayOfWeek(DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)dateTime.DayOfWeek;
}