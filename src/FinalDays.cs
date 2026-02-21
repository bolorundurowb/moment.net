using System;

namespace moment.net;

/// <summary>
/// Provides a set of utility methods for determining the last occurrences of specific days of the week
/// within a given month or year, based on a starting <see cref="DateTimeOffset"/> value.
/// </summary>
public class FinalDays
{
    private readonly DateTimeOffset _dateTimeOffset;

    /// <summary>
    /// Represents a class for determining the final occurrences of specific days of the week within a
    /// given month or year, based on a starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    public FinalDays(DateTimeOffset dateTimeOffset) => _dateTimeOffset = dateTimeOffset;

    /// <summary>
    /// Determines the final occurrence of a Monday for a given context, such as the final seven days
    /// within a specified month or year, based on the starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>
    /// A <see cref="FinalSpan"/> instance, which provides methods to calculate the last Monday
    /// in a month or a year.
    /// </returns>
    public FinalSpan Monday() => new(_dateTimeOffset, DayOfWeek.Monday);

    /// <summary>
    /// Determines the final occurrence of a Tuesday for a given context, such as the final seven days
    /// within a specified month or year, based on the starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>
    /// A <see cref="FinalSpan"/> instance, which provides methods to calculate the last Tuesday
    /// in a month or a year.
    /// </returns>
    public FinalSpan Tuesday() => new(_dateTimeOffset, DayOfWeek.Tuesday);

    /// <summary>
    /// Determines the final occurrence of a Wednesday for a given context, such as the last seven days
    /// within a specified month or year, based on the starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>
    /// A <see cref="FinalSpan"/> instance, which provides methods to calculate the last Wednesday
    /// in a month or a year.
    /// </returns>
    public FinalSpan Wednesday() => new(_dateTimeOffset, DayOfWeek.Wednesday);

    /// <summary>
    /// Determines the final occurrence of a Thursday for a given context, such as the final seven days
    /// within a specified month or year, based on the starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>
    /// A <see cref="FinalSpan"/> instance, which provides methods to calculate the last Thursday
    /// in a month or a year.
    /// </returns>
    public FinalSpan Thursday() => new(_dateTimeOffset, DayOfWeek.Thursday);

    /// <summary>
    /// Determines the final occurrence of a Friday for a given context, such as the final seven days
    /// within a specified month or year, based on the starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>
    /// A <see cref="FinalSpan"/> instance, which provides methods to calculate the last Friday
    /// in a month or a year.
    /// </returns>
    public FinalSpan Friday() => new(_dateTimeOffset, DayOfWeek.Friday);

    /// <summary>
    /// Determines the final occurrence of a Saturday for a given context, such as the final seven days
    /// within a specified month or year, based on the starting <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>
    /// A <see cref="FinalSpan"/> instance, which provides methods to calculate the last Saturday
    /// in a month or a year.
    /// </returns>
    public FinalSpan Saturday() => new(_dateTimeOffset, DayOfWeek.Saturday);

    /// <summary>
    /// Determines the last occurrence of Sunday within the final days of a specified month or year,
    /// based on the provided <see cref="DateTimeOffset"/> value.
    /// </summary>
    /// <returns>A <see cref="FinalSpan"/> representing the last occurrence of Sunday within the specified period.</returns>
    public FinalSpan Sunday() => new(_dateTimeOffset, DayOfWeek.Sunday);
}