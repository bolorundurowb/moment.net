using System;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class EndOfTests
{
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void EndOfMinuteTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.EndOf(DateTimeAnchor.Minute).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:59");
        date.EndOf(DateTimeAnchor.Minute).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void EndOfHourTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.EndOf(DateTimeAnchor.Hour).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:59:59");
        date.EndOf(DateTimeAnchor.Hour).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void EndOfDayTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.EndOf(DateTimeAnchor.Day).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 23:59:59");
        date.EndOf(DateTimeAnchor.Day).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void EndOfWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.EndOf(DateTimeAnchor.Week).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("03/05/2008 23:59:59");
        date.EndOf(DateTimeAnchor.Week).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void EndOfMonthTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.EndOf(DateTimeAnchor.Month).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("31/05/2008 23:59:59");
        date.EndOf(DateTimeAnchor.Month).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void EndOfYearTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.EndOf(DateTimeAnchor.Year).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("31/12/2008 23:59:59");
        date.EndOf(DateTimeAnchor.Year).Kind.ShouldBe(DateTimeKind.Utc);
    }
}