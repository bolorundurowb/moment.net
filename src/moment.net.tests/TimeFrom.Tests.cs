using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class TimeFromTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public TimeFromTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        aFewSecondsAgo.FromNow().ShouldBe("few seconds ago");
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        largeSecondsAgo.FromNow().ShouldBe("one minute ago");
    }

    [Test]
    public void FromNow_ExactlyOneMinute_ReturnsOneMinuteAgo()
    {
        var aFewMinutesAgo = DateTime.Now.AddMinutes(-1);
        aFewMinutesAgo.FromNow().ShouldBe("one minute ago");
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTime.Now.AddMinutes(-15);
        minutesAgo.FromNow().ShouldBe("15 minutes ago");
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        dateTime.FromNow().ShouldBe("one hour ago");
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        dateTime.FromNow().ShouldBe("20 hours ago");
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        dateTime.FromNow().ShouldBe("one day ago");
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        dateTime.FromNow().ShouldBe("4 days ago");
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        dateTime.FromNow().ShouldBe("one month ago");
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        dateTime.FromNow().ShouldBe("2 months ago");
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        dateTime.FromNow().ShouldBe("one year ago");
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        dateTime.FromNow().ShouldBe("2 years ago");
    }

    [Test]
    public void FromNow_ManyYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-3650);
        dateTime.FromNow().ShouldBe("10 years ago");
    }

    [Test]
    public void From_SpecifiedDateTime_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        twoThousandAndTwelve.From(twoThousandAndEighteen).ShouldBe("6 years ago");
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}