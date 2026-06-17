using NUnit.Framework;
using Shouldly;
using System;
using System.Globalization;

namespace MomentNet.Tests.Display;

[TestFixture]
public class TimeFromTests_ES
{
    private static readonly CultureInfo EsCI = CultureInfo.GetCultureInfo("es-AR");

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        aFewSecondsAgo.FromNow(EsCI).ShouldBe("algunos segundos atrás");
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        largeSecondsAgo.FromNow(EsCI).ShouldBe("un minuto atrás");
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTime.UtcNow.AddMinutes(-15);
        minutesAgo.FromNow(EsCI).ShouldBe("15 minutos atrás");
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        dateTime.FromNow(EsCI).ShouldBe("una hora atrás");
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        dateTime.FromNow(EsCI).ShouldBe("20 horas atrás");
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        dateTime.FromNow(EsCI).ShouldBe("un día atrás");
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        dateTime.FromNow(EsCI).ShouldBe("4 días atrás");
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        dateTime.FromNow(EsCI).ShouldBe("un mes atrás");
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        dateTime.FromNow(EsCI).ShouldBe("2 meses atrás");
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        dateTime.FromNow(EsCI).ShouldBe("un año atrás");
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        dateTime.FromNow(EsCI).ShouldBe("2 años atrás");
    }
}
