using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.GetSet;

public class FirstLastDateInWeekDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    // Thursday 2008-05-01 at UTC+03:00
    private static readonly DateTimeOffset Thursday =
        new DateTimeOffset(2008, 5, 1, 8, 30, 52, TimeSpan.FromHours(3));

    public FirstLastDateInWeekDateTimeOffsetTests()
    {
        _cultureWrapper = new CultureWrapper(CultureInfo.GetCultureInfo("en-US"));
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
        // ISO-8601 week starts on Monday (e.g. fr-FR)
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
        using var wrapper = new CultureWrapper(CultureInfo.GetCultureInfo("en-US"));
        var date = new DateTimeOffset(2024, 6, 12, 10, 0, 0, TimeSpan.FromHours(2));
        var result = date.FirstDateInWeek();
        result.Date.ShouldBe(new DateTime(2024, 6, 9));
        result.Offset.ShouldBe(TimeSpan.FromHours(2));
    }

    [Test]
    public void LastDateInWeek_NoArgs_UsesCurrentCulture()
    {
        using var wrapper = new CultureWrapper(CultureInfo.GetCultureInfo("en-US"));
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

    public void Dispose() => _cultureWrapper.Dispose();
}
