using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

[TestFixture]
public class BusinessDayHolidayTests
{
    [Test]
    public void IsBusinessDay_WithHoliday_ReturnsFalseForHolidayWeekday()
    {
        var holiday = new DateTime(2023, 12, 25);
        var holidays = new[] { holiday };

        holiday.IsBusinessDay(holidays).ShouldBeFalse();
    }

    [Test]
    public void AddBusinessDays_WithHoliday_SkipsWeekendAndHoliday()
    {
        var friday = new DateTime(2023, 12, 22);
        var holidays = new[] { new DateTime(2023, 12, 25) };

        friday.AddBusinessDays(1, holidays).ShouldBe(new DateTime(2023, 12, 26));
    }

    [Test]
    public void AddBusinessDays_WithHolidayAndNegativeDays_SkipsHolidayWhenMovingBackward()
    {
        var tuesday = new DateTime(2023, 12, 26);
        var holidays = new[] { new DateTime(2023, 12, 25) };

        tuesday.AddBusinessDays(-1, holidays).ShouldBe(new DateTime(2023, 12, 22));
    }

    [Test]
    public void DateTimeOffset_AddBusinessDays_WithHoliday_PreservesOffset()
    {
        var friday = new DateTimeOffset(2023, 12, 22, 9, 0, 0, TimeSpan.FromHours(2));
        var holidays = new[] { new DateTimeOffset(2023, 12, 25, 0, 0, 0, TimeSpan.Zero) };

        friday.AddBusinessDays(1, holidays)
            .ShouldBe(new DateTimeOffset(2023, 12, 26, 9, 0, 0, TimeSpan.FromHours(2)));
    }
}
