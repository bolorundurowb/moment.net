using System;
using NUnit.Framework;

namespace MomentNet.Tests.Query;

[TestFixture]
public class PositionalTimeDateOnlyTests
{
    [TestCase(2000, true)]
    [TestCase(1900, false)]
    [TestCase(2024, true)]
    public void IsLeapYear_ReturnsExpectedResult(int year, bool expected)
    {
        (new DateOnly(year, 1, 1).IsLeapYear() == expected).VerifyExpression();
    }

    [Test]
    public void IsSame_EqualDates_ReturnsTrue()
    {
        var date = new DateOnly(2024, 1, 15);
        date.IsSame(new DateOnly(2024, 1, 15)).Verify().ToBeTrue();
    }

    [Test]
    public void IsBefore_EarlierDate_ReturnsTrue()
    {
        new DateOnly(2024, 1, 1).IsBefore(new DateOnly(2024, 1, 2)).Verify().ToBeTrue();
    }

    [Test]
    public void IsSameOrBefore_SameDate_ReturnsTrue()
    {
        var date = new DateOnly(2024, 1, 1);
        date.IsSameOrBefore(date).Verify().ToBeTrue();
    }

    [Test]
    public void IsAfter_LaterDate_ReturnsTrue()
    {
        new DateOnly(2024, 1, 2).IsAfter(new DateOnly(2024, 1, 1)).Verify().ToBeTrue();
    }

    [Test]
    public void IsSameOrAfter_SameDate_ReturnsTrue()
    {
        var date = new DateOnly(2024, 1, 1);
        date.IsSameOrAfter(date).Verify().ToBeTrue();
    }

    [TestCase("2024-01-15", "2024-01-01", "2024-01-31", true)]
    [TestCase("2024-01-01", "2024-01-01", "2024-01-31", true)]
    [TestCase("2024-02-01", "2024-01-01", "2024-01-31", false)]
    public void IsBetween_VariousDates_ReturnsExpectedResult(string dateString, string startString, string endString, bool expected)
    {
        (DateOnly.Parse(dateString).IsBetween(DateOnly.Parse(startString), DateOnly.Parse(endString)) == expected).VerifyExpression();
    }
}

[TestFixture]
public class PositionalTimeTimeOnlyTests
{
    private static readonly TimeOnly Noon = new(12, 0, 0);

    [Test]
    public void IsSame_EqualTimes_ReturnsTrue()
    {
        Noon.IsSame(new TimeOnly(12, 0, 0)).Verify().ToBeTrue();
    }

    [Test]
    public void IsBefore_EarlierTime_ReturnsTrue()
    {
        new TimeOnly(11, 0, 0).IsBefore(Noon).Verify().ToBeTrue();
    }

    [Test]
    public void IsSameOrBefore_SameTime_ReturnsTrue()
    {
        Noon.IsSameOrBefore(Noon).Verify().ToBeTrue();
    }

    [Test]
    public void IsAfter_LaterTime_ReturnsTrue()
    {
        new TimeOnly(13, 0, 0).IsAfter(Noon).Verify().ToBeTrue();
    }

    [Test]
    public void IsSameOrAfter_SameTime_ReturnsTrue()
    {
        Noon.IsSameOrAfter(Noon).Verify().ToBeTrue();
    }

    [TestCase("10:00", "09:00", "11:00", true)]
    [TestCase("09:00", "09:00", "11:00", true)]
    [TestCase("12:00", "09:00", "11:00", false)]
    public void IsBetween_VariousTimes_ReturnsExpectedResult(string timeString, string startString, string endString, bool expected)
    {
        (TimeOnly.Parse(timeString).IsBetween(TimeOnly.Parse(startString), TimeOnly.Parse(endString)) == expected).VerifyExpression();
    }
}
