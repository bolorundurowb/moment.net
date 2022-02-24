using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class PositionalTimeTests
{
    [TestCase(1700)]
    [TestCase(1900)]
    [TestCase(2046)]
    public void IsLeapYearShouldReturnFalseWhenYearIsNot(int year)
    {
        var date = DateTime.Parse($"{year}-01-02");
        var result = date.IsLeapYear();

        result.ShouldBeFalse();
    }

    [TestCase(1992)]
    [TestCase(2000)]
    public void IsLeapYearShouldReturnTrueWhenYearIs(int year)
    {
        var date = DateTime.Parse($"{year}-01-02");
        var result = date.IsLeapYear();

        result.ShouldBeTrue();
    }

    [TestCase("1972-01-01", "1972-01-01")]
    [TestCase("2022-01-01T00:00:00+01:00", "2021-12-31T23:00:00.000Z")]
    [TestCase("2022-01-01T23:00:00-01:00", "2022-01-02T03:00:00+03:00")]
    public void IsSameShouldReturnTrueWhenDatesAreSame(string first, string second)
    {
        var date = DateTime.Parse(first);
        var comparison = DateTime.Parse(second);
        var result = date.IsSame(comparison);

        result.ShouldBeTrue();
    }

    [TestCase("1972-01-01", "1972-01-02")]
    [TestCase("2022-01-01T00:00:00+01:00", "2022-01-01T23:00:00.000Z")]
    [TestCase("2022-01-02T23:00:00-01:00", "2022-01-02T03:00:00+03:00")]
    public void IsSameShouldReturnFalseWhenDatesAreNotSame(string first, string second)
    {
        var date = DateTime.Parse(first);
        var comparison = DateTime.Parse(second);
        var result = date.IsSame(comparison);

        result.ShouldBeFalse();
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(1000)]
    [TestCase(100000)]
    public void IsBeforeShouldReturnTrueWhenDatesIsAfter(int timeToAdd)
    {
        var date = DateTime.Now;
        var comparison = date - TimeSpan.FromSeconds(timeToAdd);
        var result = comparison.IsBefore(date);

        result.ShouldBeTrue();
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(1000)]
    [TestCase(100000)]
    public void IsSameOrBeforeShouldReturnTrueWhenDatesIsAfter(int timeToAdd)
    {
        var date = DateTime.Now;
        var comparison = date - TimeSpan.FromSeconds(timeToAdd);
        var result = comparison.IsSameOrBefore(date);

        result.ShouldBeTrue();
    }

    [TestCase(1)]
    [TestCase(10)]
    [TestCase(1000)]
    [TestCase(100000)]
    public void IsAfterShouldReturnTrueWhenDatesIsAfter(int timeToAdd)
    {
        var date = DateTime.Now;
        var comparison = date + TimeSpan.FromSeconds(timeToAdd);
        var result = comparison.IsAfter(date);

        result.ShouldBeTrue();
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(10)]
    [TestCase(1000)]
    [TestCase(100000)]
    public void IsSameOrAfterShouldReturnTrueWhenDatesIsAfter(int timeToAdd)
    {
        var date = DateTime.Now;
        var comparison = date + TimeSpan.FromSeconds(timeToAdd);
        var result = comparison.IsSameOrAfter(date);

        result.ShouldBeTrue();
    }
}