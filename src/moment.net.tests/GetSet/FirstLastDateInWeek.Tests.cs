using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.GetSet;

[TestFixture]
public class FirstLastDateInWeekTests : IDisposable
{
    private readonly CultureInfo _originalCulture;
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    public FirstLastDateInWeekTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void FirstDateInWeek_ReturnsFirstDayOfWeek()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.FirstDateInWeek().ToString("dd/MM/yyyy HH:mm:ss") == "27/04/2008 00:00:00").VerifyExpression();
        (date.FirstDateInWeek().Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_ReturnsLastDayOfWeek()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.LastDateInWeek().ToString("dd/MM/yyyy HH:mm:ss") == "03/05/2008 00:00:00").VerifyExpression();
        (date.LastDateInWeek().Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTime(2024, 6, 12);
        (date.FirstDateInWeek() == new DateTime(2024, 6, 9)).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTime(2024, 6, 12);
        (date.LastDateInWeek() == new DateTime(2024, 6, 15)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_WhenSundayInFrenchCulture_ReturnsMonday()
    {
        var sunday = new DateTime(2024, 6, 16);
        var frenchCulture = CultureInfo.GetCultureInfo("fr-FR");
        var firstDate = sunday.FirstDateInWeek(frenchCulture);
        (firstDate == new DateTime(2024, 6, 10)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTime.MinValue.FirstDateInWeek(); });
    }

    [Test]
    public void FirstDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTime.MaxValue.FirstDateInWeek(); });
    }

    [Test]
    public void LastDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTime.MinValue.LastDateInWeek(); });
    }

    [Test]
    public void LastDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTime.MaxValue.LastDateInWeek(); });
    }

    [Test]
    public void FirstDateInWeek_December31_ReturnsCorrectStartOfWeek()
    {
        var date = new DateTime(2023, 12, 31);
        var result = date.FirstDateInWeek();
        (result == new DateTime(2023, 12, 31)).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_January1_ReturnsCorrectEndOfWeek()
    {
        var date = new DateTime(2024, 1, 1);
        var result = date.LastDateInWeek();
        (result == new DateTime(2024, 1, 6)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_IsoWeekStartsOnMonday_WorksAtYearBoundary()
    {
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var date = new DateTime(2024, 1, 1);
        var result = date.FirstDateInWeek(culture);
        (result == new DateTime(2024, 1, 1)).VerifyExpression();
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
