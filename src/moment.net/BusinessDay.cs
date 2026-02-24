using System;

namespace moment.net;

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
    /// Check if date time instance is a weekend (Saturday or Sunday)
    /// </summary>
    /// <param name="dt">The given date</param>
    /// <returns>A boolean value stating whether this date is a weekend</returns>
    public static bool IsWeekend(this DateTime dt)
    {
        return dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;
    }

    /// <summary>
    /// Check if date time instance is a weekday (Monday to Friday)
    /// </summary>
    /// <param name="dt">The given date</param>
    /// <returns>A boolean value stating whether this date is a weekday</returns>
    public static bool IsWeekday(this DateTime dt)
    {
        return !dt.IsWeekend();
    }

    /// <summary>
    /// Adds business days to the current date time instance
    /// </summary>
    /// <param name="dateTime">The given date</param>
    /// <param name="days">The number of business days to add</param>
    /// <returns>A new date time instance with the added business days</returns>
    public static DateTime AddBusinessDays(this DateTime dateTime, int days)
    {
        if (days == 0)
        {
            return dateTime;
        }

        var result = dateTime;
        var direction = days > 0 ? 1 : -1;
        var remainingDays = Math.Abs(days);

        while (remainingDays > 0)
        {
            result = result.AddDays(direction);
            if (result.IsBusinessDay())
            {
                remainingDays--;
            }
        }

        return result;
    }
}
