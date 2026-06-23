using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.GetSet;

[TestFixture]
public class FirstLastDateInWeekDateTimeOffsetTests : IDisposable
{
    private readonly CultureInfo _originalCulture;

    private static readonly DateTimeOffset Thursday =
        new DateTimeOffset(2008, 5, 1, 8, 30, 52, TimeSpan.FromHours(3));

    public FirstLastDateInWeekDateTimeOffsetTests()
    {
        _originalCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
    }

    [Test]
    public void FirstDateInWeek_EnUs_ReturnsPrecedingSunday()
    {
        var result = Thursday.FirstDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        (result.ToString("dd/MM/yyyy") == "27/04/2008").VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_EnUs_PreservesOffset()
    {
        var result = Thursday.FirstDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        (result.Offset == TimeSpan.FromHours(3)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_Iso_ReturnsPrecedingMonday()
    {
        var result = Thursday.FirstDateInWeek(CultureInfo.GetCultureInfo("fr-FR"));
        (result.ToString("dd/MM/yyyy") == "28/04/2008").VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_EnUs_ReturnsSaturday()
    {
        var result = Thursday.LastDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        (result.ToString("dd/MM/yyyy") == "03/05/2008").VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_EnUs_PreservesOffset()
    {
        var result = Thursday.LastDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        (result.Offset == TimeSpan.FromHours(3)).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_Iso_ReturnsSunday()
    {
        var result = Thursday.LastDateInWeek(CultureInfo.GetCultureInfo("fr-FR"));
        (result.ToString("dd/MM/yyyy") == "04/05/2008").VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTimeOffset(2024, 6, 12, 10, 0, 0, TimeSpan.FromHours(2));
        var result = date.FirstDateInWeek();
        (result.Date == new DateTime(2024, 6, 9)).VerifyExpression();
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTimeOffset(2024, 6, 12, 10, 0, 0, TimeSpan.FromHours(2));
        var result = date.LastDateInWeek();
        (result.Date == new DateTime(2024, 6, 15)).VerifyExpression();
        (result.Offset == TimeSpan.FromHours(2)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_WhenSundayInFrenchCulture_ReturnsMonday()
    {
        var sunday = new DateTimeOffset(2024, 6, 16, 10, 0, 0, TimeSpan.Zero);
        var frenchCulture = CultureInfo.GetCultureInfo("fr-FR");
        var firstDate = sunday.FirstDateInWeek(frenchCulture);
        (firstDate.Date == new DateTime(2024, 6, 10)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTimeOffset.MinValue.FirstDateInWeek(); });
    }

    [Test]
    public void FirstDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTimeOffset.MaxValue.FirstDateInWeek(); });
    }

    [Test]
    public void LastDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTimeOffset.MinValue.LastDateInWeek(); });
    }

    [Test]
    public void LastDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { DateTimeOffset.MaxValue.LastDateInWeek(); });
    }

    [Test]
    public void FirstDateInWeek_December31_ReturnsCorrectStartOfWeek()
    {
        var date = new DateTimeOffset(2023, 12, 31, 10, 0, 0, TimeSpan.Zero);
        var result = date.FirstDateInWeek();
        (result.Date == new DateTime(2023, 12, 31)).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_January1_ReturnsCorrectEndOfWeek()
    {
        var date = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var result = date.LastDateInWeek();
        (result.Date == new DateTime(2024, 1, 6)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_IsoWeekStartsOnMonday_WorksAtYearBoundary()
    {
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var date = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var result = date.FirstDateInWeek(culture);
        (result.Date == new DateTime(2024, 1, 1)).VerifyExpression();
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
