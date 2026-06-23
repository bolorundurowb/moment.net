using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class FinalTests
{
   
    private static readonly DateTime May2008Utc =
        DateTime.Parse("5/1/2008 8:30:52Z AM",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.AdjustToUniversal);

   
    private static readonly DateTime Dec2008Utc =
        new DateTime(2008, 12, 1, 0, 0, 0, DateTimeKind.Utc);

   
    private static readonly DateTime Feb2008Utc =
        new DateTime(2008, 2, 1, 0, 0, 0, DateTimeKind.Utc);

   
    private static readonly DateTime Feb2009Utc =
        new DateTime(2009, 2, 1, 0, 0, 0, DateTimeKind.Utc);

    [Test]
    public void Final_InMonth_ReturnsLastMondayOfMonth()
    {
        (May2008Utc.Final().Monday().InMonth().ToString("dd/MM/yyyy") == "26/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastTuesdayOfMonth()
    {
        (May2008Utc.Final().Tuesday().InMonth().ToString("dd/MM/yyyy") == "27/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastWednesdayOfMonth()
    {
        (May2008Utc.Final().Wednesday().InMonth().ToString("dd/MM/yyyy") == "28/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastThursdayOfMonth()
    {
        (May2008Utc.Final().Thursday().InMonth().ToString("dd/MM/yyyy") == "29/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastFridayOfMonth()
    {
        (May2008Utc.Final().Friday().InMonth().ToString("dd/MM/yyyy") == "30/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastSaturdayOfMonth()
    {
        (May2008Utc.Final().Saturday().InMonth().ToString("dd/MM/yyyy") == "31/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_ReturnsLastSundayOfMonth()
    {
        (May2008Utc.Final().Sunday().InMonth().ToString("dd/MM/yyyy") == "25/05/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_LeapYearFebruary_ReturnsLastFriday()
    {
        (Feb2008Utc.Final().Friday().InMonth().ToString("dd/MM/yyyy") == "29/02/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_LeapYearFebruary_ReturnsLastMonday()
    {
        (Feb2008Utc.Final().Monday().InMonth().ToString("dd/MM/yyyy") == "25/02/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_NonLeapYearFebruary_ReturnsLastSaturday()
    {
        (Feb2009Utc.Final().Saturday().InMonth().ToString("dd/MM/yyyy") == "28/02/2009").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_NonLeapYearFebruary_ReturnsLastSunday()
    {
        (Feb2009Utc.Final().Sunday().InMonth().ToString("dd/MM/yyyy") == "22/02/2009").VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastMondayOfYear()
    {
        (May2008Utc.Final().Monday().InYear().ToString("dd/MM/yyyy") == "29/12/2008").VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastSundayOfYear()
    {
        (May2008Utc.Final().Sunday().InYear().ToString("dd/MM/yyyy") == "28/12/2008").VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastSaturdayOfYear()
    {
        (May2008Utc.Final().Saturday().InYear().ToString("dd/MM/yyyy") == "27/12/2008").VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastFridayOfYear()
    {
        (May2008Utc.Final().Friday().InYear().ToString("dd/MM/yyyy") == "26/12/2008").VerifyExpression();
    }

    [Test]
    public void Final_InYear_ReturnsLastWednesdayOfYear()
    {
        (May2008Utc.Final().Wednesday().InYear().ToString("dd/MM/yyyy") == "31/12/2008").VerifyExpression();
    }

    [Test]
    public void Final_InMonth_UtcDateTime_PreservesUtcKind()
    {
        (May2008Utc.Final().Monday().InMonth().Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void Final_InYear_UtcDateTime_PreservesUtcKind()
    {
        (May2008Utc.Final().Sunday().InYear().Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void Final_InMonth_LocalDateTime_PreservesLocalKind()
    {
        var local = new DateTime(2008, 5, 1, 8, 30, 52, DateTimeKind.Local);
        (local.Final().Monday().InMonth().Kind == DateTimeKind.Local).VerifyExpression();
    }

    [Test]
    public void Final_InMonth_UnspecifiedDateTime_PreservesUnspecifiedKind()
    {
        var unspecified = new DateTime(2008, 5, 1, 8, 30, 52, DateTimeKind.Unspecified);
        (unspecified.Final().Monday().InMonth().Kind == DateTimeKind.Unspecified).VerifyExpression();
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
        var finalDays = May2008Utc.Final();
        var result = expected switch
        {
            DayOfWeek.Monday    => finalDays.Monday().InMonth(),
            DayOfWeek.Tuesday   => finalDays.Tuesday().InMonth(),
            DayOfWeek.Wednesday => finalDays.Wednesday().InMonth(),
            DayOfWeek.Thursday  => finalDays.Thursday().InMonth(),
            DayOfWeek.Friday    => finalDays.Friday().InMonth(),
            DayOfWeek.Saturday  => finalDays.Saturday().InMonth(),
            DayOfWeek.Sunday    => finalDays.Sunday().InMonth(),
            _ => throw new ArgumentOutOfRangeException()
        };
        (result.DayOfWeek == expected).VerifyExpression();
    }

    [TestCase(DayOfWeek.Monday)]
    [TestCase(DayOfWeek.Tuesday)]
    [TestCase(DayOfWeek.Wednesday)]
    [TestCase(DayOfWeek.Thursday)]
    [TestCase(DayOfWeek.Friday)]
    [TestCase(DayOfWeek.Saturday)]
    [TestCase(DayOfWeek.Sunday)]
    public void Final_InYear_EachDayMethod_ReturnsCorrectDayOfWeek(DayOfWeek expected)
    {
        var finalDays = May2008Utc.Final();
        var result = expected switch
        {
            DayOfWeek.Monday    => finalDays.Monday().InYear(),
            DayOfWeek.Tuesday   => finalDays.Tuesday().InYear(),
            DayOfWeek.Wednesday => finalDays.Wednesday().InYear(),
            DayOfWeek.Thursday  => finalDays.Thursday().InYear(),
            DayOfWeek.Friday    => finalDays.Friday().InYear(),
            DayOfWeek.Saturday  => finalDays.Saturday().InYear(),
            DayOfWeek.Sunday    => finalDays.Sunday().InYear(),
            _ => throw new ArgumentOutOfRangeException()
        };
        (result.DayOfWeek == expected).VerifyExpression();
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
        var finalDays = May2008Utc.Final();
        var result = day switch
        {
            DayOfWeek.Monday    => finalDays.Monday().InYear(),
            DayOfWeek.Tuesday   => finalDays.Tuesday().InYear(),
            DayOfWeek.Wednesday => finalDays.Wednesday().InYear(),
            DayOfWeek.Thursday  => finalDays.Thursday().InYear(),
            DayOfWeek.Friday    => finalDays.Friday().InYear(),
            DayOfWeek.Saturday  => finalDays.Saturday().InYear(),
            DayOfWeek.Sunday    => finalDays.Sunday().InYear(),
            _ => throw new ArgumentOutOfRangeException()
        };
        (result.Month == 12).VerifyExpression();
        (result.Year == 2008).VerifyExpression();
    }
}
