using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class Next
{
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void NextDayOfWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.Next(DayOfWeek.Thursday).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("08/05/2008 08:30:52");
        date.Next(DayOfWeek.Thursday).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void NextNthDayOfWeekTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.Next(DayOfWeek.Thursday, 3).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("22/05/2008 08:30:52");
        date.Next(DayOfWeek.Thursday, 3).Kind.ShouldBe(DateTimeKind.Utc);
    }
}