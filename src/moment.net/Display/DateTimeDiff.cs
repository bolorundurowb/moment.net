using System;

namespace MomentNet.Display;

public static class DateTimeDiff
{
    /// <summary>
    /// Returns the difference in days between two dates.
    /// </summary>
    /// <param name="dateTime">The date to compare from.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <returns>The signed difference in days.</returns>
    public static double DiffInDays(this DateTime dateTime, DateTime comparisonDateTime)
    {
        return (dateTime - comparisonDateTime).TotalDays;
    }

    /// <summary>
    /// Returns the difference in months between two dates.
    /// The fractional component is computed by dividing the day-of-month difference
    /// by the actual number of days in the source month (<paramref name="dateTime"/>).
    /// </summary>
    /// <param name="dateTime">The date to compare from.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <returns>The signed difference in months, including the fractional month component.</returns>
    public static double DiffInMonths(this DateTime dateTime, DateTime comparisonDateTime)
    {
        var months = (dateTime.Year - comparisonDateTime.Year) * 12 + dateTime.Month - comparisonDateTime.Month;
        var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        var dayDiff = (double)(dateTime.Day - comparisonDateTime.Day) / daysInMonth;
        return months + dayDiff;
    }

    /// <summary>
    /// Returns the difference in quarters between two dates.
    /// </summary>
    /// <param name="dateTime">The date to compare from.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <returns>The signed difference in quarters.</returns>
    public static double DiffInQuarters(this DateTime dateTime, DateTime comparisonDateTime)
    {
        return dateTime.DiffInMonths(comparisonDateTime) / 3.0;
    }

    /// <summary>
    /// Returns the difference in years between two dates.
    /// </summary>
    /// <param name="dateTime">The date to compare from.</param>
    /// <param name="comparisonDateTime">The date to compare against.</param>
    /// <returns>The signed difference in years.</returns>
    public static double DiffInYears(this DateTime dateTime, DateTime comparisonDateTime)
    {
        return dateTime.DiffInMonths(comparisonDateTime) / 12.0;
    }

    /// <summary>
    /// Returns the difference in days between two offset-aware dates.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare from.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <returns>The signed difference in days, comparing the underlying UTC instants.</returns>
    public static double DiffInDays(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset) =>
        (dateTimeOffset - comparisonDateTimeOffset).TotalDays;

    /// <summary>
    /// Returns the difference in months between two offset-aware dates.
    /// The fractional component is computed by dividing the day-of-month difference
    /// by the actual number of days in the source month (<paramref name="dateTimeOffset"/>).
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare from.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <returns>The signed difference in months, using each value's local date components.</returns>
    public static double DiffInMonths(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset)
    {
        var months = (dateTimeOffset.Year - comparisonDateTimeOffset.Year) * 12 + dateTimeOffset.Month - comparisonDateTimeOffset.Month;
        var daysInMonth = DateTime.DaysInMonth(dateTimeOffset.Year, dateTimeOffset.Month);
        var dayDiff = (double)(dateTimeOffset.Day - comparisonDateTimeOffset.Day) / daysInMonth;
        return months + dayDiff;
    }

    /// <summary>
    /// Returns the difference in quarters between two offset-aware dates.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare from.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <returns>The signed difference in quarters.</returns>
    public static double DiffInQuarters(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset) =>
        dateTimeOffset.DiffInMonths(comparisonDateTimeOffset) / 3.0;

    /// <summary>
    /// Returns the difference in years between two offset-aware dates.
    /// </summary>
    /// <param name="dateTimeOffset">The date to compare from.</param>
    /// <param name="comparisonDateTimeOffset">The date to compare against.</param>
    /// <returns>The signed difference in years.</returns>
    public static double DiffInYears(this DateTimeOffset dateTimeOffset, DateTimeOffset comparisonDateTimeOffset) =>
        dateTimeOffset.DiffInMonths(comparisonDateTimeOffset) / 12.0;
}