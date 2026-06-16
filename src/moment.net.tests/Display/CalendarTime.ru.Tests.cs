using MomentNet.Display.Models;
using NUnit.Framework;
using Shouldly;
using System;
using System.Globalization;

namespace MomentNet.Tests.Display;

[TestFixture]
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
    public void CalendarTime_CalledOnYesterday_ReturnsYesterday()
    {
        var yesterday = DateTime.Now.AddDays(-1);
        yesterday.CalendarTime().ShouldStartWith("Вчера в ");
    }

    [Test]
    public void CalendarTime_CalledOnTomorrow_ReturnsTomorrow()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        tomorrow.CalendarTime().ShouldStartWith("Завтра в ");
    }

    [Test]
    public void CalendarTime_PastDateWithinAWeek_ReturnsLastDayName()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2012, 12, 18);
        var ruCI = CultureInfo.GetCultureInfo("ru");
        initialDate.CalendarTime(nextDate).ShouldStartWith(initialDate.ToLocalTime().ToString("'Последний' dddd 'в' ", ruCI));
    }

    [Test]
    public void CalendarTime_FutureDateWithinAWeek_ReturnsDayName()
    {
        var earlierDate = new DateTime(2012, 12, 12);
        var laterDate = new DateTime(2012, 12, 18);
        var ruCI = CultureInfo.GetCultureInfo("ru");
        laterDate.CalendarTime(earlierDate).ShouldStartWith(laterDate.ToLocalTime().ToString("dddd 'в' ", ruCI));
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