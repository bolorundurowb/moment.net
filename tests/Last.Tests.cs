using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class Last
{
    string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void LastDayOfWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.Last(DayOfWeek.Thursday).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("24/04/2008 08:30:52");
        date.Last(DayOfWeek.Thursday).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void LastNthDayOfWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.Last(DayOfWeek.Thursday, 3).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("10/04/2008 08:30:52");
        date.Last(DayOfWeek.Thursday, 3).Kind.ShouldBe(DateTimeKind.Utc);
    }
}