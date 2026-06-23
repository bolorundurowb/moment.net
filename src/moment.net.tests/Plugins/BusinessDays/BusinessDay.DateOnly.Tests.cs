using System;
using NUnit.Framework;

namespace MomentNet.Tests.Plugins.BusinessDays;

[TestFixture]
public class BusinessDayDateOnlyTests
{
    [TestCase("2023-10-23", true)]
    [TestCase("2023-10-28", false)]
    public void IsBusinessDay_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        (DateOnly.Parse(dateString).IsBusinessDay() == expected).VerifyExpression();
    }

    [Test]
    public void IsBusinessDay_WithHoliday_ReturnsFalseForHolidayWeekday()
    {
        var holiday = new DateOnly(2023, 12, 25);
        holiday.IsBusinessDay(new[] { holiday }).Verify().ToBeFalse();
    }

    [TestCase("2023-10-28", true)]
    [TestCase("2023-10-23", false)]
    public void IsWeekend_ReturnsExpectedResult(string dateString, bool expected)
    {
        (DateOnly.Parse(dateString).IsWeekend() == expected).VerifyExpression();
    }

    [TestCase("2023-10-23", true)]
    [TestCase("2023-10-28", false)]
    public void IsWeekday_ReturnsExpectedResult(string dateString, bool expected)
    {
        (DateOnly.Parse(dateString).IsWeekday() == expected).VerifyExpression();
    }

    [TestCase("2023-10-20", 1, "2023-10-23")]
    [TestCase("2023-10-23", 0, "2023-10-23")]
    [TestCase("2023-10-23", -1, "2023-10-20")]
    public void AddBusinessDays_VariousCounts_ReturnsExpectedDate(string startDateString, int daysToAdd, string expectedDateString)
    {
        (DateOnly.Parse(startDateString).AddBusinessDays(daysToAdd) == DateOnly.Parse(expectedDateString)).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_WithHoliday_SkipsWeekendAndHoliday()
    {
        var friday = new DateOnly(2023, 12, 22);
        var holidays = new[] { new DateOnly(2023, 12, 25) };

        (friday.AddBusinessDays(1, holidays) == new DateOnly(2023, 12, 26)).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_WithZeroDays_ReturnsSameDate()
    {
        var date = new DateOnly(2023, 10, 23);
        var holidays = new[] { new DateOnly(2023, 12, 25) };

        (date.AddBusinessDays(0, holidays) == date).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_NullHolidays_ThrowsArgumentNullException()
    {
        var date = new DateOnly(2023, 10, 23);
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { date.AddBusinessDays(1, null!); });
    }
}
