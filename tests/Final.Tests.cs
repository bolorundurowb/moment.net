using System;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class FinalTests
{
    string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void FinalInMonthTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.Final().Monday().InMonth().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("26/05/2008 00:00:00");
        date.Final().Monday().InMonth().Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void FinalInYearTest()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.Final().Sunday().InYear().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("28/12/2008 00:00:00");
        date.Final().Sunday().InYear().Kind.ShouldBe(DateTimeKind.Utc);
    }
}