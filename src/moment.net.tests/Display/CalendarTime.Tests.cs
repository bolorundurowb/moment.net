using System;
using System.Globalization;
using MomentNet.Display.Models;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class CalendarTimeTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;
    public CalendarTimeTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void CalendarTime_Today_ReturnsToday()
    {
        var today = DateTime.Now.Date.AddHours(2);
        today.CalendarTime().ShouldStartWith("Today at ");
    }

    [Test]
    public void CalendarTime_CalledOnYesterday_ReturnsYesterday()
    {
        var yesterday = DateTime.Now.AddDays(-1);
        yesterday.CalendarTime().ShouldStartWith("Yesterday at ");
    }

    [Test]
    public void CalendarTime_CalledOnTomorrow_ReturnsTomorrow()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        tomorrow.CalendarTime().ShouldStartWith("Tomorrow at ");
    }

    [Test]
    public void CalendarTime_PastDateWithinAWeek_ReturnsLastDayName()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2012, 12, 18);
        var expectedLabel = "'Last' dddd 'at' ";
        initialDate.CalendarTime(nextDate).ShouldStartWith(initialDate.ToLocalTime().ToString(expectedLabel));
    }

    [Test]
    public void CalendarTime_FutureDateWithinAWeek_ReturnsDayName()
    {
        var earlierDate = new DateTime(2012, 12, 12);
        var laterDate = new DateTime(2012, 12, 18);
        laterDate.CalendarTime(earlierDate).ShouldStartWith(laterDate.ToLocalTime().ToString("dddd 'at' "));
    }

    [Test]
    public void CalendarTime_DateBeyondWeekWithCustomFormat_ReturnsFormattedDate()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2018, 12, 12);
        initialDate.CalendarTime(nextDate, new CalendarTimeFormats("", "", "", "", "", "dd/MM/yyyy")).ShouldBe(initialDate.ToLocalTime().ToString("dd/MM/yyyy"));
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}