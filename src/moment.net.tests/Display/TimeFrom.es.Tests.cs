using NUnit.Framework;
using Shouldly;
using System;
using System.Globalization;

namespace MomentNet.Tests.Display;

[TestFixture]
public class TimeFromTests_ES : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public TimeFromTests_ES() => _cultureWrapper = new CultureWrapper(CultureInfo.GetCultureInfo("es-AR")); // spanish argentina

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        aFewSecondsAgo.FromNow().ShouldBe("algunos segundos atrás");
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        largeSecondsAgo.FromNow().ShouldBe("un minuto atrás");
    }

    [Test]
    public void FromNow_ExactlyOneMinute_ReturnsOneMinuteAgo()
    {
        var aFewMinutesAgo = DateTime.Now.AddMinutes(-1);
        aFewMinutesAgo.FromNow().ShouldBe("un minuto atrás");
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTime.Now.AddMinutes(-15);
        minutesAgo.FromNow().ShouldBe("15 minutos atrás");
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        dateTime.FromNow().ShouldBe("una hora atrás");
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        dateTime.FromNow().ShouldBe("20 horas atrás");
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        dateTime.FromNow().ShouldBe("un día atrás");
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        dateTime.FromNow().ShouldBe("4 días atrás");
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        dateTime.FromNow().ShouldBe("un mes atrás");
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        dateTime.FromNow().ShouldBe("2 meses atrás");
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        dateTime.FromNow().ShouldBe("un año atrás");
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        dateTime.FromNow().ShouldBe("2 años atrás");
    }

    [Test]
    public void FromNow_ManyYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-3650);
        dateTime.FromNow().ShouldBe("10 años atrás");
    }

    [Test]
    public void From_SpecifiedDateTime_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        twoThousandAndTwelve.From(twoThousandAndEighteen).ShouldBe("6 años atrás");
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}