using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;
using TimeZoneConverter;

namespace moment.net.Tests;

public class UnixTimeTests : IDisposable
{
    private CultureWrapper _cultureWrapper;

    public UnixTimeTests()
    {
        _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);
    }

    [Test]
    public void UnixTimeInMillisecondsOneYearFromEpoch()
    {
        var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds();
        millisecondsElapsed.ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimeInMillisecondsOneLocalYearFromEpoch()
    {
        // var tz = TZConvert.GetTimeZoneInfo("W. Central Africa Standard Time");
        var tz = TZConvert.GetTimeZoneInfo("Pacific Standard Time");
        // var dateTime = TimeZoneInfo.ConvertTime(new DateTime(1971, 01, 01, 1, 0, 0, DateTimeKind.Local), tz);
        var dateTime = TimeZoneInfo.ConvertTime(new DateTime(1971, 01, 01, 8, 0, 0, DateTimeKind.Local), tz);
        var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds();
        millisecondsElapsed.ShouldBe(365.0 * 24 * 60 * 60 * 1000);
    }

    [Test]
    public void UnixTimeInSecondsOneUtcYearFromEpoch()
    {
        var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
        var secondsElapsed = dateTime.UnixTimestampInSeconds();
        secondsElapsed.ShouldBe(365.0 * 24 * 60 * 60);
    }

    [Test]
    public void UnixTimeInSecondsOneLocalYearFromEpoch()
    {
        // var tz = TZConvert.GetTimeZoneInfo("W. Central Africa Standard Time");
        var tz = TZConvert.GetTimeZoneInfo("Pacific Standard Time");
        // var dateTime = TimeZoneInfo.ConvertTime(new DateTime(1971, 01, 01, 1, 0, 0, DateTimeKind.Local), tz);
        var dateTime = TimeZoneInfo.ConvertTime(new DateTime(1971, 01, 01, 8, 0, 0, DateTimeKind.Local), tz);
        var secondsElapsed = dateTime.UnixTimestampInSeconds();
        secondsElapsed.ShouldBe(365.0 * 24 * 60 * 60);
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}