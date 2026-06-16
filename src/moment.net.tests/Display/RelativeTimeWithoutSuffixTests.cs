using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class RelativeTimeWithoutSuffixTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public RelativeTimeWithoutSuffixTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void FromNow_WhenWithoutSuffixIsTrue_OmitsAgoSuffix()
    {
        var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-5).AddSeconds(-10);

        fiveMinutesAgo.FromNow(true).ShouldBe("5 minutes");
    }

    [Test]
    public void ToNow_WhenWithoutSuffixIsTrue_OmitsInPrefix()
    {
        var fiveMinutesFromNow = DateTime.UtcNow.AddMinutes(5).AddSeconds(10);

        fiveMinutesFromNow.ToNow(true).ShouldBe("5 minutes");
    }

    [Test]
    public void From_WhenWithoutSuffixIsTrue_OmitsAgoSuffix()
    {
        var earlier = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var later = earlier.AddMinutes(5);

        earlier.From(later, true).ShouldBe("5 minutes");
    }

    [Test]
    public void To_WhenWithoutSuffixIsTrue_OmitsInPrefix()
    {
        var start = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var end = start.AddMinutes(5);

        start.To(end, true).ShouldBe("5 minutes");
    }

    [Test]
    public void DateTimeOffset_FromNow_WhenWithoutSuffixIsTrue_OmitsAgoSuffix()
    {
        var fiveMinutesAgo = DateTimeOffset.UtcNow.AddMinutes(-5).AddSeconds(-10);

        fiveMinutesAgo.FromNow(true).ShouldBe("5 minutes");
    }

    [TestCase("", "5 minutes")]
    [TestCase("es", "5 minutos")]
    [TestCase("fr", "5 minutes")]
    [TestCase("de", "5 Minuten")]
    [TestCase("pt", "5 minutos")]
    [TestCase("ru", "5 минут")]
    public void From_WhenWithoutSuffixIsTrue_UsesSupportedCultureResources(string cultureName, string expected)
    {
        var earlier = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var later = earlier.AddMinutes(5);
        var cultureInfo = string.IsNullOrEmpty(cultureName)
            ? CultureInfo.InvariantCulture
            : CultureInfo.GetCultureInfo(cultureName);

        earlier.From(later, true, cultureInfo).ShouldBe(expected);
    }

    [TestCase("", "5 minutes")]
    [TestCase("es", "5 minutos")]
    [TestCase("fr", "5 minutes")]
    [TestCase("de", "5 Minuten")]
    [TestCase("pt", "5 minutos")]
    [TestCase("ru", "5 минут")]
    public void To_WhenWithoutSuffixIsTrue_UsesSupportedCultureResources(string cultureName, string expected)
    {
        var start = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
        var end = start.AddMinutes(5);
        var cultureInfo = string.IsNullOrEmpty(cultureName)
            ? CultureInfo.InvariantCulture
            : CultureInfo.GetCultureInfo(cultureName);

        start.To(end, true, cultureInfo).ShouldBe(expected);
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
