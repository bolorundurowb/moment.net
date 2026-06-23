using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Plugins.BusinessDays;

[TestFixture]
public class BusinessDayDateOnlyTests
{
    [TestCase("2023-10-23", true)]
    [TestCase("2023-10-28", false)]
    public void IsBusinessDay_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        DateOnly.Parse(dateString).IsBusinessDay().ShouldBe(expected);
    }

    [Test]
    public void IsBusinessDay_WithHoliday_ReturnsFalseForHolidayWeekday()
    {
        var holiday = new DateOnly(2023, 12, 25);
        holiday.IsBusinessDay(new[] { holiday }).ShouldBeFalse();
    }

    [TestCase("2023-10-28", true)]
    [TestCase("2023-10-23", false)]
    public void IsWeekend_ReturnsExpectedResult(string dateString, bool expected)
    {
        DateOnly.Parse(dateString).IsWeekend().ShouldBe(expected);
    }

    [TestCase("2023-10-23", true)]
    [TestCase("2023-10-28", false)]
    public void IsWeekday_ReturnsExpectedResult(string dateString, bool expected)
    {
        DateOnly.Parse(dateString).IsWeekday().ShouldBe(expected);
    }

    [TestCase("2023-10-20", 1, "2023-10-23")]
    [TestCase("2023-10-23", 0, "2023-10-23")]
    [TestCase("2023-10-23", -1, "2023-10-20")]
    public void AddBusinessDays_VariousCounts_ReturnsExpectedDate(string startDateString, int daysToAdd, string expectedDateString)
    {
        DateOnly.Parse(startDateString).AddBusinessDays(daysToAdd).ShouldBe(DateOnly.Parse(expectedDateString));
    }

    [Test]
    public void AddBusinessDays_WithHoliday_SkipsWeekendAndHoliday()
    {
        var friday = new DateOnly(2023, 12, 22);
        var holidays = new[] { new DateOnly(2023, 12, 25) };

        friday.AddBusinessDays(1, holidays).ShouldBe(new DateOnly(2023, 12, 26));
    }

    [Test]
    public void AddBusinessDays_WithZeroDays_ReturnsSameDate()
    {
        var date = new DateOnly(2023, 10, 23);
        var holidays = new[] { new DateOnly(2023, 12, 25) };

        date.AddBusinessDays(0, holidays).ShouldBe(date);
    }

    [Test]
    public void AddBusinessDays_NullHolidays_ThrowsArgumentNullException()
    {
        var date = new DateOnly(2023, 10, 23);
        Should.Throw<ArgumentNullException>(() => date.AddBusinessDays(1, null!));
    }
}
