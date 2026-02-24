using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class DateTimeDiffTests
{
    [TestCase("2023-10-23", "2023-10-25", -2.0)]
    [TestCase("2023-10-25", "2023-10-23", 2.0)]
    [TestCase("2023-10-23", "2023-10-23", 0.0)]
    public void DiffInDaysShouldReturnExpectedResult(string dateString, string otherDateString, double expected)
    {
        var date = DateTime.Parse(dateString);
        var otherDate = DateTime.Parse(otherDateString);
        date.DiffInDays(otherDate).ShouldBe(expected);
    }

    [TestCase("2023-10-23", "2023-11-23", -1.0)]
    [TestCase("2023-11-23", "2023-10-23", 1.0)]
    [TestCase("2023-10-23", "2023-10-23", 0.0)]
    [TestCase("2024-02-29", "2023-02-28", 12.035714285714286)]
    public void DiffInMonthsShouldReturnExpectedResult(string dateString, string otherDateString, double expected)
    {
        var date = DateTime.Parse(dateString);
        var otherDate = DateTime.Parse(otherDateString);
        date.DiffInMonths(otherDate).ShouldBe(expected, 1e-10);
    }

    [TestCase("2023-10-23", "2024-10-23", -1.0)]
    [TestCase("2024-10-23", "2023-10-23", 1.0)]
    [TestCase("2023-10-23", "2023-10-23", 0.0)]
    [TestCase("2024-02-29", "2020-02-29", 4.0)]
    public void DiffInYearsShouldReturnExpectedResult(string dateString, string otherDateString, double expected)
    {
        var date = DateTime.Parse(dateString);
        var otherDate = DateTime.Parse(otherDateString);
        date.DiffInYears(otherDate).ShouldBe(expected, 1e-10);
    }
}
