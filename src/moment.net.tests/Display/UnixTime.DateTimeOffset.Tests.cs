using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class UnixTimeDateTimeOffsetTests
{
    [Test]
    public void UnixTimestampInSeconds_UtcOffset_ReturnsCorrectTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 0, 0, 0, TimeSpan.Zero);
        dto.UnixTimestampInSeconds().ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimestampInMilliseconds_UtcOffset_ReturnsCorrectTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 0, 0, 0, TimeSpan.Zero);
        dto.UnixTimestampInMilliseconds().ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimestampInSeconds_NonZeroOffset_ReturnsUtcTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 5, 0, 0, TimeSpan.FromHours(5));
        dto.UnixTimestampInSeconds().ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimestampInMilliseconds_NonZeroOffset_ReturnsUtcTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 5, 0, 0, TimeSpan.FromHours(5));
        dto.UnixTimestampInMilliseconds().ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }
}
