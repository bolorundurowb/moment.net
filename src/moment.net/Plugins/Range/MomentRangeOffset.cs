using System;

namespace MomentNet.Plugins.Range;

public sealed class MomentRangeOffset
{
    public MomentRangeOffset(DateTimeOffset start, DateTimeOffset end)
    {
        if (start > end)
            throw new ArgumentException("Range start must be before or equal to range end.", nameof(start));

        Start = start;
        End = end;
    }

    public DateTimeOffset Start { get; }

    public DateTimeOffset End { get; }

    public TimeSpan Duration => End - Start;

    /// <summary>
    /// Returns whether the date is inside this range.
    /// </summary>
    public bool Contains(DateTimeOffset dateTime, bool inclusive = true) =>
        inclusive ? dateTime >= Start && dateTime <= End : dateTime > Start && dateTime < End;

    /// <summary>
    /// Returns whether the other range is fully inside this range.
    /// </summary>
    public bool Contains(MomentRangeOffset other, bool inclusive = true)
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
    public bool Overlaps(MomentRangeOffset other, bool inclusive = true)
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
    public MomentRangeOffset? Intersect(MomentRangeOffset other)
    {
        if (other is null)
            throw new ArgumentNullException(nameof(other));

        if (!Overlaps(other))
            return null;

        return new MomentRangeOffset(Max(Start, other.Start), Min(End, other.End));
    }

    private static DateTimeOffset Min(DateTimeOffset first, DateTimeOffset second) => first <= second ? first : second;

    private static DateTimeOffset Max(DateTimeOffset first, DateTimeOffset second) => first >= second ? first : second;
}
