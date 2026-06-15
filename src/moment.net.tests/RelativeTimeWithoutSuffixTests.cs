using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

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

    public void Dispose() => _cultureWrapper.Dispose();
}
