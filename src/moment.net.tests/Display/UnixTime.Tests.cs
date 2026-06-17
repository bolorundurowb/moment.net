using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class UnixTimeTests
{
    [Test]
    public void UnixTimestampInMilliseconds_UtcDateTime_ReturnsCorrectTimestamp()
    {
        var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds();
        millisecondsElapsed.ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimestampInMilliseconds_LocalDateTime_ReturnsCorrectTimestamp()
    {
        var oneYearAfterEpochUtc = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var localEquivalent = oneYearAfterEpochUtc.ToLocalTime();
        var millisecondsElapsed = localEquivalent.UnixTimestampInMilliseconds();
        millisecondsElapsed.ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimestampInSeconds_UtcDateTime_ReturnsCorrectTimestamp()
    {
        var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var secondsElapsed = dateTime.UnixTimestampInSeconds();
        secondsElapsed.ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimestampInSeconds_LocalDateTime_ReturnsCorrectTimestamp()
    {
        var oneYearAfterEpochUtc = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var localEquivalent = oneYearAfterEpochUtc.ToLocalTime();
        var secondsElapsed = localEquivalent.UnixTimestampInSeconds();
        secondsElapsed.ShouldBe(365.0 * 24 * 60 * 60);
    }
}
