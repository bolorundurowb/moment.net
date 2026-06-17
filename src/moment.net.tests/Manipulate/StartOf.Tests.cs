using System;
using System.Globalization;
using System.Threading;
using MomentNet.Manipulate;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class StartOfTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    public StartOfTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void StartOf_Minute_TruncatesSecondsToZero()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Minute).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:00");
        date.StartOf(DateTimeAnchor.Minute).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOf_Hour_TruncatesMinutesAndSecondsToZero()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Hour).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:00:00");
        date.StartOf(DateTimeAnchor.Hour).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOf_Day_TruncatesTimeToMidnight()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Day).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Day).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOf_Week_ReturnsPrecedingSunday()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Week).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Week).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOf_Month_ReturnsFirstDayAtMidnight()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Month).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Month).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOf_Year_ReturnsJanuaryFirstAtMidnight()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.StartOf(DateTimeAnchor.Year).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/01/2008 00:00:00");
        date.StartOf(DateTimeAnchor.Year).Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void StartOf_InvalidAnchor_ThrowsArgumentException()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        Should.Throw<ArgumentException>(() => date.StartOf((DateTimeAnchor)999));
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
