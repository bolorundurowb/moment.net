using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class FinalDateOnlyTests
{
    private static readonly DateOnly May2008 = new(2008, 5, 1);
    private static readonly DateOnly Feb2008 = new(2008, 2, 1);
    private static readonly DateOnly Feb2009 = new(2009, 2, 1);

    [Test]
    public void Final_InMonth_ReturnsLastMondayOfMonth()
    {
        (May2008.Final().Monday().InMonth() == new DateOnly(2008, 5, 26)).VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastSundayOfMonth()
    {
        (May2008.Final().Sunday().InMonth() == new DateOnly(2008, 5, 25)).VerifyExpression();
    }

    [Test]
    public void Final_InMonth_LeapYearFebruary_ReturnsLastFriday()
    {
        (Feb2008.Final().Friday().InMonth() == new DateOnly(2008, 2, 29)).VerifyExpression();
    }

    [Test]
    public void Final_InMonth_NonLeapYearFebruary_ReturnsLastSaturday()
    {
        (Feb2009.Final().Saturday().InMonth() == new DateOnly(2009, 2, 28)).VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastMondayOfYear()
    {
        (May2008.Final().Monday().InYear() == new DateOnly(2008, 12, 29)).VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastWednesdayOfYear()
    {
        (May2008.Final().Wednesday().InYear() == new DateOnly(2008, 12, 31)).VerifyExpression();
    }

    [TestCase(DayOfWeek.Monday)]
    [TestCase(DayOfWeek.Tuesday)]
    [TestCase(DayOfWeek.Wednesday)]
    [TestCase(DayOfWeek.Thursday)]
    [TestCase(DayOfWeek.Friday)]
    [TestCase(DayOfWeek.Saturday)]
    [TestCase(DayOfWeek.Sunday)]
    public void Final_InMonth_EachDayMethod_ReturnsCorrectDayOfWeek(DayOfWeek expected)
    {
        var finalDays = May2008.Final();
        var result = expected switch
        {
            DayOfWeek.Monday => finalDays.Monday().InMonth(),
            DayOfWeek.Tuesday => finalDays.Tuesday().InMonth(),
            DayOfWeek.Wednesday => finalDays.Wednesday().InMonth(),
            DayOfWeek.Thursday => finalDays.Thursday().InMonth(),
            DayOfWeek.Friday => finalDays.Friday().InMonth(),
            DayOfWeek.Saturday => finalDays.Saturday().InMonth(),
            DayOfWeek.Sunday => finalDays.Sunday().InMonth(),
            _ => throw new ArgumentOutOfRangeException()
        };
        (result.DayOfWeek == expected).VerifyExpression();
    }
}
