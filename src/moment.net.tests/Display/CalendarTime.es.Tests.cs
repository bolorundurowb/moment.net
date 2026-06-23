using NUnit.Framework;
using System;
using System.Globalization;

namespace MomentNet.Tests.Display;

[TestFixture]
public class CalendarTimeEsTests
{
    private static readonly CultureInfo EsCI = CultureInfo.GetCultureInfo("es-AR");

    [Test]
    public void CalendarTime_Today_ReturnsToday()
    {
        var today = DateTime.Now.Date.AddHours(2);
        today.CalendarTime(formats: new CalendarTimeFormats(EsCI)).Verify().ToStartWith("Hoy a las ");
    }

    [Test]
    public void CalendarTime_CalledOnYesterday_ReturnsYesterday()
    {
        var yesterday = DateTime.Now.AddDays(-1);
        yesterday.CalendarTime(formats: new CalendarTimeFormats(EsCI)).Verify().ToStartWith("Ayer a las ");
    }

    [Test]
    public void CalendarTime_CalledOnTomorrow_ReturnsTomorrow()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        tomorrow.CalendarTime(formats: new CalendarTimeFormats(EsCI)).Verify().ToStartWith("Mañana a las ");
    }

    [Test]
    public void CalendarTime_PastDateWithinAWeek_ReturnsLastDayName()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2012, 12, 18);
        initialDate.CalendarTime(nextDate, ci: EsCI).Verify().ToStartWith(initialDate.ToLocalTime().ToString("'el pasado' dddd 'a las' ", EsCI));
    }

    [Test]
    public void CalendarTime_FutureDateWithinAWeek_ReturnsDayName()
    {
        var earlierDate = new DateTime(2012, 12, 12);
        var laterDate = new DateTime(2012, 12, 18);
        laterDate.CalendarTime(earlierDate, ci: EsCI).Verify().ToStartWith(laterDate.ToLocalTime().ToString("dddd 'a las' ", EsCI));
    }

    [Test]
    public void CalendarTime_DateBeyondWeekWithCustomFormat_ReturnsFormattedDate()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2018, 12, 12);
        (initialDate.CalendarTime(nextDate, new CalendarTimeFormats("", "", "", "", "", "dd/MM/yyyy")) == initialDate.ToLocalTime().ToString("dd/MM/yyyy")).VerifyExpression();
    }
}
