using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class FinalDateTimeOffsetTests
{
    private static readonly TimeSpan PlusFive = TimeSpan.FromHours(5);

    // May 2008: Sat=31, Fri=30, Thu=29, Wed=28, Tue=27, Mon=26, Sun=25
    private static readonly DateTimeOffset May2008 =
        new DateTimeOffset(2008, 5, 1, 8, 30, 52, PlusFive);

    // Dec 2008: Wed=31, Tue=30, Mon=29, Sun=28, Sat=27, Fri=26, Thu=25
    private static readonly DateTimeOffset Dec2008 =
        new DateTimeOffset(2008, 12, 1, 0, 0, 0, PlusFive);

    // Feb 2008 (leap, 29 days): Fri=29, Thu=28, Wed=27, Tue=26, Mon=25, Sun=24, Sat=23
    private static readonly DateTimeOffset Feb2008 =
        new DateTimeOffset(2008, 2, 1, 0, 0, 0, PlusFive);

    // Feb 2009 (non-leap, 28 days): Sat=28, Fri=27, Thu=26, Wed=25, Tue=24, Mon=23, Sun=22
    private static readonly DateTimeOffset Feb2009 =
        new DateTimeOffset(2009, 2, 1, 0, 0, 0, PlusFive);

    [Test]
    public void Final_InMonth_ReturnsLastMondayOfMonth()
    {
        May2008.Final().Monday().InMonth().ToString("dd/MM/yyyy").ShouldBe("26/05/2008");
    }

    [Test]
    public void Final_InMonth_ReturnsLastTuesdayOfMonth()
    {
        May2008.Final().Tuesday().InMonth().ToString("dd/MM/yyyy").ShouldBe("27/05/2008");
    }

    [Test]
    public void Final_InMonth_ReturnsLastWednesdayOfMonth()
    {
        May2008.Final().Wednesday().InMonth().ToString("dd/MM/yyyy").ShouldBe("28/05/2008");
    }

    [Test]
    public void Final_InMonth_ReturnsLastThursdayOfMonth()
    {
        May2008.Final().Thursday().InMonth().ToString("dd/MM/yyyy").ShouldBe("29/05/2008");
    }

    [Test]
    public void Final_InMonth_ReturnsLastFridayOfMonth()
    {
        May2008.Final().Friday().InMonth().ToString("dd/MM/yyyy").ShouldBe("30/05/2008");
    }

    [Test]
    public void Final_InMonth_ReturnsLastSaturdayOfMonth()
    {
        May2008.Final().Saturday().InMonth().ToString("dd/MM/yyyy").ShouldBe("31/05/2008");
    }

    [Test]
    public void Final_InMonth_ReturnsLastSundayOfMonth()
    {
        May2008.Final().Sunday().InMonth().ToString("dd/MM/yyyy").ShouldBe("25/05/2008");
    }

    [Test]
    public void Final_InMonth_LeapYearFebruary_ReturnsLastFriday()
    {
        Feb2008.Final().Friday().InMonth().ToString("dd/MM/yyyy").ShouldBe("29/02/2008");
    }

    [Test]
    public void Final_InMonth_LeapYearFebruary_ReturnsLastMonday()
    {
        Feb2008.Final().Monday().InMonth().ToString("dd/MM/yyyy").ShouldBe("25/02/2008");
    }

    [Test]
    public void Final_InMonth_NonLeapYearFebruary_ReturnsLastSaturday()
    {
        Feb2009.Final().Saturday().InMonth().ToString("dd/MM/yyyy").ShouldBe("28/02/2009");
    }

    [Test]
    public void Final_InMonth_NonLeapYearFebruary_ReturnsLastSunday()
    {
        Feb2009.Final().Sunday().InMonth().ToString("dd/MM/yyyy").ShouldBe("22/02/2009");
    }

    [Test]
    public void Final_InYear_ReturnsLastMondayOfYear()
    {
        May2008.Final().Monday().InYear().ToString("dd/MM/yyyy").ShouldBe("29/12/2008");
    }

    [Test]
    public void Final_InYear_ReturnsLastSundayOfYear()
    {
        May2008.Final().Sunday().InYear().ToString("dd/MM/yyyy").ShouldBe("28/12/2008");
    }

    [Test]
    public void Final_InYear_ReturnsLastWednesdayOfYear()
    {
        May2008.Final().Wednesday().InYear().ToString("dd/MM/yyyy").ShouldBe("31/12/2008");
    }

    [Test]
    public void Final_InMonth_PreservesOffset()
    {
        May2008.Final().Monday().InMonth().Offset.ShouldBe(PlusFive);
    }

    [Test]
    public void Final_InYear_PreservesOffset()
    {
        May2008.Final().Sunday().InYear().Offset.ShouldBe(PlusFive);
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
        result.DayOfWeek.ShouldBe(expected);
    }

    [TestCase(DayOfWeek.Monday)]
    [TestCase(DayOfWeek.Tuesday)]
    [TestCase(DayOfWeek.Wednesday)]
    [TestCase(DayOfWeek.Thursday)]
    [TestCase(DayOfWeek.Friday)]
    [TestCase(DayOfWeek.Saturday)]
    [TestCase(DayOfWeek.Sunday)]
    public void Final_InYear_AllDays_ResultIsInDecember(DayOfWeek day)
    {
        var finalDays = May2008.Final();
        var result = day switch
        {
            DayOfWeek.Monday => finalDays.Monday().InYear(),
            DayOfWeek.Tuesday => finalDays.Tuesday().InYear(),
            DayOfWeek.Wednesday => finalDays.Wednesday().InYear(),
            DayOfWeek.Thursday => finalDays.Thursday().InYear(),
            DayOfWeek.Friday => finalDays.Friday().InYear(),
            DayOfWeek.Saturday => finalDays.Saturday().InYear(),
            DayOfWeek.Sunday => finalDays.Sunday().InYear(),
            _ => throw new ArgumentOutOfRangeException()
        };
        result.Month.ShouldBe(12);
        result.Year.ShouldBe(2008);
    }
}
