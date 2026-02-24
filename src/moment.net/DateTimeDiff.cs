using System;

namespace moment.net;

public static class DateTimeDiff
{
    private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
    private const double DaysInAMonth = DaysInAYear / 12;

    /// <summary>
    /// Returns the difference in days between this and another date
    /// </summary>
    /// <param name="dt">The given date</param>
    /// <param name="other">The date to compare with</param>
    /// <returns>The difference in days</returns>
    public static double DiffInDays(this DateTime dt, DateTime other)
    {
        return (dt - other).TotalDays;
    }

    /// <summary>
    /// Returns the difference in months between this and another date
    /// </summary>
    /// <param name="dt">The given date</param>
    /// <param name="other">The date to compare with</param>
    /// <returns>The difference in months</returns>
    public static double DiffInMonths(this DateTime dt, DateTime other)
    {
        var months = (dt.Year - other.Year) * 12 + dt.Month - other.Month;
        var daysInMonth = DateTime.DaysInMonth(other.Year, other.Month);
        var dayDiff = (double)(dt.Day - other.Day) / daysInMonth;
        return months + dayDiff;
    }

    /// <summary>
    /// Returns the difference in years between this and another date
    /// </summary>
    /// <param name="dt">The given date</param>
    /// <param name="other">The date to compare with</param>
    /// <returns>The difference in years</returns>
    public static double DiffInYears(this DateTime dt, DateTime other)
    {
        return dt.DiffInMonths(other) / 12.0;
    }
}
