using System;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class UnixTimeDateTimeOffsetTests
{
    [Test]
    public void UnixTimestampInSeconds_UtcOffset_ReturnsCorrectTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 0, 0, 0, TimeSpan.Zero);
        (dto.UnixTimestampInSeconds() == 365.0 * 24 * 60 * 60).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInMilliseconds_UtcOffset_ReturnsCorrectTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 0, 0, 0, TimeSpan.Zero);
        (dto.UnixTimestampInMilliseconds() == 365.0 * 24 * 60 * 60 * 1000).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInSeconds_NonZeroOffset_ReturnsUtcTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 5, 0, 0, TimeSpan.FromHours(5));
        (dto.UnixTimestampInSeconds() == 365.0 * 24 * 60 * 60).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInMilliseconds_NonZeroOffset_ReturnsUtcTimestamp()
    {
        var dto = new DateTimeOffset(1971, 01, 01, 5, 0, 0, TimeSpan.FromHours(5));
        (dto.UnixTimestampInMilliseconds() == 365.0 * 24 * 60 * 60 * 1000).VerifyExpression();
    }
}
