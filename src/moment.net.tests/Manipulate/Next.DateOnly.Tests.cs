using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class NextDateOnlyTests
{
    private static readonly DateOnly Thursday = new(2008, 5, 1);

    [Test]
    public void Next_DayOfWeek_ReturnsNextOccurrence()
    {
        (Thursday.Next(DayOfWeek.Thursday) == new DateOnly(2008, 5, 8)).VerifyExpression();
    }

    [Test]
    public void Next_NthDayOfWeek_ReturnsNthFutureOccurrence()
    {
        (Thursday.Next(DayOfWeek.Thursday, 3) == new DateOnly(2008, 5, 22)).VerifyExpression();
    }

    [Test]
    public void Next_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { Thursday.Next(DayOfWeek.Thursday, 0); });
    }
}

[TestFixture]
public class LastDateOnlyTests
{
    private static readonly DateOnly Thursday = new(2008, 5, 1);

    [Test]
    public void Last_DayOfWeek_ReturnsPreviousOccurrence()
    {
        (Thursday.Last(DayOfWeek.Thursday) == new DateOnly(2008, 4, 24)).VerifyExpression();
    }

    [Test]
    public void Last_NthDayOfWeek_ReturnsNthPreviousOccurrence()
    {
        (Thursday.Last(DayOfWeek.Thursday, 3) == new DateOnly(2008, 4, 10)).VerifyExpression();
    }

    [Test]
    public void Last_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { Thursday.Last(DayOfWeek.Thursday, 0); });
    }
}
