using System;
using System.Globalization;
using moment.net.Models;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class CalendarTimeTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;
    public CalendarTimeTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void CalendarTimeSameDay()
    {
        var today = DateTime.Now.Date.AddHours(2);
        today.CalendarTime().ShouldStartWith("Today at ");
    }

    [Test]
    public void CalendarTimeFromYesterday()
    {
        var yesterday = DateTime.Now.AddDays(-1);
        yesterday.CalendarTime().ShouldStartWith("Tomorrow at ");
    }

    [Test]
    public void CalendarTimeFromTomorrow()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        tomorrow.CalendarTime().ShouldStartWith("Yesterday at ");
    }

    [Test]
    public void CalendarTimeFromTwoFixedDates()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2012, 12, 18);
        initialDate.CalendarTime(nextDate).ShouldStartWith(nextDate.ToLocalTime().ToString("dddd 'at' "));
    }

    [Test]
    public void CalendarTimeToTwoFixedDates()
    {
        var earlierDate = new DateTime(2012, 12, 12);
        var laterDate = new DateTime(2012, 12, 18);
        laterDate.CalendarTime(earlierDate).ShouldStartWith(earlierDate.ToLocalTime().ToString("'Last' dddd 'at' "));
    }

    [Test]
    public void CalendarTimeForEcessiveTimeSpanWithSpecifiedFormat()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2018, 12, 12);
        initialDate.CalendarTime(nextDate, new CalendarTimeFormats("", "", "", "", "", "dd/MM/yyyy")).ShouldBe(nextDate.ToLocalTime().ToString("dd/MM/yyyy"));
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}