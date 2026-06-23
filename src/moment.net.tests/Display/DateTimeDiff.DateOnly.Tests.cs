using System;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class DateTimeDiffDateOnlyTests
{
    [TestCase("2023-10-23", "2023-10-25", -2.0)]
    [TestCase("2023-10-25", "2023-10-23", 2.0)]
    public void DiffInDays_VariousDates_ReturnsExpectedDifference(string dateString, string otherDateString, double expected)
    {
        (DateOnly.Parse(dateString).DiffInDays(DateOnly.Parse(otherDateString)) == expected).VerifyExpression();
    }

    [TestCase("2023-11-23", "2023-10-23", 1.0)]
    [TestCase("2024-02-29", "2023-02-28", 12.034482758620689)]
    public void DiffInMonths_VariousDates_ReturnsExpectedDifference(string dateString, string otherDateString, double expected)
    {
        DateOnly.Parse(dateString).DiffInMonths(DateOnly.Parse(otherDateString)).Verify().ToBeApproximately(expected, 1e-10);
    }

    [TestCase("2024-07-01", "2024-01-01", 2.0)]
    public void DiffInQuarters_VariousDates_ReturnsExpectedDifference(string dateString, string otherDateString, double expected)
    {
        DateOnly.Parse(dateString).DiffInQuarters(DateOnly.Parse(otherDateString)).Verify().ToBeApproximately(expected, 1e-10);
    }

    [TestCase("2024-10-23", "2023-10-23", 1.0)]
    public void DiffInYears_VariousDates_ReturnsExpectedDifference(string dateString, string otherDateString, double expected)
    {
        DateOnly.Parse(dateString).DiffInYears(DateOnly.Parse(otherDateString)).Verify().ToBeApproximately(expected, 1e-10);
    }
}
