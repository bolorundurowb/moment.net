using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class LastDateTimeOffsetTests
{
    // Friday 2024-03-15 at UTC-05:00
    private static readonly DateTimeOffset Friday =
        new DateTimeOffset(2024, 3, 15, 10, 0, 0, TimeSpan.FromHours(-5));

    [Test]
    public void Last_Monday_ReturnsPreviousMonday()
    {
        var result = Friday.Last(DayOfWeek.Monday);
        result.DayOfWeek.ShouldBe(DayOfWeek.Monday);
        result.ToString("dd/MM/yyyy").ShouldBe("11/03/2024");
    }

    [Test]
    public void Last_SameDayOfWeek_SkipsToPreviousWeek()
    {
        var result = Friday.Last(DayOfWeek.Friday);
        result.DayOfWeek.ShouldBe(DayOfWeek.Friday);
        result.ToString("dd/MM/yyyy").ShouldBe("08/03/2024");
    }

    [Test]
    public void Last_PreservesOffset()
    {
        var result = Friday.Last(DayOfWeek.Monday);
        result.Offset.ShouldBe(TimeSpan.FromHours(-5));
    }

    [Test]
    public void Last_WithCount_ReturnsNthPreviousOccurrence()
    {
        var result = Friday.Last(DayOfWeek.Friday, 2);
        result.DayOfWeek.ShouldBe(DayOfWeek.Friday);
        result.ToString("dd/MM/yyyy").ShouldBe("01/03/2024");
    }

    [Test]
    public void Last_WithCount_PreservesOffset()
    {
        var result = Friday.Last(DayOfWeek.Monday, 2);
        result.Offset.ShouldBe(TimeSpan.FromHours(-5));
    }

    [Test]
    public void Last_WithCountOfOne_IsSameAsSingleLast()
    {
        Friday.Last(DayOfWeek.Monday, 1).ShouldBe(Friday.Last(DayOfWeek.Monday));
    }
}
