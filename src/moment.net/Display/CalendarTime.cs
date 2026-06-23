using System;
using System.Globalization;
using MomentNet.Display.Models;

namespace MomentNet.Display;

public static class CalendarTimeExtensions
{
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
        ci ??= CultureInfo.CurrentCulture;
        formats ??= new CalendarTimeFormats(ci);
        var startDate = dateTime.Kind == DateTimeKind.Local ? dateTime : dateTime.ToLocalTime();
        var endDate = comparisonDateTime.Kind == DateTimeKind.Local ? comparisonDateTime : comparisonDateTime.ToLocalTime();

        if (startDate.Date == endDate.Date)
            return startDate.ToString(formats.SameDay, ci);

        if (startDate.Date == endDate.AddDays(1).Date)
            return startDate.ToString(formats.NextDay, ci);

        if (startDate.Date == endDate.AddDays(-1).Date)
            return startDate.ToString(formats.LastDay, ci);

        if (startDate.Date >= endDate.AddDays(2).Date && startDate.Date <= endDate.AddDays(7).Date)
            return startDate.ToString(formats.NextWeek, ci);

        if (startDate.Date <= endDate.AddDays(-2).Date && startDate.Date >= endDate.AddDays(-7).Date)
            return startDate.ToString(formats.LastWeek, ci);

        return startDate.ToString(formats.EverythingElse, ci);
    }

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
        ci ??= CultureInfo.CurrentCulture;
        formats ??= new CalendarTimeFormats(ci);
        var startDate = dateTimeOffset.ToLocalTime();
        var endDate = comparisonDateTimeOffset.ToLocalTime();

        if (startDate.Date == endDate.Date)
            return startDate.ToString(formats.SameDay, ci);

        if (startDate.Date == endDate.AddDays(1).Date)
            return startDate.ToString(formats.NextDay, ci);

        if (startDate.Date == endDate.AddDays(-1).Date)
            return startDate.ToString(formats.LastDay, ci);

        if (startDate.Date >= endDate.AddDays(2).Date && startDate.Date <= endDate.AddDays(7).Date)
            return startDate.ToString(formats.NextWeek, ci);

        if (startDate.Date <= endDate.AddDays(-2).Date && startDate.Date >= endDate.AddDays(-7).Date)
            return startDate.ToString(formats.LastWeek, ci);

        return startDate.ToString(formats.EverythingElse, ci);
    }

#if NET6_0_OR_GREATER
    /// <summary>
    /// Returns a calendar-style formatted string relative to now for a <see cref="DateOnly"/>.
    /// </summary>
    public static string CalendarTime(this DateOnly dateOnly, CalendarTimeFormats? formats = null) =>
        dateOnly.ToDateTime(default).CalendarTime(formats);

    /// <summary>
    /// Returns a calendar-style formatted string relative to a reference date for a <see cref="DateOnly"/>.
    /// </summary>
    public static string CalendarTime(this DateOnly dateOnly, DateOnly comparisonDate,
        CalendarTimeFormats? formats = null, CultureInfo? ci = null) =>
        dateOnly.ToDateTime(default).CalendarTime(comparisonDate.ToDateTime(default), formats, ci);
#endif
}
