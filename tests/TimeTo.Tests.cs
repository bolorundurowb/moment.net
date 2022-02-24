using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

public class TimeToTests
{
    [Test]
    public void TimeToAFewSecondsTest()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(20);
        aFewSecondsAgo.ToNow().ShouldBe("in few seconds");
    }

    [Test]
    public void TimeToSecondsMoreThanHalfButLessThanAMinuteTest()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(50);
        largeSecondsAgo.ToNow().ShouldBe("in one minute");
    }

    [Test]
    public void TimeToExactlyAMinuteTest()
    {
        var afewMinutesAgo = DateTime.Now.AddMinutes(1);
        afewMinutesAgo.ToNow().ShouldBe("in one minute");
    }

    [Test]
    public void TimeToADefiniteNumberOfMinutesTest()
    {
        var minutesAgo = DateTime.Now.AddMinutes(15);
        minutesAgo.ToNow().ShouldBe("in 15 minutes");
    }

    [Test]
    public void TimeToMinutesThatCanBeRoundedUpOrDownToAnHourTest()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(65);
        dateTime.ToNow().ShouldBe("in one hour");
    }

    [Test]
    public void TimeToADefiniteNumberOfHoursTest()
    {
        var dateTime = DateTime.UtcNow.AddHours(20);
        dateTime.ToNow().ShouldBe("in 20 hours");
    }

    [Test]
    public void TimeToHoursThatCanBeRoundedUpOrDownToADayTest()
    {
        var dateTime = DateTime.UtcNow.AddHours(25);
        dateTime.ToNow().ShouldBe("in one day");
    }

    [Test]
    public void TimeToADefiniteNumberOfDaysTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(4);
        dateTime.ToNow().ShouldBe("in 4 days");
    }

    [Test]
    public void TimeToDaysThatCanBeRoundedUpOrDownToAMonthTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(27);
        dateTime.ToNow().ShouldBe("in one month");
    }

    [Test]
    public void TimeToMultipleMonthsTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(60);
        dateTime.ToNow().ShouldBe("in 2 months");
    }

    [Test]
    public void TimeToDaysAddingUpToAYearTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(360);
        dateTime.ToNow().ShouldBe("in one year");
    }

    [Test]
    public void TimeToMultipleYearsTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(570);
        dateTime.ToNow().ShouldBe("in 2 years");
    }

    [Test]
    public void TimeToMultipleYearsV2Test()
    {
        var dateTime = DateTime.UtcNow.AddDays(3650);
        dateTime.ToNow().ShouldBe("in 10 years");
    }

    [Test]
    public void TimeToASpecifiedDateTimeTest()
    {
        var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        twoThousandAndTwelve.To(twoThousandAndEighteen).ShouldBe("in 6 years");
    }
}