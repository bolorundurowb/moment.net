using System;
using NUnit.Framework;

namespace MomentNet.Tests.Query;

[TestFixture]
public class PositionalTimeDateTimeOffsetTests
{
    [TestCase(1700)]
    [TestCase(1900)]
    [TestCase(2046)]
    public void IsLeapYear_NonLeapYear_ReturnsFalse(int year)
    {
        var date = new DateTimeOffset(year, 1, 2, 0, 0, 0, TimeSpan.Zero);
        date.IsLeapYear().Verify().ToBeFalse();
    }

    [TestCase(1992)]
    [TestCase(2000)]
    public void IsLeapYear_LeapYear_ReturnsTrue(int year)
    {
        var date = new DateTimeOffset(year, 1, 2, 0, 0, 0, TimeSpan.Zero);
        date.IsLeapYear().Verify().ToBeTrue();
    }

    [Test]
    public void IsSame_IdenticalInstants_SameOffset_ReturnsTrue()
    {
        var a = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var b = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        a.IsSame(b).Verify().ToBeTrue();
    }

    [Test]
    public void IsSame_IdenticalInstants_DifferentOffsets_ReturnsTrue()
    {
       
        var utc = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var plusOne = new DateTimeOffset(2022, 1, 1, 1, 0, 0, TimeSpan.FromHours(1));
        utc.IsSame(plusOne).Verify().ToBeTrue();
    }

    [Test]
    public void IsSame_DifferentInstants_ReturnsFalse()
    {
        var a = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var b = new DateTimeOffset(2022, 1, 2, 0, 0, 0, TimeSpan.Zero);
        a.IsSame(b).Verify().ToBeFalse();
    }

    [TestCase(1)]
    [TestCase(100)]
    [TestCase(100000)]
    public void IsBefore_EarlierInstant_ReturnsTrue(int secondsToAdd)
    {
        var reference = new DateTimeOffset(2023, 6, 1, 0, 0, 0, TimeSpan.Zero);
        var earlier = reference.AddSeconds(-secondsToAdd);
        earlier.IsBefore(reference).Verify().ToBeTrue();
    }

    [Test]
    public void IsBefore_CrossOffset_ComparesInstants()
    {
       
        var utc = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var plusOne = new DateTimeOffset(2022, 1, 1, 1, 0, 0, TimeSpan.FromHours(1));
        utc.IsBefore(plusOne).Verify().ToBeFalse();
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100000)]
    public void IsSameOrBefore_SameOrEarlierInstant_ReturnsTrue(int secondsToAdd)
    {
        var reference = new DateTimeOffset(2023, 6, 1, 0, 0, 0, TimeSpan.Zero);
        var comparison = reference.AddSeconds(-secondsToAdd);
        comparison.IsSameOrBefore(reference).Verify().ToBeTrue();
    }

    [TestCase(1)]
    [TestCase(100)]
    [TestCase(100000)]
    public void IsAfter_LaterInstant_ReturnsTrue(int secondsToAdd)
    {
        var reference = new DateTimeOffset(2023, 6, 1, 0, 0, 0, TimeSpan.Zero);
        var later = reference.AddSeconds(secondsToAdd);
        later.IsAfter(reference).Verify().ToBeTrue();
    }

    [TestCase(0)]
    [TestCase(1)]
    [TestCase(100000)]
    public void IsSameOrAfter_SameOrLaterInstant_ReturnsTrue(int secondsToAdd)
    {
        var reference = new DateTimeOffset(2023, 6, 1, 0, 0, 0, TimeSpan.Zero);
        var comparison = reference.AddSeconds(secondsToAdd);
        comparison.IsSameOrAfter(reference).Verify().ToBeTrue();
    }

    [TestCase("2023-10-23", "2023-10-20", "2023-10-25", true)]
    [TestCase("2023-10-20", "2023-10-20", "2023-10-25", true)]
    [TestCase("2023-10-25", "2023-10-20", "2023-10-25", true)]
    [TestCase("2023-10-19", "2023-10-20", "2023-10-25", false)]
    [TestCase("2023-10-26", "2023-10-20", "2023-10-25", false)]
    public void IsBetween_VariousDates_ReturnsExpectedResult(
        string dateString, string startString, string endString, bool expected)
    {
        var offset = TimeSpan.FromHours(2);
        var date  = new DateTimeOffset(DateTime.Parse(dateString), offset);
        var start = new DateTimeOffset(DateTime.Parse(startString), offset);
        var end   = new DateTimeOffset(DateTime.Parse(endString), offset);
        (date.IsBetween(start, end) == expected).VerifyExpression();
    }

    [Test]
    public void IsBetween_CrossOffsetBoundaries_ComparesInstants()
    {
       
       
        var date  = new DateTimeOffset(2023, 10, 23, 0, 0, 0, TimeSpan.FromHours(2));
        var start = new DateTimeOffset(2023, 10, 22, 22, 0, 0, TimeSpan.Zero);
        var end   = new DateTimeOffset(2023, 10, 24, 0, 0, 0, TimeSpan.Zero);
        date.IsBetween(start, end).Verify().ToBeTrue();
    }
}
