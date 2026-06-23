using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class StartOfDateOnlyTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    private static readonly DateOnly May2008 = new(2008, 5, 15);

    public StartOfDateOnlyTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void StartOf_Day_ReturnsSameDate()
    {
        (May2008.StartOf(DateTimeAnchor.Day) == May2008).VerifyExpression();
    }

    [Test]
    public void StartOf_NoCultureArg_UsesCurrentCulture()
    {
        (May2008.StartOf(DateTimeAnchor.Week) == new DateOnly(2008, 5, 11)).VerifyExpression();
    }

    [Test]
    public void EndOf_NoCultureArg_UsesCurrentCulture()
    {
        (May2008.EndOf(DateTimeAnchor.Month) == new DateOnly(2008, 5, 31)).VerifyExpression();
    }

    [Test]
    public void StartOf_Week_ReturnsPrecedingSunday()
    {
        (May2008.StartOf(DateTimeAnchor.Week) == new DateOnly(2008, 5, 11)).VerifyExpression();
    }

    [Test]
    public void StartOf_IsoWeek_ReturnsPrecedingMonday()
    {
        (May2008.StartOf(DateTimeAnchor.IsoWeek) == new DateOnly(2008, 5, 12)).VerifyExpression();
    }

    [Test]
    public void StartOf_Month_ReturnsFirstDayOfMonth()
    {
        (May2008.StartOf(DateTimeAnchor.Month) == new DateOnly(2008, 5, 1)).VerifyExpression();
    }

    [Test]
    public void StartOf_Quarter_ReturnsFirstDayOfQuarter()
    {
        (May2008.StartOf(DateTimeAnchor.Quarter) == new DateOnly(2008, 4, 1)).VerifyExpression();
    }

    [Test]
    public void StartOf_Year_ReturnsJanuaryFirst()
    {
        (May2008.StartOf(DateTimeAnchor.Year) == new DateOnly(2008, 1, 1)).VerifyExpression();
    }

    [Test]
    public void EndOf_Month_ReturnsLastDayOfMonth()
    {
        (May2008.EndOf(DateTimeAnchor.Month) == new DateOnly(2008, 5, 31)).VerifyExpression();
    }

    [Test]
    public void EndOf_Quarter_ReturnsLastDayOfQuarter()
    {
        (May2008.EndOf(DateTimeAnchor.Quarter) == new DateOnly(2008, 6, 30)).VerifyExpression();
    }

    [Test]
    public void EndOf_IsoWeek_ReturnsFollowingSunday()
    {
        (May2008.EndOf(DateTimeAnchor.IsoWeek) == new DateOnly(2008, 5, 18)).VerifyExpression();
    }

    [Test]
    public void StartOf_InvalidAnchor_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { May2008.StartOf(DateTimeAnchor.Minute); });
    }

    [Test]
    public void EndOf_InvalidAnchor_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { May2008.EndOf(DateTimeAnchor.Hour); });
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}

[TestFixture]
public class StartOfTimeOnlyTests
{
    private static readonly TimeOnly Sample = new(8, 30, 45, 500);

    [Test]
    public void StartOf_Minute_TruncatesSeconds()
    {
        (Sample.StartOf(DateTimeAnchor.Minute) == new TimeOnly(8, 30, 0, 0)).VerifyExpression();
    }

    [Test]
    public void StartOf_Hour_TruncatesMinutesAndSeconds()
    {
        (Sample.StartOf(DateTimeAnchor.Hour) == new TimeOnly(8, 0, 0, 0)).VerifyExpression();
    }

    [Test]
    public void EndOf_Minute_ReturnsEndOfMinute()
    {
        (Sample.EndOf(DateTimeAnchor.Minute) == new TimeOnly(8, 30, 59, 999)).VerifyExpression();
    }

    [Test]
    public void EndOf_Hour_ReturnsEndOfHour()
    {
        (Sample.EndOf(DateTimeAnchor.Hour) == new TimeOnly(8, 59, 59, 999)).VerifyExpression();
    }

    [Test]
    public void StartOf_InvalidAnchor_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Sample.StartOf(DateTimeAnchor.Day); });
    }
}
