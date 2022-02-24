using System;
using System.Collections.Generic;
using System.Text;

namespace moment.net;

public class FinalDays
{
    readonly DateTime dateTime;
        
    public FinalDays(DateTime dateTime) => this.dateTime = dateTime;

    public FinalSpan Monday() => new(dateTime, DayOfWeek.Monday);

    public FinalSpan Tuesday() => new(dateTime, DayOfWeek.Tuesday);

    public FinalSpan Wednesday() => new(dateTime, DayOfWeek.Wednesday);

    public FinalSpan Thursday() => new(dateTime, DayOfWeek.Thursday);

    public FinalSpan Friday() => new(dateTime, DayOfWeek.Friday);

    public FinalSpan Saturday() => new(dateTime, DayOfWeek.Saturday);

    public FinalSpan Sunday() => new(dateTime, DayOfWeek.Sunday);
}