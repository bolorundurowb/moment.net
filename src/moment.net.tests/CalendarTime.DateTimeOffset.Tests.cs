using System;
using System.Globalization;
using MomentNet.Models;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

public class CalendarTimeDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public CalendarTimeDateTimeOffsetTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    private static readonly TimeSpan Utc = TimeSpan.Zero;

    [Test]
    public void CalendarTime_SameDay_ReturnsTodayFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end   = new DateTimeOffset(2012, 12, 12, 12, 0, 0, Utc);
        start.CalendarTime(end).ShouldContain("Today");
    }

    [Test]
    public void CalendarTime_NextDay_ReturnsTomorrowFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end   = new DateTimeOffset(2012, 12, 13, 12, 0, 0, Utc);
        start.CalendarTime(end).ShouldContain("Tomorrow");
    }

    [Test]
    public void CalendarTime_LastDay_ReturnsYesterdayFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end   = new DateTimeOffset(2012, 12, 11, 12, 0, 0, Utc);
        start.CalendarTime(end).ShouldContain("Yesterday");
    }

    [Test]
    public void CalendarTime_NextWeek_ReturnsDayNameFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc); // Wednesday
        var end   = new DateTimeOffset(2012, 12, 17, 12, 0, 0, Utc); // Monday (5 days ahead)
        start.CalendarTime(end).ShouldContain("Monday");
    }

    [Test]
    public void CalendarTime_LastWeek_ReturnsLastDayNameFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc); // Wednesday
        var end   = new DateTimeOffset(2012, 12, 7, 12, 0, 0, Utc);  // Friday (5 days before)
        start.CalendarTime(end).ShouldContain("Friday");
    }

    [Test]
    public void CalendarTime_EverythingElse_ReturnsDateFormat()
    {
        var start = new DateTimeOffset(2012, 12, 12, 0, 0, 0, Utc);
        var end   = new DateTimeOffset(2012, 11, 1, 12, 0, 0, Utc);
        start.CalendarTime(end).ShouldBe("11/01/2012");
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
        start.CalendarTime(sameDay, formats).ShouldBe("Same");
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
