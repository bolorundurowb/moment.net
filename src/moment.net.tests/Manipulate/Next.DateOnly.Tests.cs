using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class NextDateOnlyTests
{
    private static readonly DateOnly Thursday = new(2008, 5, 1);

    [Test]
    public void Next_DayOfWeek_ReturnsNextOccurrence()
    {
        Thursday.Next(DayOfWeek.Thursday).ShouldBe(new DateOnly(2008, 5, 8));
    }

    [Test]
    public void Next_NthDayOfWeek_ReturnsNthFutureOccurrence()
    {
        Thursday.Next(DayOfWeek.Thursday, 3).ShouldBe(new DateOnly(2008, 5, 22));
    }

    [Test]
    public void Next_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Thursday.Next(DayOfWeek.Thursday, 0));
    }
}

[TestFixture]
public class LastDateOnlyTests
{
    private static readonly DateOnly Thursday = new(2008, 5, 1);

    [Test]
    public void Last_DayOfWeek_ReturnsPreviousOccurrence()
    {
        Thursday.Last(DayOfWeek.Thursday).ShouldBe(new DateOnly(2008, 4, 24));
    }

    [Test]
    public void Last_NthDayOfWeek_ReturnsNthPreviousOccurrence()
    {
        Thursday.Last(DayOfWeek.Thursday, 3).ShouldBe(new DateOnly(2008, 4, 10));
    }

    [Test]
    public void Last_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => Thursday.Last(DayOfWeek.Thursday, 0));
    }
}
