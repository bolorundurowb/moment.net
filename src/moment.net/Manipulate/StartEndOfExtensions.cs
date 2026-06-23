using System;
using System.Globalization;

namespace MomentNet.Manipulate;

public static class StartEndOfExtensions
{
    /// <summary>
    /// Returns the start of the requested period using the current culture for week calculations.
    /// </summary>
    /// <param name="dateTime">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <returns>A new <see cref="DateTime"/> at the start of the requested period.</returns>
    public static DateTime StartOf(this DateTime dateTime, DateTimeAnchor timeAnchor) => dateTime.StartOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the start of the requested period using the supplied culture for week calculations.
    /// </summary>
    /// <param name="dateTime">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <param name="cultureInfo">The culture whose week settings should be used.</param>
    /// <returns>A new <see cref="DateTime"/> at the start of the requested period.</returns>
    public static DateTime StartOf(this DateTime dateTime, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0, dateTime.Kind);
            case DateTimeAnchor.Hour:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, 0, dateTime.Kind);
            case DateTimeAnchor.Day:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0, dateTime.Kind);
            case DateTimeAnchor.Week:
                var weekStart = FirstDateInWeek(dateTime, cultureInfo);
                return new DateTime(weekStart.Year, weekStart.Month, weekStart.Day, 0, 0, 0, 0, dateTime.Kind);
            case DateTimeAnchor.IsoWeek:
                var isoWeekStart = dateTime.Date.AddDays(1 - GetIsoDayOfWeek(dateTime));
                return new DateTime(isoWeekStart.Year, isoWeekStart.Month, isoWeekStart.Day, 0, 0, 0, 0, dateTime.Kind);
            case DateTimeAnchor.Month:
                return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, 0, dateTime.Kind);
            case DateTimeAnchor.Quarter:
                return new DateTime(dateTime.Year, GetStartOfQuarterMonth(dateTime.Month), 1, 0, 0, 0, 0, dateTime.Kind);
            case DateTimeAnchor.Year:
                return new DateTime(dateTime.Year, 1, 1, 0, 0, 0, 0, dateTime.Kind);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    /// <summary>
    /// Returns the end of the requested period using the current culture for week calculations.
    /// </summary>
    /// <param name="dateTime">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <returns>A new <see cref="DateTime"/> at the end of the requested period.</returns>
    public static DateTime EndOf(this DateTime dateTime, DateTimeAnchor timeAnchor) => dateTime.EndOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the end of the requested period using the supplied culture for week calculations.
    /// </summary>
    /// <param name="dateTime">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <param name="cultureInfo">The culture whose week settings should be used.</param>
    /// <returns>A new <see cref="DateTime"/> at the end of the requested period.</returns>
    public static DateTime EndOf(this DateTime dateTime, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 59, 999, dateTime.Kind);
            case DateTimeAnchor.Hour:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 59, 59, 999, dateTime.Kind);
            case DateTimeAnchor.Day:
                return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999, dateTime.Kind);
            case DateTimeAnchor.Week:
                var weekEnd = LastDateInWeek(dateTime, cultureInfo);
                return new DateTime(weekEnd.Year, weekEnd.Month, weekEnd.Day, 23, 59, 59, 999, dateTime.Kind);
            case DateTimeAnchor.IsoWeek:
                var isoWeekEnd = dateTime.Date.AddDays(7 - GetIsoDayOfWeek(dateTime));
                return new DateTime(isoWeekEnd.Year, isoWeekEnd.Month, isoWeekEnd.Day, 23, 59, 59, 999, dateTime.Kind);
            case DateTimeAnchor.Month:
                var days = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                return new DateTime(dateTime.Year, dateTime.Month, days, 23, 59, 59, 999, dateTime.Kind);
            case DateTimeAnchor.Quarter:
                var endMonth = GetStartOfQuarterMonth(dateTime.Month) + 2;
                var daysInQuarterEndMonth = DateTime.DaysInMonth(dateTime.Year, endMonth);
                return new DateTime(dateTime.Year, endMonth, daysInQuarterEndMonth, 23, 59, 59, 999, dateTime.Kind);
            case DateTimeAnchor.Year:
                return new DateTime(dateTime.Year, 12, DateTime.DaysInMonth(dateTime.Year, 12), 23, 59, 59, 999, dateTime.Kind);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    /// <summary>
    /// Returns the start of the requested period using the current culture for week calculations.
    /// </summary>
    /// <param name="dateTimeOffset">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <returns>A new <see cref="DateTimeOffset"/> at the start of the requested period, preserving the offset.</returns>
    public static DateTimeOffset StartOf(this DateTimeOffset dateTimeOffset, DateTimeAnchor timeAnchor) =>
        dateTimeOffset.StartOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the start of the requested period using the supplied culture for week calculations.
    /// </summary>
    /// <param name="dateTimeOffset">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <param name="cultureInfo">The culture whose week settings should be used.</param>
    /// <returns>A new <see cref="DateTimeOffset"/> at the start of the requested period, preserving the offset.</returns>
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
                var weekStart = FirstDateInWeek(dateTimeOffset, cultureInfo);
                return new DateTimeOffset(weekStart.Year, weekStart.Month, weekStart.Day, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.IsoWeek:
                var isoWeekStart = dateTimeOffset.Date.AddDays(1 - GetIsoDayOfWeek(dateTimeOffset.DateTime));
                return new DateTimeOffset(isoWeekStart.Year, isoWeekStart.Month, isoWeekStart.Day, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Month:
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, 1, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Quarter:
                return new DateTimeOffset(dateTimeOffset.Year, GetStartOfQuarterMonth(dateTimeOffset.Month), 1, 0, 0, 0, 0, dateTimeOffset.Offset);
            case DateTimeAnchor.Year:
                return new DateTimeOffset(dateTimeOffset.Year, 1, 1, 0, 0, 0, 0, dateTimeOffset.Offset);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    /// <summary>
    /// Returns the end of the requested period using the current culture for week calculations.
    /// </summary>
    /// <param name="dateTimeOffset">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <returns>A new <see cref="DateTimeOffset"/> at the end of the requested period, preserving the offset.</returns>
    public static DateTimeOffset EndOf(this DateTimeOffset dateTimeOffset, DateTimeAnchor timeAnchor) =>
        dateTimeOffset.EndOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the end of the requested period using the supplied culture for week calculations.
    /// </summary>
    /// <param name="dateTimeOffset">The date to anchor.</param>
    /// <param name="timeAnchor">The period to anchor to.</param>
    /// <param name="cultureInfo">The culture whose week settings should be used.</param>
    /// <returns>A new <see cref="DateTimeOffset"/> at the end of the requested period, preserving the offset.</returns>
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
                var weekEnd = LastDateInWeek(dateTimeOffset, cultureInfo);
                return new DateTimeOffset(weekEnd.Year, weekEnd.Month, weekEnd.Day, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.IsoWeek:
                var isoWeekEnd = dateTimeOffset.Date.AddDays(7 - GetIsoDayOfWeek(dateTimeOffset.DateTime));
                return new DateTimeOffset(isoWeekEnd.Year, isoWeekEnd.Month, isoWeekEnd.Day, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Month:
                var days = DateTime.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month);
                return new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, days, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Quarter:
                var endMonth = GetStartOfQuarterMonth(dateTimeOffset.Month) + 2;
                var daysInQuarterEndMonth = DateTime.DaysInMonth(dateTimeOffset.Year, endMonth);
                return new DateTimeOffset(dateTimeOffset.Year, endMonth, daysInQuarterEndMonth, 23, 59, 59, 999, dateTimeOffset.Offset);
            case DateTimeAnchor.Year:
                return new DateTimeOffset(dateTimeOffset.Year, 12, DateTime.DaysInMonth(dateTimeOffset.Year, 12), 23, 59, 59, 999, dateTimeOffset.Offset);
            default:
                throw new ArgumentException("Invalid timeAnchor argument.");
        }
    }

    private static DateTime FirstDateInWeek(DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dayInWeek.Date;
        var diff = ((int)firstDateInWeek.DayOfWeek - (int)firstDayOfWeek + 7) % 7;
        return firstDateInWeek.AddDays(-diff);
    }

    private static DateTime LastDateInWeek(DateTime dayInWeek, CultureInfo cultureInfo) =>
        FirstDateInWeek(dayInWeek, cultureInfo).AddDays(6);

    private static DateTimeOffset FirstDateInWeek(DateTimeOffset dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var localDate = dayInWeek.Date;
        var diff = ((int)localDate.DayOfWeek - (int)firstDayOfWeek + 7) % 7;
        var firstDay = localDate.AddDays(-diff);
        return new DateTimeOffset(firstDay.Year, firstDay.Month, firstDay.Day, 0, 0, 0, 0, dayInWeek.Offset);
    }

    private static DateTimeOffset LastDateInWeek(DateTimeOffset dayInWeek, CultureInfo cultureInfo) =>
        FirstDateInWeek(dayInWeek, cultureInfo).AddDays(6);

    private static int GetStartOfQuarterMonth(int month) => ((month - 1) / 3 * 3) + 1;

    private static int GetIsoDayOfWeek(DateTime dateTime) =>
        dateTime.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)dateTime.DayOfWeek;

#if NET6_0_OR_GREATER
    /// <summary>
    /// Returns the start of the requested period for the <see cref="DateOnly"/> using the current culture for week calculations.
    /// </summary>
    public static DateOnly StartOf(this DateOnly dateOnly, DateTimeAnchor timeAnchor) =>
        dateOnly.StartOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the start of the requested period for the <see cref="DateOnly"/> using the supplied culture for week calculations.
    /// </summary>
    public static DateOnly StartOf(this DateOnly dateOnly, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Day:
                return dateOnly;
            case DateTimeAnchor.Week:
                var dateTime = dateOnly.ToDateTime(default);
                var weekStart = FirstDateInWeekDateOnly(dateTime, cultureInfo);
                return DateOnly.FromDateTime(weekStart);
            case DateTimeAnchor.IsoWeek:
                var isoDateTime = dateOnly.ToDateTime(default);
                var isoWeekStart = isoDateTime.AddDays(1 - GetIsoDayOfWeek(isoDateTime));
                return DateOnly.FromDateTime(isoWeekStart);
            case DateTimeAnchor.Month:
                return new DateOnly(dateOnly.Year, dateOnly.Month, 1);
            case DateTimeAnchor.Quarter:
                return new DateOnly(dateOnly.Year, GetStartOfQuarterMonth(dateOnly.Month), 1);
            case DateTimeAnchor.Year:
                return new DateOnly(dateOnly.Year, 1, 1);
            default:
                throw new ArgumentException("Invalid timeAnchor argument for DateOnly. Only Day, Week, IsoWeek, Month, Quarter, and Year are supported.");
        }
    }

    /// <summary>
    /// Returns the end of the requested period for the <see cref="DateOnly"/> using the current culture for week calculations.
    /// </summary>
    public static DateOnly EndOf(this DateOnly dateOnly, DateTimeAnchor timeAnchor) =>
        dateOnly.EndOf(timeAnchor, CultureInfo.CurrentCulture);

    /// <summary>
    /// Returns the end of the requested period for the <see cref="DateOnly"/> using the supplied culture for week calculations.
    /// </summary>
    public static DateOnly EndOf(this DateOnly dateOnly, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Day:
                return dateOnly;
            case DateTimeAnchor.Week:
                var dateTime = dateOnly.ToDateTime(default);
                var weekEnd = LastDateInWeekDateOnly(dateTime, cultureInfo);
                return DateOnly.FromDateTime(weekEnd);
            case DateTimeAnchor.IsoWeek:
                var isoDateTime = dateOnly.ToDateTime(default);
                var isoWeekEnd = isoDateTime.AddDays(7 - GetIsoDayOfWeek(isoDateTime));
                return DateOnly.FromDateTime(isoWeekEnd);
            case DateTimeAnchor.Month:
                var days = DateTime.DaysInMonth(dateOnly.Year, dateOnly.Month);
                return new DateOnly(dateOnly.Year, dateOnly.Month, days);
            case DateTimeAnchor.Quarter:
                var endMonth = GetStartOfQuarterMonth(dateOnly.Month) + 2;
                var daysInQuarterEndMonth = DateTime.DaysInMonth(dateOnly.Year, endMonth);
                return new DateOnly(dateOnly.Year, endMonth, daysInQuarterEndMonth);
            case DateTimeAnchor.Year:
                return new DateOnly(dateOnly.Year, 12, DateTime.DaysInMonth(dateOnly.Year, 12));
            default:
                throw new ArgumentException("Invalid timeAnchor argument for DateOnly. Only Day, Week, IsoWeek, Month, Quarter, and Year are supported.");
        }
    }

    /// <summary>
    /// Returns the start of the requested period for the <see cref="TimeOnly"/>.
    /// </summary>
    public static TimeOnly StartOf(this TimeOnly timeOnly, DateTimeAnchor timeAnchor)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new TimeOnly(timeOnly.Hour, timeOnly.Minute, 0, 0);
            case DateTimeAnchor.Hour:
                return new TimeOnly(timeOnly.Hour, 0, 0, 0);
            default:
                throw new ArgumentException("Invalid timeAnchor argument for TimeOnly. Only Minute and Hour are supported.");
        }
    }

    /// <summary>
    /// Returns the end of the requested period for the <see cref="TimeOnly"/>.
    /// </summary>
    public static TimeOnly EndOf(this TimeOnly timeOnly, DateTimeAnchor timeAnchor)
    {
        switch (timeAnchor)
        {
            case DateTimeAnchor.Minute:
                return new TimeOnly(timeOnly.Hour, timeOnly.Minute, 59, 999);
            case DateTimeAnchor.Hour:
                return new TimeOnly(timeOnly.Hour, 59, 59, 999);
            default:
                throw new ArgumentException("Invalid timeAnchor argument for TimeOnly. Only Minute and Hour are supported.");
        }
    }

    private static DateTime FirstDateInWeekDateOnly(DateTime dayInWeek, CultureInfo cultureInfo)
    {
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
        var firstDateInWeek = dayInWeek.Date;
        var diff = ((int)firstDateInWeek.DayOfWeek - (int)firstDayOfWeek + 7) % 7;
        return firstDateInWeek.AddDays(-diff);
    }

    private static DateTime LastDateInWeekDateOnly(DateTime dayInWeek, CultureInfo cultureInfo) =>
        FirstDateInWeekDateOnly(dayInWeek, cultureInfo).AddDays(6);
#endif
}