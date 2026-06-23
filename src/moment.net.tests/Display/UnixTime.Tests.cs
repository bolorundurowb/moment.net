using System;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class UnixTimeTests
{
    [Test]
    public void UnixTimestampInMilliseconds_UtcDateTime_ReturnsCorrectTimestamp()
    {
        var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds();
        (millisecondsElapsed == 365.0 * 24 * 60 * 60 * 1000).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInMilliseconds_LocalDateTime_ReturnsCorrectTimestamp()
    {
        var oneYearAfterEpochUtc = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var localEquivalent = oneYearAfterEpochUtc.ToLocalTime();
        var millisecondsElapsed = localEquivalent.UnixTimestampInMilliseconds();
        (millisecondsElapsed == 365.0 * 24 * 60 * 60 * 1000).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInSeconds_UtcDateTime_ReturnsCorrectTimestamp()
    {
        var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var secondsElapsed = dateTime.UnixTimestampInSeconds();
        (secondsElapsed == 365.0 * 24 * 60 * 60).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInSeconds_LocalDateTime_ReturnsCorrectTimestamp()
    {
        var oneYearAfterEpochUtc = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var localEquivalent = oneYearAfterEpochUtc.ToLocalTime();
        var secondsElapsed = localEquivalent.UnixTimestampInSeconds();
        (secondsElapsed == 365.0 * 24 * 60 * 60).VerifyExpression();
    }
}
