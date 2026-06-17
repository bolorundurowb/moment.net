using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using Shouldly;

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
        result.ToString("dd/MM/yyyy").ShouldBe("27/04/2008");
    }

    [Test]
    public void FirstDateInWeek_EnUs_PreservesOffset()
    {
        var result = Thursday.FirstDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        result.Offset.ShouldBe(TimeSpan.FromHours(3));
    }

    [Test]
    public void FirstDateInWeek_Iso_ReturnsPrecedingMonday()
    {
        var result = Thursday.FirstDateInWeek(CultureInfo.GetCultureInfo("fr-FR"));
        result.ToString("dd/MM/yyyy").ShouldBe("28/04/2008");
    }

    [Test]
    public void LastDateInWeek_EnUs_ReturnsSaturday()
    {
        var result = Thursday.LastDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        result.ToString("dd/MM/yyyy").ShouldBe("03/05/2008");
    }

    [Test]
    public void LastDateInWeek_EnUs_PreservesOffset()
    {
        var result = Thursday.LastDateInWeek(CultureInfo.GetCultureInfo("en-US"));
        result.Offset.ShouldBe(TimeSpan.FromHours(3));
    }

    [Test]
    public void LastDateInWeek_Iso_ReturnsSunday()
    {
        var result = Thursday.LastDateInWeek(CultureInfo.GetCultureInfo("fr-FR"));
        result.ToString("dd/MM/yyyy").ShouldBe("04/05/2008");
    }

    [Test]
    public void FirstDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTimeOffset(2024, 6, 12, 10, 0, 0, TimeSpan.FromHours(2));
        var result = date.FirstDateInWeek();
        result.Date.ShouldBe(new DateTime(2024, 6, 9));
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void LastDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTimeOffset(2024, 6, 12, 10, 0, 0, TimeSpan.FromHours(2));
        var result = date.LastDateInWeek();
        result.Date.ShouldBe(new DateTime(2024, 6, 15));
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void FirstDateInWeek_SundayInFrenchCulture_ShouldBeMonday()
    {
        var sunday = new DateTimeOffset(2024, 6, 16, 10, 0, 0, TimeSpan.Zero);
        var frenchCulture = CultureInfo.GetCultureInfo("fr-FR");
        var firstDate = sunday.FirstDateInWeek(frenchCulture);
        firstDate.Date.ShouldBe(new DateTime(2024, 6, 10));
    }

    [Test]
    public void FirstDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTimeOffset.MinValue.FirstDateInWeek());
    }

    [Test]
    public void FirstDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTimeOffset.MaxValue.FirstDateInWeek());
    }

    [Test]
    public void LastDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTimeOffset.MinValue.LastDateInWeek());
    }

    [Test]
    public void LastDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTimeOffset.MaxValue.LastDateInWeek());
    }

    [Test]
    public void FirstDateInWeek_December31_ReturnsCorrectStartOfWeek()
    {
        var date = new DateTimeOffset(2023, 12, 31, 10, 0, 0, TimeSpan.Zero); // Sunday
        var result = date.FirstDateInWeek();
        result.Date.ShouldBe(new DateTime(2023, 12, 31)); // Sunday
    }

    [Test]
    public void LastDateInWeek_January1_ReturnsCorrectEndOfWeek()
    {
        var date = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero); // Monday
        var result = date.LastDateInWeek();
        result.Date.ShouldBe(new DateTime(2024, 1, 6)); // Saturday
    }

    [Test]
    public void FirstDateInWeek_IsoWeekStartsOnMonday_WorksAtYearBoundary()
    {
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var date = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero); // Monday
        var result = date.FirstDateInWeek(culture);
        result.Date.ShouldBe(new DateTime(2024, 1, 1)); // Monday
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
