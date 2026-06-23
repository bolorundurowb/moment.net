using System;
using System.Globalization;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class UnixTimeDateOnlyTests
{
    [Test]
    public void UnixTimestampInSeconds_ReturnsCorrectTimestamp()
    {
        var date = new DateOnly(1971, 1, 1);
        (date.UnixTimestampInSeconds() == 365.0 * 24 * 60 * 60).VerifyExpression();
    }

    [Test]
    public void UnixTimestampInMilliseconds_ReturnsCorrectTimestamp()
    {
        var date = new DateOnly(1971, 1, 1);
        (date.UnixTimestampInMilliseconds() == 365.0 * 24 * 60 * 60 * 1000).VerifyExpression();
    }
}

[TestFixture]
public class CalendarTimeDateOnlyTests
{
    private static readonly CalendarTimeFormats InvariantFormats = new(CultureInfo.InvariantCulture);

    [Test]
    public void CalendarTime_WithReferenceDate_FormatsSourceDate()
    {
        var source = new DateOnly(2024, 1, 15);
        var reference = new DateOnly(2024, 1, 15);
        source.CalendarTime(reference, InvariantFormats, CultureInfo.InvariantCulture)
            .Verify().ToContain("Today");
    }

    [Test]
    public void CalendarTime_NoReference_UsesNow()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        today.CalendarTime(InvariantFormats).Verify().ToContain("Today");
    }
}
