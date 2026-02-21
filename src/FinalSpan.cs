using System;

namespace moment.net;

public class FinalSpan
{
    private DateTimeOffset _dateTimeOffset;
    private readonly DayOfWeek _dayOfWeek;

    /// <summary>
    /// Represents a utility for calculating the last occurrence of a specific day of the week
    /// within the final seven days of a month or a year.
    /// </summary>
    public FinalSpan(DateTimeOffset dateTimeOffset, DayOfWeek dayOfWeek)
    {
        _dateTimeOffset = dateTimeOffset;
        _dayOfWeek = dayOfWeek;
    }

    /// <summary>
    /// Determines the last occurrence of the specified day of the week within the final seven days of the month
    /// based on the stored <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>A <see cref="DateTimeOffset"/> representing the final occurrence of the specified day of the week
    /// in the last seven days of the month. If no matching day is found, it returns <see cref="DateTimeOffset.MaxValue"/>.</returns>
    public DateTimeOffset InMonth()
    {
        var month = _dateTimeOffset.Month;
        // Evaluate only through the final seven days of the month
        _dateTimeOffset = new DateTimeOffset(_dateTimeOffset.Year, _dateTimeOffset.Month, (DateTime.DaysInMonth(_dateTimeOffset.Year, _dateTimeOffset.Month) - 7), 0, 0, 0, _dateTimeOffset.Offset);
        while (_dateTimeOffset.Month == month)
        {
            if (_dateTimeOffset.DayOfWeek == _dayOfWeek)
            {
                return _dateTimeOffset;
            }

            _dateTimeOffset = _dateTimeOffset.AddDays(1);
        }

        return DateTimeOffset.MaxValue;
    }

    /// <summary>
    /// Determines the last occurrence of the specified day of the week within the final seven days of the year
    /// based on the stored <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>A <see cref="DateTimeOffset"/> representing the final occurrence of the specified day of the week
    /// in the last seven days of the year. If no matching day is found, it returns <see cref="DateTimeOffset.MaxValue"/>.</returns>
    public DateTimeOffset InYear()
    {
        var endOfYear = new DateTimeOffset(_dateTimeOffset.Year, 12, (DateTime.DaysInMonth(_dateTimeOffset.Year, 12) - 7), 0, 0, 0, _dateTimeOffset.Offset);
        return new FinalSpan(endOfYear, _dayOfWeek).InMonth();
    }
}