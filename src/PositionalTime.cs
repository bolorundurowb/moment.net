using System;

namespace moment.net;

public static class PositionalTime
{
    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsLeapYear(this DateTime dateTime) => new DateTimeOffset(dateTime).IsLeapYear();

    /// <summary>
    /// Checks if the provided date time instance characterises a leap year.
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <returns>A boolean value identifying whether this date falls in a leap year.</returns>
    public static bool IsLeapYear(this DateTimeOffset dateTimeOffset)
    {
        var year = dateTimeOffset.Year;

        if (year % 4 != 0)
            return false;

        return year % 100 != 0 || year % 400 == 0;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsSame(this DateTime dateTime, DateTime dateToCompare) =>
        new DateTimeOffset(dateTime).IsSame(new DateTimeOffset(dateToCompare));

    /// <summary>
    /// Checks if the date time instance evaluates to the exact same time as a compared date.
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <param name="dateToCompare">The date to evaluate against.</param>
    /// <returns>A boolean value signifying whether both dates evaluate equally.</returns>
    public static bool IsSame(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare)
    {
        var normalisedCurrent = dateTimeOffset.ToUniversalTime();
        var normalisedComparison = dateToCompare.ToUniversalTime();

        return normalisedCurrent == normalisedComparison;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsBefore(this DateTime dateTime, DateTime dateToCompare) =>
        new DateTimeOffset(dateTime).IsBefore(new DateTimeOffset(dateToCompare));

    /// <summary>
    /// Checks if the date time instance occurs earlier than a specified date.
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <param name="dateToCompare">The date to evaluate against.</param>
    /// <returns>A boolean value signifying whether the primary date occurs before the comparison date.</returns>
    public static bool IsBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare)
    {
        var normalisedCurrent = dateTimeOffset.ToUniversalTime();
        var normalisedComparison = dateToCompare.ToUniversalTime();

        return normalisedCurrent < normalisedComparison;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsSameOrBefore(this DateTime dateTime, DateTime dateToCompare) =>
        new DateTimeOffset(dateTime).IsSameOrBefore(new DateTimeOffset(dateToCompare));

    /// <summary>
    /// Determines whether the current DateTimeOffset instance occurs at the same time or before the specified DateTimeOffset instance.
    /// </summary>
    /// <param name="dateTimeOffset">The current DateTimeOffset instance to be compared.</param>
    /// <param name="dateToCompare">The DateTimeOffset instance to compare against.</param>
    /// <returns>A boolean indicating whether the current DateTimeOffset instance is the same or occurs before the specified DateTimeOffset instance.</returns>
    public static bool IsSameOrBefore(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare)
    {
        var normalisedCurrent = dateTimeOffset.ToUniversalTime();
        var normalisedComparison = dateToCompare.ToUniversalTime();

        return normalisedCurrent <= normalisedComparison;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsAfter(this DateTime dateTime, DateTime dateToCompare) =>
        new DateTimeOffset(dateTime).IsAfter(new DateTimeOffset(dateToCompare));

    /// <summary>
    /// Checks if the date time instance occurs later than a specified date.
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <param name="dateToCompare">The date to evaluate against.</param>
    /// <returns>A boolean value signifying whether the primary date occurs after the comparison date.</returns>
    public static bool IsAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare)
    {
        var normalisedCurrent = dateTimeOffset.ToUniversalTime();
        var normalisedComparison = dateToCompare.ToUniversalTime();

        return normalisedCurrent > normalisedComparison;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsSameOrAfter(this DateTime dateTime, DateTime dateToCompare) =>
        new DateTimeOffset(dateTime).IsSameOrAfter(new DateTimeOffset(dateToCompare));

    /// <summary>
    /// Checks if the date time instance occurs later than or concurrently with a specified date.
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <param name="dateToCompare">The date to evaluate against.</param>
    /// <returns>A boolean value signifying whether the primary date is concurrent or subsequent to the comparison date.</returns>
    public static bool IsSameOrAfter(this DateTimeOffset dateTimeOffset, DateTimeOffset dateToCompare)
    {
        var normalisedCurrent = dateTimeOffset.ToUniversalTime();
        var normalisedComparison = dateToCompare.ToUniversalTime();

        return normalisedCurrent >= normalisedComparison;
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsBetween(this DateTime dateTime, DateTime start, DateTime end) =>
        new DateTimeOffset(dateTime).IsBetween(new DateTimeOffset(start), new DateTimeOffset(end));

    /// <summary>
    /// Checks if the date time instance is bounded exclusively between a start and end date.
    /// </summary>
    /// <param name="dateTimeOffset">The given date to evaluate.</param>
    /// <param name="start">The start of the bounding period.</param>
    /// <param name="end">The end of the bounding period.</param>
    /// <returns>A boolean value signifying if the primary date is situated between the given limits.</returns>
    public static bool IsBetween(this DateTimeOffset dateTimeOffset, DateTimeOffset start, DateTimeOffset end)
    {
        var normalisedCurrent = dateTimeOffset.ToUniversalTime();
        return normalisedCurrent > start.ToUniversalTime() && normalisedCurrent < end.ToUniversalTime();
    }

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsWeekend(this DateTime dateTime) => new DateTimeOffset(dateTime).IsWeekend();

    /// <summary>
    /// Checks if the date time instance falls on a weekend (Saturday or Sunday).
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <returns>A boolean value signifying whether the date is a weekend.</returns>
    public static bool IsWeekend(this DateTimeOffset dateTimeOffset) => dateTimeOffset.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsWeekday(this DateTime dateTime) => new DateTimeOffset(dateTime).IsWeekday();

    /// <summary>
    /// Checks if the date time instance falls on a weekday (Monday to Friday).
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <returns>A boolean value signifying whether the date is a weekday.</returns>
    public static bool IsWeekday(this DateTimeOffset dateTimeOffset) => !dateTimeOffset.IsWeekend();

    [Obsolete("This method will be removed in the next major version. Use the DateTimeOffset overload instead.", false)]
    public static bool IsBusinessDay(this DateTime dateTime) => new DateTimeOffset(dateTime).IsBusinessDay();

    /// <summary>
    /// Checks if the date time instance is a standard business day (Monday to Friday). 
    /// Note: This method does not account for public holidays.
    /// </summary>
    /// <param name="dateTimeOffset">The given date.</param>
    /// <returns>A boolean value signifying whether the date is a standard business day.</returns>
    public static bool IsBusinessDay(this DateTimeOffset dateTimeOffset) => dateTimeOffset.IsWeekday();
}