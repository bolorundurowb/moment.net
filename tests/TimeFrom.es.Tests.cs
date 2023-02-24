using NUnit.Framework;
using Shouldly;
using System;
using System.Globalization;

namespace moment.net.Tests;

public class TimeFromTests_ES : IDisposable
{
    private CultureWrapper _cultureWrapper;

    public TimeFromTests_ES()
    {
        _cultureWrapper = new CultureWrapper(CultureInfo.GetCultureInfo("es-AR")); // spanish argentina
    }

    [Test]
    public void TimeFromAFewSecondsTest()
    {
        var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
        aFewSecondsAgo.FromNow().ShouldBe("algunos segundos atrás");
    }

    [Test]
    public void TimeFromSecondsMoreThanHalfButLessThanAMinuteTest()
    {
        var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
        largeSecondsAgo.FromNow().ShouldBe("un minuto atrás");
    }

    [Test]
    public void TimeFromExactlyAMinuteTest()
    {
        var aFewMinutesAgo = DateTime.Now.AddMinutes(-1);
        aFewMinutesAgo.FromNow().ShouldBe("un minuto atrás");
    }

    [Test]
    public void TimeFromADefiniteNumberOfMinutesTest()
    {
        var minutesAgo = DateTime.Now.AddMinutes(-15);
        minutesAgo.FromNow().ShouldBe("15 minutos atrás");
    }

    [Test]
    public void TimeFromMinutesThatCanBeRoundedUpOrDownToAnHourTest()
    {
        var dateTime = DateTime.UtcNow.AddMinutes(-65);
        dateTime.FromNow().ShouldBe("una hora atrás");
    }

    [Test]
    public void TimeFromADefiniteNumberOfHoursTest()
    {
        var dateTime = DateTime.UtcNow.AddHours(-20);
        dateTime.FromNow().ShouldBe("20 horas atrás");
    }

    [Test]
    public void TimeFromHoursThatCanBeRoundedUpOrDownToADayTest()
    {
        var dateTime = DateTime.UtcNow.AddHours(-25);
        dateTime.FromNow().ShouldBe("un día atrás");
    }

    [Test]
    public void TimeFromADefiniteNumberOfDaysTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-4);
        dateTime.FromNow().ShouldBe("4 días atrás");
    }

    [Test]
    public void TimeFromDaysThatCanBeRoundedUpOrDownToAMonthTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-27);
        dateTime.FromNow().ShouldBe("un mes atrás");
    }

    [Test]
    public void TimeFromMultipleMonthsTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-60);
        dateTime.FromNow().ShouldBe("2 meses atrás");
    }

    [Test]
    public void TimeFromDaysAddingUpToAYearTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-360);
        dateTime.FromNow().ShouldBe("un año atrás");
    }

    [Test]
    public void TimeFromMultipleYearsTest()
    {
        var dateTime = DateTime.UtcNow.AddDays(-570);
        dateTime.FromNow().ShouldBe("2 años atrás");
    }

    [Test]
    public void TimeFromMultipleYearsV2Test()
    {
        var dateTime = DateTime.UtcNow.AddDays(-3650);
        dateTime.FromNow().ShouldBe("10 años atrás");
    }

    [Test]
    public void TimeFromASpecifiedDateTimeTest()
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