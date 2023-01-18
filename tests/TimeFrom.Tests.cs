using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class TimeFromTests : IDisposable
{
    private CultureWrapper _cultureWrapper;
    public TimeFromTests()
    {
        _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);
    }

    [Test]
    public void TimeFromAFewSecondsTest()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        aFewSecondsAgo.FromNow().ShouldBe("few seconds ago");
    }

    [Test]
    public void TimeFromSecondsMoreThanHalfButLessThanAMinuteTest()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        largeSecondsAgo.FromNow().ShouldBe("one minute ago");
    }

    [Test]
    public void TimeFromExactlyAMinuteTest()
    {
        var aFewMinutesAgo = DateTime.Now.AddMinutes(-1);
        aFewMinutesAgo.FromNow().ShouldBe("one minute ago");
    }

    [Test]
    public void TimeFromADefiniteNumberOfMinutesTest()
    {
        var minutesAgo = DateTime.Now.AddMinutes(-15);
        minutesAgo.FromNow().ShouldBe("15 minutes ago");
    }

    [Test]
    public void TimeFromMinutesThatCanBeRoundedUpOrDownToAnHourTest()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        dateTime.FromNow().ShouldBe("one hour ago");
    }

    [Test]
    public void TimeFromADefiniteNumberOfHoursTest()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        dateTime.FromNow().ShouldBe("20 hours ago");
    }

    [Test]
    public void TimeFromHoursThatCanBeRoundedUpOrDownToADayTest()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        dateTime.FromNow().ShouldBe("one day ago");
    }

    [Test]
    public void TimeFromADefiniteNumberOfDaysTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        dateTime.FromNow().ShouldBe("4 days ago");
    }

    [Test]
    public void TimeFromDaysThatCanBeRoundedUpOrDownToAMonthTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        dateTime.FromNow().ShouldBe("one month ago");
    }

    [Test]
    public void TimeFromMultipleMonthsTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        dateTime.FromNow().ShouldBe("2 months ago");
    }

    [Test]
    public void TimeFromDaysAddingUpToAYearTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        dateTime.FromNow().ShouldBe("one year ago");
    }

    [Test]
    public void TimeFromMultipleYearsTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        dateTime.FromNow().ShouldBe("2 years ago");
    }

    [Test]
    public void TimeFromMultipleYearsV2Test()
    {
        var dateTime = DateTime.UtcNow.AddDays(-3650);
        dateTime.FromNow().ShouldBe("10 years ago");
    }

    [Test]
    public void TimeFromASpecifiedDateTimeTest()
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