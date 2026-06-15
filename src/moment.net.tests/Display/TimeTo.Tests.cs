using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

public class TimeToTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public TimeToTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void ToNow_WithinFewSeconds_ReturnsInFewSeconds()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(20);
        aFewSecondsAgo.ToNow().ShouldBe("in few seconds");
    }

    [Test]
    public void ToNow_MoreThan45Seconds_ReturnsInOneMinute()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(50);
        largeSecondsAgo.ToNow().ShouldBe("in one minute");
    }

    [Test]
    public void ToNow_ExactlyOneMinute_ReturnsInOneMinute()
    {
        var afewMinutesAgo = DateTime.Now.AddMinutes(1);
        afewMinutesAgo.ToNow().ShouldBe("in one minute");
    }

    [Test]
    public void ToNow_MultipleMinutes_ReturnsInMinutes()
    {
        var minutesAgo = DateTime.Now.AddMinutes(15);
        minutesAgo.ToNow().ShouldBe("in 15 minutes");
    }

    [Test]
    public void ToNow_NearOneHour_ReturnsInOneHour()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(65);
        dateTime.ToNow().ShouldBe("in one hour");
    }

    [Test]
    public void ToNow_MultipleHours_ReturnsInHours()
    {
        var dateTime = DateTime.UtcNow.AddHours(20);
        dateTime.ToNow().ShouldBe("in 20 hours");
    }

    [Test]
    public void ToNow_NearOneDay_ReturnsInOneDay()
    {
        var dateTime = DateTime.UtcNow.AddHours(25);
        dateTime.ToNow().ShouldBe("in one day");
    }

    [Test]
    public void ToNow_MultipleDays_ReturnsInDays()
    {
        var dateTime = DateTime.UtcNow.AddDays(4);
        dateTime.ToNow().ShouldBe("in 4 days");
    }

    [Test]
    public void ToNow_NearOneMonth_ReturnsInOneMonth()
    {
        var dateTime = DateTime.UtcNow.AddDays(27);
        dateTime.ToNow().ShouldBe("in one month");
    }

    [Test]
    public void ToNow_MultipleMonths_ReturnsInMonths()
    {
        var dateTime = DateTime.UtcNow.AddDays(60);
        dateTime.ToNow().ShouldBe("in 2 months");
    }

    [Test]
    public void ToNow_NearOneYear_ReturnsInOneYear()
    {
        var dateTime = DateTime.UtcNow.AddDays(360);
        dateTime.ToNow().ShouldBe("in one year");
    }

    [Test]
    public void ToNow_MultipleYears_ReturnsInYears()
    {
        var dateTime = DateTime.UtcNow.AddDays(570);
        dateTime.ToNow().ShouldBe("in 2 years");
    }

    [Test]
    public void ToNow_ManyYears_ReturnsInYears()
    {
        var dateTime = DateTime.UtcNow.AddDays(3650);
        dateTime.ToNow().ShouldBe("in 10 years");
    }

    [Test]
    public void To_SpecifiedDateTime_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        twoThousandAndTwelve.To(twoThousandAndEighteen).ShouldBe("in 6 years");
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}