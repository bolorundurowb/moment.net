using System;
using System.Globalization;

namespace MomentNet.Manipulate;

public static class StartEndOfExtensions
{
    /// <summary>
    /// Returns the start of the year, month, week, day or hour for the given <see cref="DateTime"/>.
    /// This implementation uses the current culture.
    /// </summary>
    public static DateTime StartOf(this DateTime This, DateTimeAnchor timeAnchor) => This.StartOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the start of the year, month, week, day or hour for the given <see cref="DateTime"/>.
    /// </summary>
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
                var tmp = FirstDateInWeek(This, cultureInfo);
                return new DateTime(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.IsoWeek:
                var isoWeekStart = This.Date.AddDays(1 - GetIsoDayOfWeek(This));
                return new DateTime(isoWeekStart.Year, isoWeekStart.Month, isoWeekStart.Day, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Month:
                return new DateTime(This.Year, This.Month, 1, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Quarter:
                return new DateTime(This.Year, GetStartOfQuarterMonth(This.Month), 1, 0, 0, 0, 0, This.Kind);
            case DateTimeAnchor.Year:
                return new DateTime(This.Year, 1, 1, 0, 0, 0, 0, This.Kind);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    /// <summary>
    /// Returns the end of a year, month, week, day or hour for the given <see cref="DateTime"/>.
    /// This implementation uses the current culture.
    /// </summary>
    public static DateTime EndOf(this DateTime This, DateTimeAnchor timeAnchor) => This.EndOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the end of a year, month, week, day or hour for the given <see cref="DateTime"/>.
    /// </summary>
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
                var tmp = LastDateInWeek(This, cultureInfo);
                return new DateTime(tmp.Year, tmp.Month, tmp.Day, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.IsoWeek:
                var isoWeekEnd = This.Date.AddDays(7 - GetIsoDayOfWeek(This));
                return new DateTime(isoWeekEnd.Year, isoWeekEnd.Month, isoWeekEnd.Day, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Month:
                var days = DateTime.DaysInMonth(This.Year, This.Month);
                return new DateTime(This.Year, This.Month, days, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Quarter:
                var endMonth = GetStartOfQuarterMonth(This.Month) + 2;
                var daysInQuarterEndMonth = DateTime.DaysInMonth(This.Year, endMonth);
                return new DateTime(This.Year, endMonth, daysInQuarterEndMonth, 23, 59, 59, 999, This.Kind);
            case DateTimeAnchor.Year:
                return new DateTime(This.Year, 12, DateTime.DaysInMonth(This.Year, 12), 23, 59, 59, 999, This.Kind);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    /// <summary>
    /// Returns the start of the year, month, week, day, hour, or minute for the given <see cref="DateTimeOffset"/>.
    /// Uses the current culture for week calculations.
    /// </summary>
    public static DateTimeOffset StartOf(this DateTimeOffset This, DateTimeAnchor timeAnchor) =>
        This.StartOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the start of the year, month, week, day, hour, or minute for the given <see cref="DateTimeOffset"/>.
    /// The offset of the original value is preserved.
    /// </summary>
    public static DateTimeOffset StartOf(this DateTimeOffset This, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, This.Minute, 0, 0, This.Offset);
            case DateTimeAnchor.Hour:
                return new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, 0, 0, 0, This.Offset);
            case DateTimeAnchor.Day:
                return new DateTimeOffset(This.Year, This.Month, This.Day, 0, 0, 0, 0, This.Offset);
            case DateTimeAnchor.Week:
                var tmp = FirstDateInWeek(This, cultureInfo);
                return new DateTimeOffset(tmp.Year, tmp.Month, tmp.Day, 0, 0, 0, 0, This.Offset);
            case DateTimeAnchor.IsoWeek:
                var isoWeekStart = This.Date.AddDays(1 - GetIsoDayOfWeek(This.DateTime));
                return new DateTimeOffset(isoWeekStart.Year, isoWeekStart.Month, isoWeekStart.Day, 0, 0, 0, 0, This.Offset);
            case DateTimeAnchor.Month:
                return new DateTimeOffset(This.Year, This.Month, 1, 0, 0, 0, 0, This.Offset);
            case DateTimeAnchor.Quarter:
                return new DateTimeOffset(This.Year, GetStartOfQuarterMonth(This.Month), 1, 0, 0, 0, 0, This.Offset);
            case DateTimeAnchor.Year:
                return new DateTimeOffset(This.Year, 1, 1, 0, 0, 0, 0, This.Offset);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    /// <summary>
    /// Returns the end of the year, month, week, day, hour, or minute for the given <see cref="DateTimeOffset"/>.
    /// Uses the current culture for week calculations.
    /// </summary>
    public static DateTimeOffset EndOf(this DateTimeOffset This, DateTimeAnchor timeAnchor) =>
        This.EndOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the end of the year, month, week, day, hour, or minute for the given <see cref="DateTimeOffset"/>.
    /// The offset of the original value is preserved.
    /// </summary>
    public static DateTimeOffset EndOf(this DateTimeOffset This, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, This.Minute, 59, 999, This.Offset);
            case DateTimeAnchor.Hour:
                return new DateTimeOffset(This.Year, This.Month, This.Day, This.Hour, 59, 59, 999, This.Offset);
            case DateTimeAnchor.Day:
                return new DateTimeOffset(This.Year, This.Month, This.Day, 23, 59, 59, 999, This.Offset);
            case DateTimeAnchor.Week:
                var tmp = LastDateInWeek(This, cultureInfo);
                return new DateTimeOffset(tmp.Year, tmp.Month, tmp.Day, 23, 59, 59, 999, This.Offset);
            case DateTimeAnchor.IsoWeek:
                var isoWeekEnd = This.Date.AddDays(7 - GetIsoDayOfWeek(This.DateTime));
                return new DateTimeOffset(isoWeekEnd.Year, isoWeekEnd.Month, isoWeekEnd.Day, 23, 59, 59, 999, This.Offset);
            case DateTimeAnchor.Month:
                var days = DateTime.DaysInMonth(This.Year, This.Month);
                return new DateTimeOffset(This.Year, This.Month, days, 23, 59, 59, 999, This.Offset);
            case DateTimeAnchor.Quarter:
                var endMonth = GetStartOfQuarterMonth(This.Month) + 2;
                var daysInQuarterEndMonth = DateTime.DaysInMonth(This.Year, endMonth);
                return new DateTimeOffset(This.Year, endMonth, daysInQuarterEndMonth, 23, 59, 59, 999, This.Offset);
            case DateTimeAnchor.Year:
                return new DateTimeOffset(This.Year, 12, DateTime.DaysInMonth(This.Year, 12), 23, 59, 59, 999, This.Offset);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    private static DateTime FirstDateInWeek(DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dayInWeek.Date;
        var diff = (int)firstDateInWeek.DayOfWeek - (int)firstDayOfWeek;
        return firstDateInWeek.AddDays(-(Math.Abs(diff)));
    }

    private static DateTime LastDateInWeek(DateTime dayInWeek, CultureInfo cultureInfo) =>
        FirstDateInWeek(dayInWeek, cultureInfo).AddDays(6);

    private static DateTimeOffset FirstDateInWeek(DateTimeOffset dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var localDate = dayInWeek.Date;
        var diff = (int)localDate.DayOfWeek - (int)firstDayOfWeek;
        var firstDay = localDate.AddDays(-(Math.Abs(diff)));
        return new DateTimeOffset(firstDay.Year, firstDay.Month, firstDay.Day, 0, 0, 0, 0, dayInWeek.Offset);
    }

    private static DateTimeOffset LastDateInWeek(DateTimeOffset dayInWeek, CultureInfo cultureInfo) =>
        FirstDateInWeek(dayInWeek, cultureInfo).AddDays(6);

    private static int GetStartOfQuarterMonth(int month) => ((month - 1) / 3 * 3) + 1;

    private static int GetIsoDayOfWeek(DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)dateTime.DayOfWeek;
}