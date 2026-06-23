using System;
using NUnit.Framework;

namespace MomentNet.Tests.Plugins.BusinessDays;

[TestFixture]
public class BusinessDayDateTimeOffsetTests
{
    private static readonly TimeSpan PlusThree = TimeSpan.FromHours(3);

    private static DateTimeOffset MakeDate(string dateString) =>
        new DateTimeOffset(DateTime.Parse(dateString), PlusThree);

    [TestCase("2023-10-23", true)] 
    [TestCase("2023-10-24", true)] 
    [TestCase("2023-10-25", true)] 
    [TestCase("2023-10-26", true)] 
    [TestCase("2023-10-27", true)] 
    [TestCase("2023-10-28", false)]
    [TestCase("2023-10-29", false)]
    public void IsBusinessDay_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        (MakeDate(dateString).IsBusinessDay() == expected).VerifyExpression();
    }

    [TestCase("2023-10-23", false)]
    [TestCase("2023-10-27", false)]
    [TestCase("2023-10-28", true)] 
    [TestCase("2023-10-29", true)] 
    public void IsWeekend_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        (MakeDate(dateString).IsWeekend() == expected).VerifyExpression();
    }

    [TestCase("2023-10-23", true)] 
    [TestCase("2023-10-27", true)] 
    [TestCase("2023-10-28", false)]
    [TestCase("2023-10-29", false)]
    public void IsWeekday_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        (MakeDate(dateString).IsWeekday() == expected).VerifyExpression();
    }

    [TestCase("2023-10-20", 1, "2023-10-23")]
    [TestCase("2023-10-20", 2, "2023-10-24")]
    [TestCase("2023-10-23", 1, "2023-10-24")]
    [TestCase("2023-10-23", 5, "2023-10-30")]
    [TestCase("2023-10-21", 1, "2023-10-23")]
    [TestCase("2023-10-22", 1, "2023-10-23")]
    [TestCase("2023-10-23", -1, "2023-10-20")]
    [TestCase("2023-10-23", 0, "2023-10-23")] 
    public void AddBusinessDays_VariousStartDatesAndDayCounts_ReturnsExpectedDate(
        string startDateString, int daysToAdd, string expectedDateString)
    {
        var startDate = MakeDate(startDateString);
        var expectedDate = MakeDate(expectedDateString);
        (startDate.AddBusinessDays(daysToAdd).Date == expectedDate.Date).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_PreservesOffset()
    {
        var date = MakeDate("2023-10-20");
        (date.AddBusinessDays(3).Offset == PlusThree).VerifyExpression();
    }

    [Test]
    public void IsBusinessDay_WithHoliday_ReturnsFalseForHolidayWeekday()
    {
        var holiday = MakeDate("2023-12-25");
        var holidays = new[] { holiday };

        holiday.IsBusinessDay(holidays).Verify().ToBeFalse();
    }

    [Test]
    public void AddBusinessDays_WithZeroDaysAndHolidays_ReturnsSameDate()
    {
        var date = MakeDate("2023-10-23");
        var holidays = new[] { MakeDate("2023-12-25") };

        (date.AddBusinessDays(0, holidays) == date).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_NullHolidays_ThrowsArgumentNullException()
    {
        var date = MakeDate("2023-10-23");
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { date.AddBusinessDays(1, null!); });
    }
}
