using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class DateTimeDiffDateTimeOffsetTests
{
    private static DateTimeOffset MakeUtc(string dateString) =>
        new DateTimeOffset(DateTime.Parse(dateString), TimeSpan.Zero);

    [TestCase("2023-10-23", "2023-10-25", -2.0)]
    [TestCase("2023-10-25", "2023-10-23",  2.0)]
    [TestCase("2023-10-23", "2023-10-23",  0.0)]
    public void DiffInDays_VariousDates_ReturnsExpectedDifference(
        string dateString, string otherDateString, double expected)
    {
        MakeUtc(dateString).DiffInDays(MakeUtc(otherDateString)).ShouldBe(expected);
    }

    [Test]
    public void DiffInDays_CrossOffset_NormalizesToUtc()
    {
        // Both represent the same UTC instant → diff should be 0
        var utc    = new DateTimeOffset(2023, 10, 23, 0, 0, 0, TimeSpan.Zero);
        var plusTwo = new DateTimeOffset(2023, 10, 23, 2, 0, 0, TimeSpan.FromHours(2));
        utc.DiffInDays(plusTwo).ShouldBe(0.0);
    }

    [TestCase("2023-10-23", "2023-11-23", -1.0)]
    [TestCase("2023-11-23", "2023-10-23",  1.0)]
    [TestCase("2023-10-23", "2023-10-23",  0.0)]
    [TestCase("2024-02-29", "2023-02-28", 12.035714285714286)]
    public void DiffInMonths_VariousDates_ReturnsExpectedDifference(
        string dateString, string otherDateString, double expected)
    {
        MakeUtc(dateString).DiffInMonths(MakeUtc(otherDateString)).ShouldBe(expected, 1e-10);
    }

    [TestCase("2023-10-23", "2024-10-23", -1.0)]
    [TestCase("2024-10-23", "2023-10-23",  1.0)]
    [TestCase("2023-10-23", "2023-10-23",  0.0)]
    [TestCase("2024-02-29", "2020-02-29",  4.0)]
    public void DiffInYears_VariousDates_ReturnsExpectedDifference(
        string dateString, string otherDateString, double expected)
    {
        MakeUtc(dateString).DiffInYears(MakeUtc(otherDateString)).ShouldBe(expected, 1e-10);
    }
}
