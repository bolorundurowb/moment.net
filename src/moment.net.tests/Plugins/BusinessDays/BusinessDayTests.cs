using System;
using NUnit.Framework;

namespace MomentNet.Tests.Plugins.BusinessDays;

[TestFixture]
public class BusinessDayTests
{
    [TestCase("2023-10-23", true)]
    [TestCase("2023-10-24", true)]
    [TestCase("2023-10-25", true)]
    [TestCase("2023-10-26", true)]
    [TestCase("2023-10-27", true)]
    [TestCase("2023-10-28", false)]
    [TestCase("2023-10-29", false)]
    public void IsBusinessDay_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        var date = DateTime.Parse(dateString);
        (date.IsBusinessDay() == expected).VerifyExpression();
    }

    [TestCase("2023-10-23", false)]
    [TestCase("2023-10-27", false)]
    [TestCase("2023-10-28", true)]
    [TestCase("2023-10-29", true)]
    public void IsWeekend_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        var date = DateTime.Parse(dateString);
        (date.IsWeekend() == expected).VerifyExpression();
    }

    [TestCase("2023-10-23", true)]
    [TestCase("2023-10-27", true)]
    [TestCase("2023-10-28", false)]
    [TestCase("2023-10-29", false)]
    public void IsWeekday_WeekdaysAndWeekends_ReturnsExpectedResult(string dateString, bool expected)
    {
        var date = DateTime.Parse(dateString);
        (date.IsWeekday() == expected).VerifyExpression();
    }

    [TestCase("2023-10-20", 1, "2023-10-23")]
    [TestCase("2023-10-20", 2, "2023-10-24")]
    [TestCase("2023-10-23", 1, "2023-10-24")]
    [TestCase("2023-10-23", 5, "2023-10-30")]
    [TestCase("2023-10-21", 1, "2023-10-23")]
    [TestCase("2023-10-22", 1, "2023-10-23")]
    [TestCase("2023-10-23", -1, "2023-10-20")]
    [TestCase("2023-10-23", 0, "2023-10-23")]
    public void AddBusinessDays_VariousStartDatesAndDayCounts_ReturnsExpectedDate(string startDateString, int daysToAdd, string expectedDateString)
    {
        var startDate = DateTime.Parse(startDateString);
        var expectedDate = DateTime.Parse(expectedDateString);
        (startDate.AddBusinessDays(daysToAdd).Date == expectedDate.Date).VerifyExpression();
    }
}
