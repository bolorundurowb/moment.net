#if NET6_0_OR_GREATER
using System;

namespace MomentNet.Plugins.Range;

public sealed class MomentDateOnlyRange
{
    public MomentDateOnlyRange(DateOnly start, DateOnly end)
    {
        if (start > end)
            throw new ArgumentException("Range start must be before or equal to range end.", nameof(start));

        Start = start;
        End = end;
    }

    public DateOnly Start { get; }

    public DateOnly End { get; }

    public TimeSpan Duration => End.ToDateTime(default) - Start.ToDateTime(default);

    /// <summary>
    /// Returns whether the date is inside this range.
    /// </summary>
    public bool Contains(DateOnly dateOnly, bool inclusive = true) =>
        inclusive ? dateOnly >= Start && dateOnly <= End : dateOnly > Start && dateOnly < End;

    /// <summary>
    /// Returns whether the other range is fully inside this range.
    /// </summary>
    public bool Contains(MomentDateOnlyRange other, bool inclusive = true)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        return inclusive
            ? other.Start >= Start && other.End <= End
            : other.Start > Start && other.End < End;
    }

    /// <summary>
    /// Returns whether this range overlaps another range.
    /// </summary>
    public bool Overlaps(MomentDateOnlyRange other, bool inclusive = true)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        return inclusive
            ? Start <= other.End && other.Start <= End
            : Start < other.End && other.Start < End;
    }

    /// <summary>
    /// Returns the intersection of this range and another range, or null when they do not overlap.
    /// </summary>
    public MomentDateOnlyRange? Intersect(MomentDateOnlyRange other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        if (!Overlaps(other))
            return null;

        return new MomentDateOnlyRange(Max(Start, other.Start), Min(End, other.End));
    }

    private static DateOnly Min(DateOnly first, DateOnly second) => first <= second ? first : second;

    private static DateOnly Max(DateOnly first, DateOnly second) => first >= second ? first : second;
}
#endif
