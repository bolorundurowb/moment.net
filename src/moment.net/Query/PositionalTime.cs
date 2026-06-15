using System;

namespace MomentNet.Query;

public static class PositionalTime
{
    /// <summary>
    /// Check if date time instance is a leap year
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <returns>A boolean value stating whether this date is a leap year</returns>
    public static bool IsLeapYear(this DateTime dateTime)
    {
        var year = dateTime.Year;

        if (year % 4 != 0)
            return false;

        return year % 100 != 0 || year % 400 == 0;
    }

    /// <summary>
    /// Check if date time instance is the same as a given date
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="dateToCompare">The date to compare against</param>
    /// <returns>A boolean value stating whether the compared date is the same as this date</returns>
    public static bool IsSame(this DateTime dateTime, DateTime dateToCompare)
    {
        var normalizedCurrent = dateTime.ToUniversalTime();
        var normalizedComparison = dateToCompare.ToUniversalTime();

        return normalizedCurrent == normalizedComparison;
    }

    /// <summary>
    /// Check if date time instance comes before a given date
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="dateToCompare">The date to compare against</param>
    /// <returns>A boolean value stating whether the compared date is before this date</returns>
    public static bool IsBefore(this DateTime dateTime, DateTime dateToCompare)
    {
        var normalizedCurrent = dateTime.ToUniversalTime();
        var normalizedComparison = dateToCompare.ToUniversalTime();

        return normalizedCurrent < normalizedComparison;
    }

    /// <summary>
    /// Check if date time instance is the same or comes before a given date
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="dateToCompare">The date to compare against</param>
    /// <returns>A boolean value stating whether the compared date is same or before this date</returns>
    public static bool IsSameOrBefore(this DateTime dateTime, DateTime dateToCompare)
    {
        var normalizedCurrent = dateTime.ToUniversalTime();
        var normalizedComparison = dateToCompare.ToUniversalTime();

        return normalizedCurrent.IsSame(normalizedComparison) || normalizedCurrent.IsBefore(normalizedComparison);
    }

    /// <summary>
    /// Check if date time instance comes after a given date
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="dateToCompare">The date to compare against</param>
    /// <returns>A boolean value stating whether the compared date is after this date</returns>
    public static bool IsAfter(this DateTime dateTime, DateTime dateToCompare)
    {
        var normalizedCurrent = dateTime.ToUniversalTime();
        var normalizedComparison = dateToCompare.ToUniversalTime();

        return normalizedCurrent > normalizedComparison;
    }

    /// <summary>
    /// Check if date time instance is same or comes after a given date
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="dateToCompare">The date to compare against</param>
    /// <returns>A boolean value stating whether the compared date is same or after this date</returns>
    public static bool IsSameOrAfter(this DateTime dateTime, DateTime dateToCompare)
    {
        var normalizedCurrent = dateTime.ToUniversalTime();
        var normalizedComparison = dateToCompare.ToUniversalTime();

        return normalizedCurrent.IsSame(normalizedComparison) || normalizedCurrent.IsAfter(normalizedComparison);
    }

    /// <summary>
    /// Check if date time instance is between two given dates
    /// </summary>
    /// <param name="dateTime">The given date.</param>
    /// <param name="start">The start date</param>
    /// <param name="end">The end date</param>
    /// <returns>A boolean value stating whether this date is between the start and end date</returns>
    public static bool IsBetween(this DateTime dateTime, DateTime start, DateTime end)
    {
        return dateTime.IsSameOrAfter(start) && dateTime.IsSameOrBefore(end);
    }

    /// <summary>
    /// Check if the year of a <see cref="DateTimeOffset"/> is a leap year
    /// </summary>
    public static bool IsLeapYear(this DateTimeOffset dateTimeOffset)
    {
        var year = dateTimeOffset.Year;
        if (year % 4 != 0)
            return false;
        return year % 100 != 0 || year % 400 == 0;
    }

    /// <summary>
    /// Check if two <see cref="DateTimeOffset"/> instances represent the same instant in time.
    /// Comparison is offset-aware (equivalent to comparing the underlying UTC instants).
    /// </summary>
    public static bool IsSame(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare) =>
        dateTimeOffset == dateToCompare;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> comes before another
    /// </summary>
    public static bool IsBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare) =>
        dateTimeOffset < dateToCompare;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> is the same as or comes before another
    /// </summary>
    public static bool IsSameOrBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare) =>
        dateTimeOffset <= dateToCompare;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> comes after another
    /// </summary>
    public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare) =>
        dateTimeOffset > dateToCompare;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> is the same as or comes after another
    /// </summary>
    public static bool IsSameOrAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare) =>
        dateTimeOffset >= dateToCompare;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> falls within a range (inclusive on both ends).
    /// Comparison is offset-aware.
    /// </summary>
    public static bool IsBetween(this DateTimeOffset dateTimeOffset, DateTimeOffset start, DateTimeOffset end) =>
        dateTimeOffset.IsSameOrAfter(start) && dateTimeOffset.IsSameOrBefore(end);
}