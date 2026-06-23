using System;
using System.Collections.Generic;
using MomentNet.Plugins.Range;

namespace MomentNet;

public static class Moment
{
    /// <summary>
    /// Returns the earliest date from the supplied dates.
    /// </summary>
    public static DateTime Min(params DateTime[] dates) => Min((IEnumerable<DateTime>)dates);

    /// <summary>
    /// Returns the earliest date from the supplied dates.
    /// </summary>
    public static DateTime Min(IEnumerable<DateTime> dates)
    {
        if (dates is null)
            throw new ArgumentNullException(nameof(dates));

        using var enumerator = dates.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var min = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current < min)
                min = enumerator.Current;
        }

        return min;
    }

    /// <summary>
    /// Returns the latest date from the supplied dates.
    /// </summary>
    public static DateTime Max(params DateTime[] dates) => Max((IEnumerable<DateTime>)dates);

    /// <summary>
    /// Returns the latest date from the supplied dates.
    /// </summary>
    public static DateTime Max(IEnumerable<DateTime> dates)
    {
        if (dates is null)
            throw new ArgumentNullException(nameof(dates));

        using var enumerator = dates.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var max = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current > max)
                max = enumerator.Current;
        }

        return max;
    }

    /// <summary>
    /// Returns the earliest instant from the supplied dates.
    /// </summary>
    public static DateTimeOffset Min(params DateTimeOffset[] dates) => Min((IEnumerable<DateTimeOffset>)dates);

    /// <summary>
    /// Returns the earliest instant from the supplied dates.
    /// </summary>
    public static DateTimeOffset Min(IEnumerable<DateTimeOffset> dates)
    {
        if (dates is null)
            throw new ArgumentNullException(nameof(dates));

        using var enumerator = dates.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var min = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current < min)
                min = enumerator.Current;
        }

        return min;
    }

    /// <summary>
    /// Returns the latest instant from the supplied dates.
    /// </summary>
    public static DateTimeOffset Max(params DateTimeOffset[] dates) => Max((IEnumerable<DateTimeOffset>)dates);

    /// <summary>
    /// Returns the latest instant from the supplied dates.
    /// </summary>
    public static DateTimeOffset Max(IEnumerable<DateTimeOffset> dates)
    {
        if (dates is null)
            throw new ArgumentNullException(nameof(dates));

        using var enumerator = dates.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var max = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current > max)
                max = enumerator.Current;
        }

        return max;
    }

    /// <summary>
    /// Creates a date range with inclusive boundaries.
    /// </summary>
    public static MomentRange Range(DateTime start, DateTime end) => new MomentRange(start, end);

    /// <summary>
    /// Creates a date range with inclusive boundaries.
    /// </summary>
    public static MomentRangeOffset Range(DateTimeOffset start, DateTimeOffset end) => new MomentRangeOffset(start, end);

#if NET6_0_OR_GREATER
    /// <summary>
    /// Returns the earliest date from the supplied <see cref="DateOnly"/> values.
    /// </summary>
    public static DateOnly Min(params DateOnly[] dates) => Min((IEnumerable<DateOnly>)dates);

    /// <summary>
    /// Returns the earliest date from the supplied <see cref="DateOnly"/> values.
    /// </summary>
    public static DateOnly Min(IEnumerable<DateOnly> dates)
    {
        if (dates is null)
            throw new ArgumentNullException(nameof(dates));

        using var enumerator = dates.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var min = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current < min)
                min = enumerator.Current;
        }

        return min;
    }

    /// <summary>
    /// Returns the latest date from the supplied <see cref="DateOnly"/> values.
    /// </summary>
    public static DateOnly Max(params DateOnly[] dates) => Max((IEnumerable<DateOnly>)dates);

    /// <summary>
    /// Returns the latest date from the supplied <see cref="DateOnly"/> values.
    /// </summary>
    public static DateOnly Max(IEnumerable<DateOnly> dates)
    {
        if (dates is null)
            throw new ArgumentNullException(nameof(dates));

        using var enumerator = dates.GetEnumerator();
        if (!enumerator.MoveNext())
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var max = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current > max)
                max = enumerator.Current;
        }

        return max;
    }

    /// <summary>
    /// Creates a date range with inclusive boundaries for <see cref="DateOnly"/>.
    /// </summary>
    public static MomentDateOnlyRange Range(DateOnly start, DateOnly end) => new MomentDateOnlyRange(start, end);
#endif

#if NET8_0_OR_GREATER
    /// <summary>
    /// Returns the earliest date from the supplied dates.
    /// </summary>
    public static DateTime Min(params ReadOnlySpan<DateTime> dates)
    {
        if (dates.IsEmpty)
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var min = dates[0];
        for (var i = 1; i < dates.Length; i++)
        {
            if (dates[i] < min)
                min = dates[i];
        }

        return min;
    }

    /// <summary>
    /// Returns the latest date from the supplied dates.
    /// </summary>
    public static DateTime Max(params ReadOnlySpan<DateTime> dates)
    {
        if (dates.IsEmpty)
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var max = dates[0];
        for (var i = 1; i < dates.Length; i++)
        {
            if (dates[i] > max)
                max = dates[i];
        }

        return max;
    }

    /// <summary>
    /// Returns the earliest instant from the supplied dates.
    /// </summary>
    public static DateTimeOffset Min(params ReadOnlySpan<DateTimeOffset> dates)
    {
        if (dates.IsEmpty)
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var min = dates[0];
        for (var i = 1; i < dates.Length; i++)
        {
            if (dates[i] < min)
                min = dates[i];
        }

        return min;
    }

    /// <summary>
    /// Returns the latest instant from the supplied dates.
    /// </summary>
    public static DateTimeOffset Max(params ReadOnlySpan<DateTimeOffset> dates)
    {
        if (dates.IsEmpty)
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var max = dates[0];
        for (var i = 1; i < dates.Length; i++)
        {
            if (dates[i] > max)
                max = dates[i];
        }

        return max;
    }

    /// <summary>
    /// Returns the earliest date from the supplied <see cref="DateOnly"/> values.
    /// </summary>
    public static DateOnly Min(params ReadOnlySpan<DateOnly> dates)
    {
        if (dates.IsEmpty)
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var min = dates[0];
        for (var i = 1; i < dates.Length; i++)
        {
            if (dates[i] < min)
                min = dates[i];
        }

        return min;
    }

    /// <summary>
    /// Returns the latest date from the supplied <see cref="DateOnly"/> values.
    /// </summary>
    public static DateOnly Max(params ReadOnlySpan<DateOnly> dates)
    {
        if (dates.IsEmpty)
            throw new ArgumentException("At least one date must be supplied.", nameof(dates));

        var max = dates[0];
        for (var i = 1; i < dates.Length; i++)
        {
            if (dates[i] > max)
                max = dates[i];
        }

        return max;
    }
#endif
}
