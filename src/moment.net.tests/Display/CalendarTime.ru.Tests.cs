using NUnit.Framework;
using System;
using System.Globalization;

namespace MomentNet.Tests.Display;

[TestFixture]
public class CalendarTimeRuTests
{
    private static readonly CultureInfo RuCI = CultureInfo.GetCultureInfo("ru");

    [Test]
    public void CalendarTime_Today_ReturnsToday()
    {
        var today = DateTime.Now.Date.AddHours(2);
        today.CalendarTime(formats: new CalendarTimeFormats(RuCI)).Verify().ToStartWith("Сегодня в ");
    }

    [Test]
    public void CalendarTime_CalledOnYesterday_ReturnsYesterday()
    {
        var yesterday = DateTime.Now.AddDays(-1);
        yesterday.CalendarTime(formats: new CalendarTimeFormats(RuCI)).Verify().ToStartWith("Вчера в ");
    }

    [Test]
    public void CalendarTime_CalledOnTomorrow_ReturnsTomorrow()
    {
        var tomorrow = DateTime.Now.AddDays(1);
        tomorrow.CalendarTime(formats: new CalendarTimeFormats(RuCI)).Verify().ToStartWith("Завтра в ");
    }

    [Test]
    public void CalendarTime_PastDateWithinAWeek_ReturnsLastDayName()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2012, 12, 18);
        initialDate.CalendarTime(nextDate, ci: RuCI).Verify().ToStartWith(initialDate.ToLocalTime().ToString("'Последний' dddd 'в' ", RuCI));
    }

    [Test]
    public void CalendarTime_FutureDateWithinAWeek_ReturnsDayName()
    {
        var earlierDate = new DateTime(2012, 12, 12);
        var laterDate = new DateTime(2012, 12, 18);
        laterDate.CalendarTime(earlierDate, ci: RuCI).Verify().ToStartWith(laterDate.ToLocalTime().ToString("dddd 'в' ", RuCI));
    }

    [Test]
    public void CalendarTime_DateBeyondWeekWithCustomFormat_ReturnsFormattedDate()
    {
        var initialDate = new DateTime(2012, 12, 12);
        var nextDate = new DateTime(2018, 12, 12);
        (initialDate.CalendarTime(nextDate, new CalendarTimeFormats("", "", "", "", "", "dd/MM/yyyy")) == initialDate.ToLocalTime().ToString("dd/MM/yyyy")).VerifyExpression();
    }
}
