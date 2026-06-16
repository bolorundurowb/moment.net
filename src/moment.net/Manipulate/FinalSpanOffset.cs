using System;

namespace MomentNet.Manipulate;

/// <summary>
/// Represents the result of a <see cref="FinalDaysOffset"/> day selection, exposing
/// <see cref="InMonth"/> and <see cref="InYear"/> to resolve the final occurrence.
/// The UTC offset of the original <see cref="DateTimeOffset"/> is preserved in returned values.
/// </summary>
public class FinalSpanOffset
{
    private DateTimeOffset _dateTimeOffset;
    private readonly DayOfWeek _dayOfWeek;

    public FinalSpanOffset(DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek)
    {
        _dateTimeOffset = dateTimeOffset;
        _dayOfWeek = dayOfWeek;
    }

    /// <summary>
    /// Returns the last occurrence of the chosen weekday within the month of the original value.
    /// </summary>
    public DateTimeOffset InMonth()
    {
        var month = _dateTimeOffset.Month;
        var offset = _dateTimeOffset.Offset;
        var current = new DateTimeOffset(
            _dateTimeOffset.Year,
            _dateTimeOffset.Month,
            DateTime.DaysInMonth(_dateTimeOffset.Year, _dateTimeOffset.Month) - 6,
            0, 0, 0, offset);

        while (current.Month == month)
        {
            if (current.DayOfWeek == _dayOfWeek)
                return current;

            current = current.AddDays(1);
        }

        return DateTimeOffset.MaxValue;
    }

    /// <summary>
    /// Returns the last occurrence of the chosen weekday within the year of the original value.
    /// </summary>
    public DateTimeOffset InYear()
    {
        var offset = _dateTimeOffset.Offset;
        var dateTimeOffset = new DateTimeOffset(
            _dateTimeOffset.Year, 12,
            DateTime.DaysInMonth(_dateTimeOffset.Year, 12) - 6,
            0, 0, 0, offset);
        return new FinalSpanOffset(dateTimeOffset, _dayOfWeek).InMonth();
    }
}
