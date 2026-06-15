using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class NextDateTimeOffsetTests
{
    // Friday 2024-03-15 at UTC+05:30
    private static readonly DateTimeOffset Friday =
        new DateTimeOffset(2024, 3, 15, 10, 0, 0, TimeSpan.FromHours(5.5));

    [Test]
    public void Next_Monday_ReturnsFollowingMonday()
    {
        var result = Friday.Next(DayOfWeek.Monday);
        result.DayOfWeek.ShouldBe(DayOfWeek.Monday);
        result.ToString("dd/MM/yyyy").ShouldBe("18/03/2024");
    }

    [Test]
    public void Next_SameDayOfWeek_SkipsToFollowingWeek()
    {
        var result = Friday.Next(DayOfWeek.Friday);
        result.DayOfWeek.ShouldBe(DayOfWeek.Friday);
        result.ToString("dd/MM/yyyy").ShouldBe("22/03/2024");
    }

    [Test]
    public void Next_PreservesOffset()
    {
        var result = Friday.Next(DayOfWeek.Monday);
        result.Offset.ShouldBe(TimeSpan.FromHours(5.5));
    }

    [Test]
    public void Next_WithCount_ReturnsNthOccurrence()
    {
        var result = Friday.Next(DayOfWeek.Monday, 2);
        result.DayOfWeek.ShouldBe(DayOfWeek.Monday);
        result.ToString("dd/MM/yyyy").ShouldBe("25/03/2024");
    }

    [Test]
    public void Next_WithCount_PreservesOffset()
    {
        var result = Friday.Next(DayOfWeek.Monday, 2);
        result.Offset.ShouldBe(TimeSpan.FromHours(5.5));
    }

    [Test]
    public void Next_WithCountOfOne_IsSameAsSingleNext()
    {
        Friday.Next(DayOfWeek.Monday, 1).ShouldBe(Friday.Next(DayOfWeek.Monday));
    }
}
