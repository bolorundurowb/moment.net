using System;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class FirstLastDateInWeek
{
    string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void FirstDateInWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.FirstDateInWeek().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
        date.FirstDateInWeek().Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void LastDateInWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.LastDateInWeek().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("03/05/2008 00:00:00");
        date.LastDateInWeek().Kind.ShouldBe(DateTimeKind.Utc);
    }

}