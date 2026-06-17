using System;

namespace MomentNet.Plugins.Range;

public sealed class MomentRange
{
    public MomentRange(DateTime start, DateTime end)
    {
        if (start > end)
            throw new ArgumentException("Range start must be before or equal to range end.", nameof(start));

        Start = start;
        End = end;
    }

    public DateTime Start { get; }

    public DateTime End { get; }

    public TimeSpan Duration => End - Start;

    /// <summary>
    /// Returns whether the date is inside this range.
    /// </summary>
    public bool Contains(DateTime dateTime, bool inclusive = true) =>
        inclusive ? dateTime >= Start && dateTime <= End : dateTime > Start && dateTime < End;

    /// <summary>
    /// Returns whether the other range is fully inside this range.
    /// </summary>
    public bool Contains(MomentRange other, bool inclusive = true)
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
    public bool Overlaps(MomentRange other, bool inclusive = true)
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
    public MomentRange? Intersect(MomentRange other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        if (!Overlaps(other))
            return null;

        return new MomentRange(Max(Start, other.Start), Min(End, other.End));
    }

    private static DateTime Min(DateTime first, DateTime second) => first <= second ? first : second;

    private static DateTime Max(DateTime first, DateTime second) => first >= second ? first : second;
}
