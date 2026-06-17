using System;
using System.Collections.Generic;

namespace MomentNet.Plugins.BusinessDays;

public static class BusinessDay
{
    /// <summary>
    /// Check if date time instance is a business day (Monday to Friday)
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <returns>A boolean value stating whether this date is a business day</returns>
    public static bool IsBusinessDay(this DateTime dateTime)
    {
        return dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
    }

    /// <summary>
    /// Check if date time instance is a business day, excluding weekends and supplied holidays.
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="holidays">Holiday dates to exclude. Time components are ignored.</param>
    /// <returns>A boolean value stating whether this date is a business day</returns>
    public static bool IsBusinessDay(this DateTime dateTime, IEnumerable<DateTime> holidays)
    {
        return dateTime.IsBusinessDay() && !ToHolidaySet(holidays).Contains(dateTime.Date);
    }

    /// <summary>
    /// Check if date time instance is a weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="dateTime">The given date.</param>
    /// <returns>A boolean value stating whether this date is a weekend</returns>
    public static bool IsWeekend(this DateTime dateTime)
    {
        return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    /// Check if date time instance is a weekday (Monday to Friday)
    /// </summary>
    /// <param name="dateTime">The given date.</param>
    /// <returns>A boolean value stating whether this date is a weekday</returns>
    public static bool IsWeekday(this DateTime dateTime)
    {
        return !dateTime.IsWeekend();
    }

    /// <summary>
    /// Adds business days to the current date time instance.
    /// Uses week-batching for efficiency on large day counts.
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="days">The number of business days to add</param>
    /// <returns>A new date time instance with the added business days</returns>
    public static DateTime AddBusinessDays(this DateTime dateTime, int days)
    {
        if (days == 0)
            return dateTime;

        var direction = days > 0 ? 1 : -1;
        var absDays = Math.Abs(days);

        // Batch by full weeks (5 business days = 7 calendar days)
        var weeks = absDays / 5;
        var result = dateTime.AddDays(weeks * 7 * direction);

        // Handle remaining 0-4 business days
        var remaining = absDays % 5;
        while (remaining > 0)
        {
            result = result.AddDays(direction);
            if (result.IsBusinessDay())
                remaining--;
        }

        return result;
    }

    /// <summary>
    /// Adds business days to the current date time instance, skipping weekends and supplied holidays.
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="days">The number of business days to add</param>
    /// <param name="holidays">Holiday dates to skip. Time components are ignored.</param>
    /// <returns>A new date time instance with the added business days</returns>
    public static DateTime AddBusinessDays(this DateTime dateTime, int days, IEnumerable<DateTime> holidays)
    {
        if (days == 0)
        {
            return dateTime;
        }

        var result = dateTime;
        var direction = days > 0 ? 1 : -1;
        var remainingDays = Math.Abs(days);
        var holidaySet = ToHolidaySet(holidays);

        while (remainingDays > 0)
        {
            result = result.AddDays(direction);
            if (result.IsBusinessDay() && !holidaySet.Contains(result.Date))
            {
                remainingDays--;
            }
        }

        return result;
    }

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> instance is a business day (Monday to Friday).
    /// The day of week is evaluated in the offset's local time.
    /// </summary>
    public static bool IsBusinessDay(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.DayOfWeek != DayOfWeek.Saturday && dateTimeOffset.DayOfWeek != DayOfWeek.Sunday;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> instance is a business day, excluding weekends and supplied holidays.
    /// Holiday comparison uses the value's local date in its offset.
    /// </summary>
    public static bool IsBusinessDay(this DateTimeOffset dateTimeOffset, IEnumerable<DateTimeOffset> holidays) =>
        dateTimeOffset.IsBusinessDay() && !ToHolidaySet(holidays).Contains(dateTimeOffset.Date);

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> instance is a weekend (Saturday or Sunday).
    /// The day of week is evaluated in the offset's local time.
    /// </summary>
    public static bool IsWeekend(this DateTimeOffset dateTimeOffset) =>
        dateTimeOffset.DayOfWeek == DayOfWeek.Saturday || dateTimeOffset.DayOfWeek == DayOfWeek.Sunday;

    /// <summary>
    /// Check if a <see cref="DateTimeOffset"/> instance is a weekday (Monday to Friday).
    /// The day of week is evaluated in the offset's local time.
    /// </summary>
    public static bool IsWeekday(this DateTimeOffset dateTimeOffset) => !dateTimeOffset.IsWeekend();

    /// <summary>
    /// Adds business days to the given <see cref="DateTimeOffset"/>, skipping weekends.
    /// Uses week-batching for efficiency on large day counts.
    /// The UTC offset is preserved in the returned value.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date.</param>
    /// <param name="days">The number of business days to add (may be negative)</param>
    public static DateTimeOffset AddBusinessDays(this DateTimeOffset dateTimeOffset, int days)
    {
        if (days == 0)
            return dateTimeOffset;

        var direction = days > 0 ? 1 : -1;
        var absDays = Math.Abs(days);

        // Batch by full weeks (5 business days = 7 calendar days)
        var weeks = absDays / 5;
        var result = dateTimeOffset.AddDays(weeks * 7 * direction);

        // Handle remaining 0-4 business days
        var remaining = absDays % 5;
        while (remaining > 0)
        {
            result = result.AddDays(direction);
            if (result.IsBusinessDay())
                remaining--;
        }

        return result;
    }

    /// <summary>
    /// Adds business days to the given <see cref="DateTimeOffset"/>, skipping weekends and supplied holidays.
    /// The UTC offset is preserved in the returned value.
    /// </summary>
    /// <param name="dateTimeOffset">The starting date.</param>
    /// <param name="days">The number of business days to add (may be negative)</param>
    /// <param name="holidays">Holiday dates to skip. Time components and offsets are ignored.</param>
    public static DateTimeOffset AddBusinessDays(this DateTimeOffset dateTimeOffset, int days, IEnumerable<DateTimeOffset> holidays)
    {
        if (days == 0)
            return dateTimeOffset;

        var result = dateTimeOffset;
        var direction = days > 0 ? 1 : -1;
        var remainingDays = Math.Abs(days);
        var holidaySet = ToHolidaySet(holidays);

        while (remainingDays > 0)
        {
            result = result.AddDays(direction);
            if (result.IsBusinessDay() && !holidaySet.Contains(result.Date))
                remainingDays--;
        }

        return result;
    }

    private static HashSet<DateTime> ToHolidaySet(IEnumerable<DateTime> holidays)
    {
        if (holidays is null)
            throw new ArgumentNullException(nameof(holidays));

        var holidaySet = new HashSet<DateTime>();
        foreach (var holiday in holidays)
            holidaySet.Add(holiday.Date);

        return holidaySet;
    }

    private static HashSet<DateTime> ToHolidaySet(IEnumerable<DateTimeOffset> holidays)
    {
        if (holidays is null)
            throw new ArgumentNullException(nameof(holidays));

        var holidaySet = new HashSet<DateTime>();
        foreach (var holiday in holidays)
            holidaySet.Add(holiday.Date);

        return holidaySet;
    }
}
