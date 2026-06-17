using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using Shouldly;

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
        date.FirstDateInWeek().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
        date.FirstDateInWeek().Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void LastDateInWeek_ReturnsLastDayOfWeek()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        date.LastDateInWeek().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("03/05/2008 00:00:00");
        date.LastDateInWeek().Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void FirstDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTime(2024, 6, 12); // Wednesday
        date.FirstDateInWeek().ShouldBe(new DateTime(2024, 6, 9)); // Sunday
    }

    [Test]
    public void LastDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var date = new DateTime(2024, 6, 12); // Wednesday
        date.LastDateInWeek().ShouldBe(new DateTime(2024, 6, 15)); // Saturday
    }

    [Test]
    public void FirstDateInWeek_SundayInFrenchCulture_ShouldBeMonday()
    {
        var sunday = new DateTime(2024, 6, 16);
        var frenchCulture = CultureInfo.GetCultureInfo("fr-FR");
        var firstDate = sunday.FirstDateInWeek(frenchCulture);
        firstDate.ShouldBe(new DateTime(2024, 6, 10));
    }

    [Test]
    public void FirstDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTime.MinValue.FirstDateInWeek());
    }

    [Test]
    public void FirstDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTime.MaxValue.FirstDateInWeek());
    }

    [Test]
    public void LastDateInWeek_MinValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTime.MinValue.LastDateInWeek());
    }

    [Test]
    public void LastDateInWeek_MaxValue_ThrowsArgumentOutOfRangeException()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => DateTime.MaxValue.LastDateInWeek());
    }

    [Test]
    public void FirstDateInWeek_December31_ReturnsCorrectStartOfWeek()
    {
        var date = new DateTime(2023, 12, 31); // Sunday
        var result = date.FirstDateInWeek();
        result.ShouldBe(new DateTime(2023, 12, 31)); // Sunday
    }

    [Test]
    public void LastDateInWeek_January1_ReturnsCorrectEndOfWeek()
    {
        var date = new DateTime(2024, 1, 1); // Monday
        var result = date.LastDateInWeek();
        result.ShouldBe(new DateTime(2024, 1, 6)); // Saturday
    }

    [Test]
    public void FirstDateInWeek_IsoWeekStartsOnMonday_WorksAtYearBoundary()
    {
        var culture = CultureInfo.GetCultureInfo("fr-FR");
        var date = new DateTime(2024, 1, 1); // Monday
        var result = date.FirstDateInWeek(culture);
        result.ShouldBe(new DateTime(2024, 1, 1)); // Monday
    }

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _originalCulture;
    }
}
