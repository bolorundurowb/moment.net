#if NET6_0_OR_GREATER
using System;

namespace MomentNet.Manipulate;

public class FinalSpanDateOnly
{
    private DateOnly _dateOnly;
    private readonly DayOfWeek _dayOfWeek;

    public FinalSpanDateOnly(DateOnly dateOnly, DayOfWeek dayOfWeek)
    {
        _dateOnly = dateOnly;
        _dayOfWeek = dayOfWeek;
    }

    public DateOnly InMonth()
    {
        var month = _dateOnly.Month;
        var current = new DateOnly(_dateOnly.Year, _dateOnly.Month, DateTime.DaysInMonth(_dateOnly.Year, _dateOnly.Month) - 6);
        while (current.Month == month)
        {
            if (current.DayOfWeek == _dayOfWeek)
            {
                return current;
            }

            current = current.AddDays(1);
        }

        return DateOnly.MaxValue;
    }

    public DateOnly InYear()
    {
        var dateOnly = new DateOnly(_dateOnly.Year, 12, DateTime.DaysInMonth(_dateOnly.Year, 12) - 6);
        return new FinalSpanDateOnly(dateOnly, _dayOfWeek).InMonth();
    }
}
#endif
