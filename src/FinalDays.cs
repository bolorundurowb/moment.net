using System;
using System.Collections.Generic;
using System.Text;

namespace moment.net;

public class FinalDays
{
    readonly DateTime dateTime;
        
    public FinalDays(DateTime dateTime)
    {
        this.dateTime = dateTime;
    }

    public FinalSpan Monday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Monday);
    }

    public FinalSpan Tuesday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Tuesday);
    }

    public FinalSpan Wednesday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Wednesday);
    }

    public FinalSpan Thursday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Thursday);
    }

    public FinalSpan Friday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Friday);
    }

    public FinalSpan Saturday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Saturday);
    }

    public FinalSpan Sunday()
    {
        return new FinalSpan(dateTime, DayOfWeek.Sunday);
    }
}