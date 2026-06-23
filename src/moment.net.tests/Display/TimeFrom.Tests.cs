using System;
using System.Globalization;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class TimeFromTests
{
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        (aFewSecondsAgo.FromNow(Invariant) == "few seconds ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        (largeSecondsAgo.FromNow(Invariant) == "one minute ago").VerifyExpression();
    }

    [Test]
    public void FromNow_ExactlyOneMinute_ReturnsOneMinuteAgo()
    {
        var afewMinutesAgo = DateTime.Now.AddMinutes(-1).AddSeconds(-1);
        (afewMinutesAgo.FromNow(Invariant) == "one minute ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTime.Now.AddMinutes(-15);
        (minutesAgo.FromNow(Invariant) == "15 minutes ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        (dateTime.FromNow(Invariant) == "one hour ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        (dateTime.FromNow(Invariant) == "20 hours ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        (dateTime.FromNow(Invariant) == "one day ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        (dateTime.FromNow(Invariant) == "4 days ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        (dateTime.FromNow(Invariant) == "one month ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        (dateTime.FromNow(Invariant) == "2 months ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        (dateTime.FromNow(Invariant) == "one year ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        (dateTime.FromNow(Invariant) == "2 years ago").VerifyExpression();
    }

    [Test]
    public void From_SpecifiedDateTime_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        (twoThousandAndTwelve.From(twoThousandAndEighteen, Invariant) == "6 years ago").VerifyExpression();
    }

    [Test]
    public void FromNow_UtcLocal_ReturnsSameRelativeTime()
    {
        var utcNow = DateTime.UtcNow.AddMinutes(-10);
        var now = DateTime.Now.AddMinutes(-10);

        (utcNow.FromNow(Invariant) == now.FromNow(Invariant)).VerifyExpression();
    }

    [TestCase("es", "algunos segundos atrás")]
    [TestCase("fr", "quelques secondes il y a")]
    [TestCase("de", "wenige Sekunden vor")]
    [TestCase("pt", "poucos segundos atrás")]
    [TestCase("ru", "несколько секунд назад")]
    public void FromNow_LocalisedCulture_ReturnsLocalisedString(string cultureName, string expected)
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        (aFewSecondsAgo.FromNow(CultureInfo.GetCultureInfo(cultureName)) == expected).VerifyExpression();
    }
}
