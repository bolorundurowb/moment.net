using System;
using System.Globalization;
using System.Threading;
using MomentNet.Manipulate;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class StartOfDateTimeOffsetTests : IDisposable
{
    private readonly CultureInfo _originalCulture;

    private static readonly DateTimeOffset May2008PlusTwoHours =
        new DateTimeOffset(2008, 5, 1, 8, 30, 52, TimeSpan.FromHours(2));

    public StartOfDateTimeOffsetTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void StartOf_Minute_TruncatesSecondsToZero()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Minute);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:00");
    }

    [Test]
    public void StartOf_Minute_PreservesOffset()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Minute);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void StartOf_Hour_TruncatesMinutesAndSecondsToZero()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Hour);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:00:00");
    }

    [Test]
    public void StartOf_Hour_PreservesOffset()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Hour);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void StartOf_Day_TruncatesTimeToMidnight()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Day);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
    }

    [Test]
    public void StartOf_Day_PreservesOffset()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Day);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void StartOf_Week_ReturnsPrecedingSunday()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Week);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
    }

    [Test]
    public void StartOf_Week_PreservesOffset()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Week);
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void StartOf_IsoWeek_ReturnsPrecedingMonday()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.IsoWeek);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("28/04/2008 00:00:00");
    }

    [Test]
    public void StartOf_Quarter_ReturnsFirstDayOfQuarter()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Quarter);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/04/2008 00:00:00");
    }

    [Test]
    public void StartOf_Month_ReturnsFirstDayAtMidnight()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Month);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
    }

    [Test]
    public void StartOf_Year_ReturnsJanuaryFirstAtMidnight()
    {
        var result = May2008PlusTwoHours.StartOf(DateTimeAnchor.Year);
        result.ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/01/2008 00:00:00");
    }

    [Test]
    public void StartOf_InvalidAnchor_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => May2008PlusTwoHours.StartOf((DateTimeAnchor)99));
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
