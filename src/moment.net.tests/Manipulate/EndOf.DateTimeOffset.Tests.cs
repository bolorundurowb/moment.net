using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class EndOfDateTimeOffsetTests : IDisposable
{
    private readonly CultureInfo _originalCulture;

    private static readonly DateTimeOffset May2008PlusTwoHours =
        new DateTimeOffset(2008, 5, 1, 8, 30, 52, TimeSpan.FromHours(2));

    public EndOfDateTimeOffsetTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void EndOf_Minute_ReturnsLastSecondAndMillisecondOfMinute()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Minute);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "01/05/2008 08:30:59").VerifyExpression();
        (result.Millisecond == 999).VerifyExpression();
    }

    [Test]
    public void EndOf_Minute_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Minute);
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void EndOf_Hour_ReturnsLastMinuteSecondAndMillisecondOfHour()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Hour);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "01/05/2008 08:59:59").VerifyExpression();
        (result.Millisecond == 999).VerifyExpression();
    }

    [Test]
    public void EndOf_Hour_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Hour);
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void EndOf_Day_Returns235959999()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Day);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "01/05/2008 23:59:59").VerifyExpression();
        (result.Millisecond == 999).VerifyExpression();
    }

    [Test]
    public void EndOf_Day_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Day);
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void EndOf_Week_ReturnsSaturdayAt235959()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Week);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "03/05/2008 23:59:59").VerifyExpression();
    }

    [Test]
    public void EndOf_Week_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Week);
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void EndOf_IsoWeek_ReturnsFollowingSunday()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.IsoWeek);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "04/05/2008 23:59:59").VerifyExpression();
    }

    [Test]
    public void EndOf_Quarter_ReturnsLastDayOfQuarter()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Quarter);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "30/06/2008 23:59:59").VerifyExpression();
    }

    [Test]
    public void EndOf_Month_ReturnsLastDayAt235959()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Month);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "31/05/2008 23:59:59").VerifyExpression();
    }

    [Test]
    public void EndOf_Month_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Month);
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void EndOf_Year_ReturnsDecember31At235959()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Year);
        (result.ToString("dd/MM/yyyy HH:mm:ss") == "31/12/2008 23:59:59").VerifyExpression();
    }

    [Test]
    public void EndOf_Year_PreservesOffset()
    {
        var result = May2008PlusTwoHours.EndOf(DateTimeAnchor.Year);
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void EndOf_InvalidAnchor_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { May2008PlusTwoHours.EndOf((DateTimeAnchor)99); });
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
