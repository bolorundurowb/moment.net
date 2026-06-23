using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class EndOfTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    public EndOfTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void EndOf_Minute_SetsSecondsTo59()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.EndOf(DateTimeAnchor.Minute).ToString("dd/MM/yyyy HH:mm:ss") == "01/05/2008 08:30:59").VerifyExpression();
        (date.EndOf(DateTimeAnchor.Minute).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOf_Hour_SetsMinutesAndSecondsTo59()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.EndOf(DateTimeAnchor.Hour).ToString("dd/MM/yyyy HH:mm:ss") == "01/05/2008 08:59:59").VerifyExpression();
        (date.EndOf(DateTimeAnchor.Hour).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOf_Day_ReturnsFinalSecondOfDay()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.EndOf(DateTimeAnchor.Day).ToString("dd/MM/yyyy HH:mm:ss") == "01/05/2008 23:59:59").VerifyExpression();
        (date.EndOf(DateTimeAnchor.Day).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOf_Week_ReturnsSaturdayAtEndOfDay()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.EndOf(DateTimeAnchor.Week).ToString("dd/MM/yyyy HH:mm:ss") == "03/05/2008 23:59:59").VerifyExpression();
        (date.EndOf(DateTimeAnchor.Week).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOf_Month_ReturnsLastDayAtEndOfDay()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.EndOf(DateTimeAnchor.Month).ToString("dd/MM/yyyy HH:mm:ss") == "31/05/2008 23:59:59").VerifyExpression();
        (date.EndOf(DateTimeAnchor.Month).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOf_Year_ReturnsDecember31AtEndOfDay()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.EndOf(DateTimeAnchor.Year).ToString("dd/MM/yyyy HH:mm:ss") == "31/12/2008 23:59:59").VerifyExpression();
        (date.EndOf(DateTimeAnchor.Year).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOf_InvalidAnchor_ThrowsArgumentException()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        OmniAssert.Assert.Throws<ArgumentException>(() => { date.EndOf((DateTimeAnchor)999); });
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
