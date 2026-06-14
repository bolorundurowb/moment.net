using System;

namespace moment.net;

/// <summary>
/// Fluent builder for finding the last occurrence of a weekday within a month or year,
/// operating on <see cref="DateTimeOffset"/> values. The UTC offset of the original value is preserved.
/// </summary>
public class FinalDaysOffset
{
    private readonly DateTimeOffset _dateTimeOffset;

    public FinalDaysOffset(DateTimeOffset dateTimeOffset) => _dateTimeOffset = dateTimeOffset;

    public FinalSpanOffset Monday() => new(_dateTimeOffset, DayOfWeek.Monday);

    public FinalSpanOffset Tuesday() => new(_dateTimeOffset, DayOfWeek.Tuesday);

    public FinalSpanOffset Wednesday() => new(_dateTimeOffset, DayOfWeek.Wednesday);

    public FinalSpanOffset Thursday() => new(_dateTimeOffset, DayOfWeek.Thursday);

    public FinalSpanOffset Friday() => new(_dateTimeOffset, DayOfWeek.Friday);

    public FinalSpanOffset Saturday() => new(_dateTimeOffset, DayOfWeek.Saturday);

    public FinalSpanOffset Sunday() => new(_dateTimeOffset, DayOfWeek.Sunday);
}
