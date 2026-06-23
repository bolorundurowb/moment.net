using NUnit.Framework;
using System;
using System.Globalization;

namespace MomentNet.Tests.Display;

[TestFixture]
public class TimeFromEsTests
{
    private static readonly CultureInfo EsCI = CultureInfo.GetCultureInfo("es-AR");

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        (aFewSecondsAgo.FromNow(EsCI) == "algunos segundos atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        (largeSecondsAgo.FromNow(EsCI) == "un minuto atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTime.UtcNow.AddMinutes(-15);
        (minutesAgo.FromNow(EsCI) == "15 minutos atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        (dateTime.FromNow(EsCI) == "una hora atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        (dateTime.FromNow(EsCI) == "20 horas atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        (dateTime.FromNow(EsCI) == "un día atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        (dateTime.FromNow(EsCI) == "4 días atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        (dateTime.FromNow(EsCI) == "un mes atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        (dateTime.FromNow(EsCI) == "2 meses atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        (dateTime.FromNow(EsCI) == "un año atrás").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        (dateTime.FromNow(EsCI) == "2 años atrás").VerifyExpression();
    }
}
