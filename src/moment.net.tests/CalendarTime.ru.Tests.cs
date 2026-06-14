using moment.net.Models;
using NUnit.Framework;
using Shouldly;
using System;
using System.Globalization;

namespace moment.net.Tests;

public class CalendarTimeTests_RU : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public CalendarTimeTests_RU() =>
        _cultureWrapper = new CultureWrapper(CultureInfo.GetCultureInfo("ru"));

    [Test]
    public void CalendarTime_Today_ReturnsToday()
    {
        var today = DateTime.Now.Date.AddHours(2);
        today.CalendarTime().ShouldStartWith("Сегодня в ");
    }

    [Test]
    public void CalendarTime_CalledOnYesterday_ReturnsTomorrow()
    {
        var yesterday = DateTime.Now.AddDays(-1);
        yesterday.CalendarTime().ShouldStartWith("Завтра в ");
    }

    [Test]
    public void CalendarTime_CalledOnTomorrow_ReturnsYesterday()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        tomorrow.CalendarTime().ShouldStartWith("Вчера в ");
    }

    [Test]
    public void CalendarTime_FutureDateWithinAWeek_ReturnsDayName()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2012, 12, 18);
        initialDate.CalendarTime(nextDate).ShouldStartWith(nextDate.ToLocalTime().ToString("dddd 'в' ", CultureInfo.GetCultureInfo("ru")));
    }

    [Test]
    public void CalendarTime_PastDateWithinAWeek_ReturnsLastDayName()
    {
        var earlierDate = new DateTime(2012, 12, 12);
        var laterDate = new DateTime(2012, 12, 18);
        laterDate.CalendarTime(earlierDate).ShouldStartWith(earlierDate.ToLocalTime().ToString("'Последний' dddd 'в' ", CultureInfo.GetCultureInfo("ru")));
    }

    [Test]
    public void CalendarTime_DateBeyondWeekWithCustomFormat_ReturnsFormattedDate()
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