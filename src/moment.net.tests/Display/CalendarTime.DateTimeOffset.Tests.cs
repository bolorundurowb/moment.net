using System;
using System.Globalization;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class CalendarTimeDateTimeOffsetTests
{
    private static readonly TimeSpan Utc = TimeSpan.Zero;

    [Test]
    public void CalendarTime_SameDay_ReturnsTodayFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end = new DateTimeOffset(2012, 12, 12, 12, 0, 0, Utc);
        start.CalendarTime(end, ci: CultureInfo.InvariantCulture).Verify().ToContain("Today");
    }

    [Test]
    public void CalendarTime_LastDay_ReturnsYesterdayFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end = new DateTimeOffset(2012, 12, 13, 12, 0, 0, Utc);
        start.CalendarTime(end, ci: CultureInfo.InvariantCulture).Verify().ToContain("Yesterday");
    }

    [Test]
    public void CalendarTime_NextDay_ReturnsTomorrowFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end = new DateTimeOffset(2012, 12, 11, 12, 0, 0, Utc);
        start.CalendarTime(end, ci: CultureInfo.InvariantCulture).Verify().ToContain("Tomorrow");
    }

    [Test]
    public void CalendarTime_LastWeek_ReturnsLastDayNameFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end = new DateTimeOffset(2012, 12, 17, 12, 0, 0, Utc);
        start.CalendarTime(end, ci: CultureInfo.InvariantCulture).Verify().ToContain("Wednesday");
    }

    [Test]
    public void CalendarTime_NextWeek_ReturnsDayNameFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end = new DateTimeOffset(2012, 12, 7, 12, 0, 0, Utc);
        start.CalendarTime(end, ci: CultureInfo.InvariantCulture).Verify().ToContain("Wednesday");
    }

    [Test]
    public void CalendarTime_EverythingElse_ReturnsDateFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end = new DateTimeOffset(2012, 11, 1, 12, 0, 0, Utc);
        (start.CalendarTime(end, ci: CultureInfo.InvariantCulture) == "12/12/2012").VerifyExpression();
    }

    [Test]
    public void CalendarTime_CustomFormats_UsesCustomFormat()
    {
        var formats = new CalendarTimeFormats(
            sameDay: "'Same'",
            nextDay: "'Next'",
            nextWeek: "'NextWeek'",
            lastDay: "'Last'",
            lastWeek: "'LastWeek'",
            everythingElse: "'Else'");

        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var sameDay = new DateTimeOffset(2012, 12, 12, 12, 0, 0, Utc);
        (start.CalendarTime(sameDay, formats) == "Same").VerifyExpression();
    }

    [Test]
    public void CalendarTime_NoReference_UsesNow()
    {
        var now = DateTimeOffset.Now;
        now.CalendarTime().Verify().ToContain("Today");
    }
}
