using System;
using System.Globalization;
using MomentNet.Enums;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

public class EndOfDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    // 2008-05-01 08:30:52 at UTC+02:00
    private static readonly DateTimeOffset May2008PlusTwoHours =
        new DateTimeOffset(2008, 5, 1, 8, 30, 52, TimeSpan.FromHours(2));

    public EndOfDateTimeOffsetTests()
    {
        _cultureWrapper = new CultureWrapper(CultureInfo.GetCultureInfo("en-US"));
    }

    [Test]
    public void EndOf_Minute_ReturnsLastSecondAndMillisecondOfMinute()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Minute);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:59");
        result.Millisecond.ShouldBe(999);
    }

    [Test]
    public void EndOf_Minute_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Minute);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void EndOf_Hour_ReturnsLastMinuteSecondAndMillisecondOfHour()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Hour);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:59:59");
        result.Millisecond.ShouldBe(999);
    }

    [Test]
    public void EndOf_Hour_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Hour);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void EndOf_Day_Returns235959999()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Day);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 23:59:59");
        result.Millisecond.ShouldBe(999);
    }

    [Test]
    public void EndOf_Day_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Day);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void EndOf_Week_ReturnsSaturdayAt235959()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Week);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("03/05/2008 23:59:59");
    }

    [Test]
    public void EndOf_Week_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Week);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void EndOf_Month_ReturnsLastDayAt235959()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Month);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("31/05/2008 23:59:59");
    }

    [Test]
    public void EndOf_Month_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Month);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void EndOf_Year_ReturnsDecember31At235959()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Year);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("31/12/2008 23:59:59");
    }

    [Test]
    public void EndOf_Year_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Year);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void EndOf_InvalidAnchor_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => May2008PlusTwoHours.EndOf((DateTimeAnchor)99));
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
