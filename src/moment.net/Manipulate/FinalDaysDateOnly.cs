#if NET6_0_OR_GREATER
using System;

namespace MomentNet.Manipulate;

public class FinalDaysDateOnly
{
    readonly DateOnly dateOnly;

    public FinalDaysDateOnly(DateOnly dateOnly) => this.dateOnly = dateOnly;

    public FinalSpanDateOnly Monday() => new(dateOnly, DayOfWeek.Monday);

    public FinalSpanDateOnly Tuesday() => new(dateOnly, DayOfWeek.Tuesday);

    public FinalSpanDateOnly Wednesday() => new(dateOnly, DayOfWeek.Wednesday);

    public FinalSpanDateOnly Thursday() => new(dateOnly, DayOfWeek.Thursday);

    public FinalSpanDateOnly Friday() => new(dateOnly, DayOfWeek.Friday);

    public FinalSpanDateOnly Saturday() => new(dateOnly, DayOfWeek.Saturday);

    public FinalSpanDateOnly Sunday() => new(dateOnly, DayOfWeek.Sunday);
}
#endif
