using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class TimeToDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public TimeToDateTimeOffsetTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void ToNow_WithinFewSeconds_ReturnsInFewSeconds()
    {
        var future = DateTimeOffset.UtcNow.AddSeconds(20);
        future.ToNow().ShouldBe("in few seconds");
    }

    [Test]
    public void ToNow_MoreThan45Seconds_ReturnsInOneMinute()
    {
        var future = DateTimeOffset.UtcNow.AddSeconds(50);
        future.ToNow().ShouldBe("in one minute");
    }

    [Test]
    public void ToNow_MultipleMinutes_ReturnsInMinutes()
    {
        var future = DateTimeOffset.UtcNow.AddMinutes(15);
        future.ToNow().ShouldBe("in 15 minutes");
    }

    [Test]
    public void ToNow_NearOneHour_ReturnsInOneHour()
    {
        var future = DateTimeOffset.UtcNow.AddMinutes(65);
        future.ToNow().ShouldBe("in one hour");
    }

    [Test]
    public void ToNow_MultipleHours_ReturnsInHours()
    {
        var future = DateTimeOffset.UtcNow.AddHours(20);
        future.ToNow().ShouldBe("in 20 hours");
    }

    [Test]
    public void ToNow_NearOneDay_ReturnsInOneDay()
    {
        var future = DateTimeOffset.UtcNow.AddHours(25);
        future.ToNow().ShouldBe("in one day");
    }

    [Test]
    public void ToNow_MultipleDays_ReturnsInDays()
    {
        var future = DateTimeOffset.UtcNow.AddDays(4);
        future.ToNow().ShouldBe("in 4 days");
    }

    [Test]
    public void ToNow_NearOneMonth_ReturnsInOneMonth()
    {
        var future = DateTimeOffset.UtcNow.AddDays(27);
        future.ToNow().ShouldBe("in one month");
    }

    [Test]
    public void ToNow_MultipleMonths_ReturnsInMonths()
    {
        var future = DateTimeOffset.UtcNow.AddDays(60);
        future.ToNow().ShouldBe("in 2 months");
    }

    [Test]
    public void ToNow_NearOneYear_ReturnsInOneYear()
    {
        var future = DateTimeOffset.UtcNow.AddDays(360);
        future.ToNow().ShouldBe("in one year");
    }

    [Test]
    public void ToNow_MultipleYears_ReturnsInYears()
    {
        var future = DateTimeOffset.UtcNow.AddDays(570);
        future.ToNow().ShouldBe("in 2 years");
    }

    [Test]
    public void ToNow_ManyYears_ReturnsInYears()
    {
        var future = DateTimeOffset.UtcNow.AddDays(3650);
        future.ToNow().ShouldBe("in 10 years");
    }

    [Test]
    public void To_SpecifiedDateTimeOffset_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var twoThousandAndEighteen = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);

        twoThousandAndTwelve.To(twoThousandAndEighteen).ShouldBe("in 6 years");
    }

    [Test]
    public void To_WithDifferentOffsets_NormalizesToUtc()
    {
        var utcRef = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var plusFive = new DateTimeOffset(2018, 1, 1, 5, 0, 0, TimeSpan.FromHours(5));

        utcRef.To(plusFive).ShouldBe("in 6 years");
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
