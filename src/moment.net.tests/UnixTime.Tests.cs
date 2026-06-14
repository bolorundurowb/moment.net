using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;
using TimeZoneConverter;

namespace moment.net.Tests;

public class UnixTimeTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public UnixTimeTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

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
        // Create the exact instant one year after the epoch (UTC) and convert to local time
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
        // Create the exact instant one year after the epoch (UTC) and convert to local time
        var oneYearAfterEpochUtc = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var localEquivalent = oneYearAfterEpochUtc.ToLocalTime();
        var secondsElapsed = localEquivalent.UnixTimestampInSeconds();
        secondsElapsed.ShouldBe(365.0 * 24 * 60 * 60);
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}