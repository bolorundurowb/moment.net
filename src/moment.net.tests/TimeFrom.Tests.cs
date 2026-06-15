using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

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

    // -------------------------------------------------------------------------
    // Boundary tests — exact thresholds from ParseTimeDifference
    // Uses From() with fixed dates so the timespan is precise.
    // Thresholds: ≤44s | 45–89s | 90s–44min | 45–89min | 90min–21h |
    //             22–35h | 36h–25d | 26–45d | 46–319d | 320–547d | >547d
    // -------------------------------------------------------------------------

    private static readonly DateTime Anchor = new DateTime(2020, 6, 1, 12, 0, 0, DateTimeKind.Utc);

    [Test]
    public void From_Exactly44Seconds_ReturnsFewSecondsAgo()
    {
        var earlier = Anchor.AddSeconds(-44);
        earlier.From(Anchor).ShouldBe("few seconds ago");
    }

    [Test]
    public void From_Exactly45Seconds_ReturnsOneMinuteAgo()
    {
        var earlier = Anchor.AddSeconds(-45);
        earlier.From(Anchor).ShouldBe("one minute ago");
    }

    [Test]
    public void From_Exactly89Seconds_ReturnsOneMinuteAgo()
    {
        var earlier = Anchor.AddSeconds(-89);
        earlier.From(Anchor).ShouldBe("one minute ago");
    }

    [Test]
    public void From_Exactly90Seconds_ReturnsMinutesAgo()
    {
        var earlier = Anchor.AddSeconds(-90);
        earlier.From(Anchor).ShouldBe("2 minutes ago");
    }

    [Test]
    public void From_Exactly44Minutes_ReturnsMinutesAgo()
    {
        var earlier = Anchor.AddMinutes(-44);
        earlier.From(Anchor).ShouldBe("44 minutes ago");
    }

    [Test]
    public void From_Exactly45Minutes_ReturnsOneHourAgo()
    {
        var earlier = Anchor.AddMinutes(-45);
        earlier.From(Anchor).ShouldBe("one hour ago");
    }

    [Test]
    public void From_Exactly89Minutes_ReturnsOneHourAgo()
    {
        var earlier = Anchor.AddMinutes(-89);
        earlier.From(Anchor).ShouldBe("one hour ago");
    }

    [Test]
    public void From_Exactly90Minutes_ReturnsHoursAgo()
    {
        var earlier = Anchor.AddMinutes(-90);
        earlier.From(Anchor).ShouldBe("2 hours ago");
    }

    [Test]
    public void From_Exactly21Hours_ReturnsHoursAgo()
    {
        var earlier = Anchor.AddHours(-21);
        earlier.From(Anchor).ShouldBe("21 hours ago");
    }

    [Test]
    public void From_Exactly22Hours_ReturnsOneDayAgo()
    {
        var earlier = Anchor.AddHours(-22);
        earlier.From(Anchor).ShouldBe("one day ago");
    }

    [Test]
    public void From_Exactly35Hours_ReturnsOneDayAgo()
    {
        var earlier = Anchor.AddHours(-35);
        earlier.From(Anchor).ShouldBe("one day ago");
    }

    [Test]
    public void From_Exactly36Hours_ReturnsDaysAgo()
    {
        var earlier = Anchor.AddHours(-36);
        earlier.From(Anchor).ShouldBe("2 days ago");
    }

    [Test]
    public void From_Exactly25Days_ReturnsDaysAgo()
    {
        var earlier = Anchor.AddDays(-25);
        earlier.From(Anchor).ShouldBe("25 days ago");
    }

    [Test]
    public void From_Exactly26Days_ReturnsOneMonthAgo()
    {
        var earlier = Anchor.AddDays(-26);
        earlier.From(Anchor).ShouldBe("one month ago");
    }

    [Test]
    public void From_Exactly45Days_ReturnsOneMonthAgo()
    {
        var earlier = Anchor.AddDays(-45);
        earlier.From(Anchor).ShouldBe("one month ago");
    }

    [Test]
    public void From_Exactly46Days_ReturnsMonthsAgo()
    {
        var earlier = Anchor.AddDays(-46);
        earlier.From(Anchor).ShouldBe("2 months ago");
    }

    [Test]
    public void From_Exactly319Days_ReturnsMonthsAgo()
    {
        var earlier = Anchor.AddDays(-319);
        earlier.From(Anchor).ShouldBe("10 months ago");
    }

    [Test]
    public void From_Exactly320Days_ReturnsOneYearAgo()
    {
        var earlier = Anchor.AddDays(-320);
        earlier.From(Anchor).ShouldBe("one year ago");
    }

    [Test]
    public void From_Exactly547Days_ReturnsOneYearAgo()
    {
        var earlier = Anchor.AddDays(-547);
        earlier.From(Anchor).ShouldBe("one year ago");
    }

    [Test]
    public void From_Exactly548Days_ReturnsYearsAgo()
    {
        var earlier = Anchor.AddDays(-548);
        earlier.From(Anchor).ShouldBe("2 years ago");
    }

    [Test]
    public void From_StartAfterEnd_ReturnsSameResultAsNormalOrder()
    {
        var earlier = Anchor.AddDays(-6);
        earlier.From(Anchor).ShouldBe(Anchor.From(earlier));
    }

    [Test]
    public void FromNow_WithSpanishCulture_ReturnsSpanishRelativeTime()
    {
        var dateTime = DateTime.UtcNow.AddSeconds(-20);
        dateTime.FromNow(CultureInfo.GetCultureInfo("es-AR")).ShouldBe("algunos segundos atrás");
    }

    [Test]
    public void FromNow_WithFrenchCulture_ReturnsLocalizedRelativeTime()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-15);
        dateTime.FromNow(CultureInfo.GetCultureInfo("fr")).ShouldBe("15 minutes il y a");
    }

    [Test]
    public void From_WithSpanishCulture_ReturnsSpanishRelativeTime()
    {
        var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        twoThousandAndTwelve.From(twoThousandAndEighteen, CultureInfo.GetCultureInfo("es-AR"))
            .ShouldBe("6 años atrás");
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
