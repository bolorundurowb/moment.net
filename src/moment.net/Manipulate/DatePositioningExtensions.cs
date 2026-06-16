using System;

namespace MomentNet.Manipulate;

public static class DatePositioningExtensions
{
    /// <summary>
    /// Returns the next occurrence of the supplied day of week after the current date.
    /// </summary>
    /// <param name="dateTime">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <returns>The next date that falls on <paramref name="dayOfWeek"/>.</returns>
    public static DateTime Next(this DateTime dateTime, DayOfWeek dayOfWeek)
    {
        if (dateTime.DayOfWeek == dayOfWeek)
            dateTime = dateTime.AddDays(1);

        while (dateTime.DayOfWeek != dayOfWeek)
            dateTime = dateTime.AddDays(1);

        return dateTime;
    }

    /// <summary>
    /// Returns the nth next occurrence of the supplied day of week after the current date.
    /// </summary>
    /// <param name="dateTime">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <param name="count">The number of matching weekdays to advance.</param>
    /// <returns>The nth next date that falls on <paramref name="dayOfWeek"/>.</returns>
    public static DateTime Next(this DateTime dateTime, DayOfWeek dayOfWeek, int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be greater than zero.");
        for (var i = 0; i < count; i++)
            dateTime = dateTime.Next(dayOfWeek);

        return dateTime;
    }

    /// <summary>
    /// Returns the previous occurrence of the supplied day of week before the current date.
    /// </summary>
    /// <param name="dateTime">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <returns>The previous date that falls on <paramref name="dayOfWeek"/>.</returns>
    public static DateTime Last(this DateTime dateTime, DayOfWeek dayOfWeek)
    {
        if (dateTime.DayOfWeek == dayOfWeek)
            dateTime = dateTime.AddDays(-1);

        while (dateTime.DayOfWeek != dayOfWeek)
            dateTime = dateTime.AddDays(-1);

        return dateTime;
    }

    /// <summary>
    /// Returns the nth previous occurrence of the supplied day of week before the current date.
    /// </summary>
    /// <param name="dateTime">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <param name="count">The number of matching weekdays to move back.</param>
    /// <returns>The nth previous date that falls on <paramref name="dayOfWeek"/>.</returns>
    public static DateTime Last(this DateTime dateTime, DayOfWeek dayOfWeek, int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be greater than zero.");
        for (var i = 0; i < count; i++)
            dateTime = dateTime.Last(dayOfWeek);

        return dateTime;
    }

    /// <summary>
    /// Creates a builder for resolving the final occurrence of a weekday within a month or year.
    /// </summary>
    /// <param name="dateTime">The date whose month or year should be searched.</param>
    /// <returns>A fluent builder for selecting a weekday and period.</returns>
    public static FinalDays Final(this DateTime dateTime) => new FinalDays(dateTime);

    /// <summary>
    /// Returns the next occurrence of the supplied day of week after the current offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <returns>The next date that falls on <paramref name="dayOfWeek"/>, preserving the original offset.</returns>
    public static DateTimeOffset Next(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek)
    {
        if (dateTimeOffset.DayOfWeek == dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(1);

        while (dateTimeOffset.DayOfWeek != dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(1);

        return dateTimeOffset;
    }

    /// <summary>
    /// Returns the nth next occurrence of the supplied day of week after the current offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <param name="count">The number of matching weekdays to advance.</param>
    /// <returns>The nth next date that falls on <paramref name="dayOfWeek"/>, preserving the original offset.</returns>
    public static DateTimeOffset Next(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek, int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be greater than zero.");
        for (var i = 0; i < count; i++)
            dateTimeOffset = dateTimeOffset.Next(dayOfWeek);

        return dateTimeOffset;
    }

    /// <summary>
    /// Returns the previous occurrence of the supplied day of week before the current offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <returns>The previous date that falls on <paramref name="dayOfWeek"/>, preserving the original offset.</returns>
    public static DateTimeOffset Last(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek)
    {
        if (dateTimeOffset.DayOfWeek == dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(-1);

        while (dateTimeOffset.DayOfWeek != dayOfWeek)
            dateTimeOffset = dateTimeOffset.AddDays(-1);

        return dateTimeOffset;
    }

    /// <summary>
    /// Returns the nth previous occurrence of the supplied day of week before the current offset-aware date.
    /// </summary>
    /// <param name="dateTimeOffset">The date to start from.</param>
    /// <param name="dayOfWeek">The day of week to find.</param>
    /// <param name="count">The number of matching weekdays to move back.</param>
    /// <returns>The nth previous date that falls on <paramref name="dayOfWeek"/>, preserving the original offset.</returns>
    public static DateTimeOffset Last(this DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek, int count)
    {
        if (count <= 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be greater than zero.");
        for (var i = 0; i < count; i++)
            dateTimeOffset = dateTimeOffset.Last(dayOfWeek);

        return dateTimeOffset;
    }

    /// <summary>
    /// Creates a builder for resolving the final occurrence of a weekday within a month or year.
    /// </summary>
    /// <param name="dateTimeOffset">The date whose month or year should be searched.</param>
    /// <returns>A fluent builder for selecting a weekday and period.</returns>
    public static FinalDaysOffset Final(this DateTimeOffset dateTimeOffset) => new FinalDaysOffset(dateTimeOffset);
}