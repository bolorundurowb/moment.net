using System;
using NUnit.Framework;

namespace MomentNet.Tests.Plugins.BusinessDays;

[TestFixture]
public class BusinessDayHolidayTests
{
    [Test]
    public void IsBusinessDay_WithHoliday_ReturnsFalseForHolidayWeekday()
    {
        var holiday = new DateTime(2023, 12, 25);
        var holidays = new[] { holiday };

        holiday.IsBusinessDay(holidays).Verify().ToBeFalse();
    }

    [Test]
    public void AddBusinessDays_WithHoliday_SkipsWeekendAndHoliday()
    {
        var friday = new DateTime(2023, 12, 22);
        var holidays = new[] { new DateTime(2023, 12, 25) };

        (friday.AddBusinessDays(1, holidays) == new DateTime(2023, 12, 26)).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_WithHolidayAndNegativeDays_SkipsHolidayWhenMovingBackward()
    {
        var tuesday = new DateTime(2023, 12, 26);
        var holidays = new[] { new DateTime(2023, 12, 25) };

        (tuesday.AddBusinessDays(-1, holidays) == new DateTime(2023, 12, 22)).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_WithZeroDays_ReturnsSameDate()
    {
        var date = new DateTime(2023, 10, 23);
        var holidays = new[] { new DateTime(2023, 12, 25) };

        (date.AddBusinessDays(0, holidays) == date).VerifyExpression();
    }

    [Test]
    public void AddBusinessDays_NullHolidays_ThrowsArgumentNullException()
    {
        var date = new DateTime(2023, 10, 23);
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { date.AddBusinessDays(1, null!); });
    }

    [Test]
    public void AddBusinessDays_WhenDateTimeOffsetAndHoliday_PreservesOffset()
    {
        var friday = new DateTimeOffset(2023, 12, 22, 9, 0, 0, TimeSpan.FromHours(2));
        var holidays = new[] { new DateTimeOffset(2023, 12, 25, 0, 0, 0, TimeSpan.Zero) };

        (friday.AddBusinessDays(1, holidays) == new DateTimeOffset(2023, 12, 26, 9, 0, 0, TimeSpan.FromHours(2))).VerifyExpression();
    }
}
