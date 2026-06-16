using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class UnixTimeDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public UnixTimeDateTimeOffsetTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void UnixTimestampInSeconds_UtcOffset_ReturnsCorrectTimestamp()
    {
        var dateTime = new DateTimeOffset(1971, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTime.UnixTimestampInSeconds().ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimestampInSeconds_PositiveOffset_NormalizesToUtc()
    {
        // Same instant as 1971-01-01T00:00:00Z, expressed in +05:00
        var dateTime = new DateTimeOffset(1971, 1, 1, 5, 0, 0, TimeSpan.FromHours(5));
        dateTime.UnixTimestampInSeconds().ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimestampInSeconds_NegativeOffset_NormalizesToUtc()
    {
        // Same instant as 1971-01-01T00:00:00Z, expressed in -05:00
        var dateTime = new DateTimeOffset(1970, 12, 31, 19, 0, 0, TimeSpan.FromHours(-5));
        dateTime.UnixTimestampInSeconds().ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimestampInMilliseconds_UtcOffset_ReturnsCorrectTimestamp()
    {
        var dateTime = new DateTimeOffset(1971, 1, 1, 0, 0, 0, TimeSpan.Zero);
        dateTime.UnixTimestampInMilliseconds().ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimestampInMilliseconds_PositiveOffset_NormalizesToUtc()
    {
        var dateTime = new DateTimeOffset(1971, 1, 1, 5, 0, 0, TimeSpan.FromHours(5));
        dateTime.UnixTimestampInMilliseconds().ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimestampInSeconds_Epoch_ReturnsZero()
    {
        var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        epoch.UnixTimestampInSeconds().ShouldBe(0.0);
    }

    [Test]
    public void UnixTimestampInMilliseconds_Epoch_ReturnsZero()
    {
        var epoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        epoch.UnixTimestampInMilliseconds().ShouldBe(0.0);
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
