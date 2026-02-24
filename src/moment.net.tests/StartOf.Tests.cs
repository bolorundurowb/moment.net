using System;
using System.Globalization;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class StartOfTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    public StartOfTests()
    {
        // Ensure week calculations assume Sunday as first day (en-US)
        _cultureWrapper = new CultureWrapper(CultureInfo.GetCultureInfo("en-US"));
    }

    [Test]
    public void StartOfMinuteTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Minute).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:00");
        date.StartOf(DateTimeAnchor.Minute).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOfHourTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Hour).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:00:00");
        date.StartOf(DateTimeAnchor.Hour).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOfDayTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Day).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Day).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOfWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Week).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Week).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOfMonthTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Month).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Month).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOfYearTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Year).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/01/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Year).Kind.ShouldBe(DateTimeKind.Utc);
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}