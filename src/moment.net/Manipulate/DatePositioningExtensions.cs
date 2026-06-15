using System;

namespace MomentNet.Manipulate;

public static class DatePositioningExtensions
{
    /// <summary>
    /// Return the <see cref="DateTime"/> for the next <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTime Next(this DateTime This, DayOfWeek dayOfWeek)
    {
        if (This.DayOfWeek == dayOfWeek)
            This = This.AddDays(1);

        while (This.DayOfWeek != dayOfWeek)
            This = This.AddDays(1);

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTime"/> for the nth next <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTime Next(this DateTime This, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
            This = This.Next(dayOfWeek);

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTime"/> for the previous <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTime Last(this DateTime This, DayOfWeek dayOfWeek)
    {
        if (This.DayOfWeek == dayOfWeek)
            This = This.AddDays(-1);

        while (This.DayOfWeek != dayOfWeek)
            This = This.AddDays(-1);

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTime"/> for the nth previous <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTime Last(this DateTime This, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
            This = This.Last(dayOfWeek);

        return This;
    }

    public static FinalDays Final(this DateTime This) => new FinalDays(This);

    /// <summary>
    /// Return the <see cref="DateTimeOffset"/> for the next <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTimeOffset Next(this DateTimeOffset This, DayOfWeek dayOfWeek)
    {
        if (This.DayOfWeek == dayOfWeek)
            This = This.AddDays(1);

        while (This.DayOfWeek != dayOfWeek)
            This = This.AddDays(1);

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTimeOffset"/> for the nth next <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTimeOffset Next(this DateTimeOffset This, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
            This = This.Next(dayOfWeek);

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTimeOffset"/> for the previous <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTimeOffset Last(this DateTimeOffset This, DayOfWeek dayOfWeek)
    {
        if (This.DayOfWeek == dayOfWeek)
            This = This.AddDays(-1);

        while (This.DayOfWeek != dayOfWeek)
            This = This.AddDays(-1);

        return This;
    }

    /// <summary>
    /// Return the <see cref="DateTimeOffset"/> for the nth previous <see cref="DayOfWeek"/> supplied.
    /// </summary>
    public static DateTimeOffset Last(this DateTimeOffset This, DayOfWeek dayOfWeek, int count)
    {
        for (var i = 0; i < count; i++)
            This = This.Last(dayOfWeek);

        return This;
    }

    /// <summary>
    /// Returns a <see cref="FinalDaysOffset"/> builder for finding the last occurrence of a weekday.
    /// </summary>
    public static FinalDaysOffset Final(this DateTimeOffset This) => new FinalDaysOffset(This);
}