using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class BusinessDayTests
{
    [TestCase("2023-10-23", true)] // Monday
    [TestCase("2023-10-24", true)] // Tuesday
    [TestCase("2023-10-25", true)] // Wednesday
    [TestCase("2023-10-26", true)] // Thursday
    [TestCase("2023-10-27", true)] // Friday
    [TestCase("2023-10-28", false)] // Saturday
    [TestCase("2023-10-29", false)] // Sunday
    public void IsBusinessDayShouldReturnExpectedResult(string dateString, bool expected)
    {
        var date = DateTime.Parse(dateString);
        date.IsBusinessDay().ShouldBe(expected);
    }

    [TestCase("2023-10-20", 1, "2023-10-23")] // Friday to Monday
    [TestCase("2023-10-20", 2, "2023-10-24")] // Friday to Tuesday
    [TestCase("2023-10-23", 1, "2023-10-24")] // Monday to Tuesday
    [TestCase("2023-10-23", 5, "2023-10-30")] // Monday to Monday
    [TestCase("2023-10-21", 1, "2023-10-23")] // Saturday to Monday
    [TestCase("2023-10-22", 1, "2023-10-23")] // Sunday to Monday
    [TestCase("2023-10-23", -1, "2023-10-20")] // Monday to Friday (backwards)
    [TestCase("2023-10-23", 0, "2023-10-23")] // Zero days
    public void AddBusinessDaysShouldReturnExpectedResult(string startDateString, int daysToAdd, string expectedDateString)
    {
        var startDate = DateTime.Parse(startDateString);
        var expectedDate = DateTime.Parse(expectedDateString);
        startDate.AddBusinessDays(daysToAdd).Date.ShouldBe(expectedDate.Date);
    }
}
