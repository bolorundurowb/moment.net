using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class TimeFromDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public TimeFromDateTimeOffsetTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTimeOffset.UtcNow.AddSeconds(-20);
        aFewSecondsAgo.FromNow().ShouldBe("few seconds ago");
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTimeOffset.UtcNow.AddSeconds(-50);
        largeSecondsAgo.FromNow().ShouldBe("one minute ago");
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTimeOffset.UtcNow.AddMinutes(-15);
        minutesAgo.FromNow().ShouldBe("15 minutes ago");
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddMinutes(-65);
        dateTime.FromNow().ShouldBe("one hour ago");
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddHours(-20);
        dateTime.FromNow().ShouldBe("20 hours ago");
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddHours(-25);
        dateTime.FromNow().ShouldBe("one day ago");
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-4);
        dateTime.FromNow().ShouldBe("4 days ago");
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-27);
        dateTime.FromNow().ShouldBe("one month ago");
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-60);
        dateTime.FromNow().ShouldBe("2 months ago");
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-360);
        dateTime.FromNow().ShouldBe("one year ago");
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-570);
        dateTime.FromNow().ShouldBe("2 years ago");
    }

    [Test]
    public void From_SpecifiedDateTimeOffset_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var twoThousandAndEighteen = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);

        twoThousandAndTwelve.From(twoThousandAndEighteen).ShouldBe("6 years ago");
    }

    [Test]
    public void From_WithDifferentOffsets_NormalizesToUtc()
    {
        // These represent the same instant: 2018-01-01T00:00:00Z
        var utcRef = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var plusFive = new DateTimeOffset(2018, 1, 1, 5, 0, 0, TimeSpan.FromHours(5));

        utcRef.From(plusFive).ShouldBe("6 years ago");
    }

    [Test]
    public void ToNow_FutureDate_ReturnsInPrefix()
    {
        var future = DateTimeOffset.UtcNow.AddDays(4);
        future.ToNow().ShouldBe("in 4 days");
    }

    [Test]
    public void To_SpecifiedDateTimeOffset_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var twoThousandAndEighteen = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);

        twoThousandAndTwelve.To(twoThousandAndEighteen).ShouldBe("in 6 years");
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
