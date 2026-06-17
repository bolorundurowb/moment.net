using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class RelativeTimeThresholdTests
{
    [TestCase(0, "few seconds")]
    [TestCase(44, "few seconds")]
    [TestCase(45, "one minute")]
    [TestCase(89, "one minute")]
    [TestCase(90, "2 minutes")]
    public void FromNow_SecondsThresholds(int seconds, string expected)
    {
        var now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var past = now.AddSeconds(-seconds);
        past.From(now, true, CultureInfo.InvariantCulture).ShouldBe(expected);
    }

    [TestCase(44, "44 minutes")]
    [TestCase(45, "one hour")]
    [TestCase(89, "one hour")]
    [TestCase(90, "2 hours")]
    public void FromNow_MinutesThresholds(int minutes, string expected)
    {
        var now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var past = now.AddMinutes(-minutes);
        past.From(now, true, CultureInfo.InvariantCulture).ShouldBe(expected);
    }

    [TestCase(21, "21 hours")]
    [TestCase(22, "one day")]
    [TestCase(35, "one day")]
    [TestCase(36, "2 days")]
    public void FromNow_HoursThresholds(int hours, string expected)
    {
        var now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var past = now.AddHours(-hours);
        past.From(now, true, CultureInfo.InvariantCulture).ShouldBe(expected);
    }

    [TestCase(25, "25 days")]
    [TestCase(26, "one month")]
    [TestCase(45, "one month")]
    [TestCase(46, "2 months")]
    public void FromNow_DaysThresholds(int days, string expected)
    {
        var now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var past = now.AddDays(-days);
        past.From(now, true, CultureInfo.InvariantCulture).ShouldBe(expected);
    }

    [TestCase(319, "10 months")]
    [TestCase(320, "one year")]
    [TestCase(547, "one year")]
    [TestCase(548, "2 years")]
    public void FromNow_YearsThresholds(int days, string expected)
    {
        var now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var past = now.AddDays(-days);
        past.From(now, true, CultureInfo.InvariantCulture).ShouldBe(expected);
    }

    [Test]
    public void FromNow_LocalTime_ReturnsCorrectRelativeTime()
    {
        var past = DateTime.Now.AddMinutes(-5);
        past.FromNow(true, CultureInfo.InvariantCulture).ShouldBe("5 minutes");
    }

    [Test]
    public void ToNow_LocalTime_ReturnsCorrectRelativeTime()
    {
        var future = DateTime.Now.AddMinutes(5);
        future.ToNow(true, CultureInfo.InvariantCulture).ShouldBe("5 minutes");
    }
}
