using System;
using System.Globalization;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class TimeFromDateTimeOffsetTests
{
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    [Test]
    public void FromNow_WithinFewSeconds_ReturnsFewSecondsAgo()
    {
        var aFewSecondsAgo = DateTimeOffset.UtcNow.AddSeconds(-20);
        (aFewSecondsAgo.FromNow(Invariant) == "few seconds ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MoreThan45Seconds_ReturnsOneMinuteAgo()
    {
        var largeSecondsAgo = DateTimeOffset.UtcNow.AddSeconds(-50);
        (largeSecondsAgo.FromNow(Invariant) == "one minute ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleMinutes_ReturnsMinutesAgo()
    {
        var minutesAgo = DateTimeOffset.UtcNow.AddMinutes(-15);
        (minutesAgo.FromNow(Invariant) == "15 minutes ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneHour_ReturnsOneHourAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddMinutes(-65);
        (dateTime.FromNow(Invariant) == "one hour ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleHours_ReturnsHoursAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddHours(-20);
        (dateTime.FromNow(Invariant) == "20 hours ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneDay_ReturnsOneDayAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddHours(-25);
        (dateTime.FromNow(Invariant) == "one day ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleDays_ReturnsDaysAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-4);
        (dateTime.FromNow(Invariant) == "4 days ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneMonth_ReturnsOneMonthAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-27);
        (dateTime.FromNow(Invariant) == "one month ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleMonths_ReturnsMonthsAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-60);
        (dateTime.FromNow(Invariant) == "2 months ago").VerifyExpression();
    }

    [Test]
    public void FromNow_NearOneYear_ReturnsOneYearAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-360);
        (dateTime.FromNow(Invariant) == "one year ago").VerifyExpression();
    }

    [Test]
    public void FromNow_MultipleYears_ReturnsYearsAgo()
    {
        var dateTime = DateTimeOffset.UtcNow.AddDays(-570);
        (dateTime.FromNow(Invariant) == "2 years ago").VerifyExpression();
    }

    [Test]
    public void From_SpecifiedDateTimeOffset_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var twoThousandAndEighteen = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);

        (twoThousandAndTwelve.From(twoThousandAndEighteen, Invariant) == "6 years ago").VerifyExpression();
    }

    [Test]
    public void From_WithDifferentOffsets_NormalizesToUtc()
    {
        var utcRef = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var plusFive = new DateTimeOffset(2018, 1, 1, 5, 0, 0, TimeSpan.FromHours(5));

        (utcRef.From(plusFive, Invariant) == "6 years ago").VerifyExpression();
    }

    [Test]
    public void ToNow_FutureDate_ReturnsInPrefix()
    {
        var future = DateTimeOffset.UtcNow.AddDays(4);
        (future.ToNow(Invariant) == "in 4 days").VerifyExpression();
    }

    [Test]
    public void To_SpecifiedDateTimeOffset_ReturnsCorrectRelativeTime()
    {
        var twoThousandAndTwelve = new DateTimeOffset(2012, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var twoThousandAndEighteen = new DateTimeOffset(2018, 1, 1, 0, 0, 0, TimeSpan.Zero);

        (twoThousandAndTwelve.To(twoThousandAndEighteen, Invariant) == "in 6 years").VerifyExpression();
    }
}
