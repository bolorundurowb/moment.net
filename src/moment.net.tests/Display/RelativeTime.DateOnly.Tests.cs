using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class RelativeTimeDateOnlyTests
{
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;

    [Test]
    public void FromNow_DelegatesToDateTimeRelativeTime()
    {
        var fiveDaysAgo = new DateOnly(2024, 1, 1);
        var reference = new DateOnly(2024, 1, 6);
        fiveDaysAgo.From(reference, Invariant).ShouldContain("days ago");
    }

    [Test]
    public void FromNow_WithoutSuffix_OmitsAgoSuffix()
    {
        var fiveDaysAgo = new DateOnly(2024, 1, 1);
        var reference = new DateOnly(2024, 1, 6);
        fiveDaysAgo.From(reference, true, Invariant).ShouldBe("5 days");
    }

    [Test]
    public void From_ComparisonDate_ReturnsRelativeString()
    {
        var earlier = new DateOnly(2024, 1, 1);
        var later = new DateOnly(2024, 1, 8);
        earlier.From(later, true, Invariant).ShouldBe("7 days");
    }

    [Test]
    public void ToNow_FutureDate_ReturnsRelativeString()
    {
        var start = new DateOnly(2024, 1, 1);
        var end = new DateOnly(2024, 1, 6);
        start.To(end, true, Invariant).ShouldBe("5 days");
    }

    [Test]
    public void To_ComparisonDate_ReturnsRelativeString()
    {
        var start = new DateOnly(2024, 1, 1);
        var end = new DateOnly(2024, 1, 8);
        start.To(end, true, Invariant).ShouldBe("7 days");
    }

    [Test]
    public void ToNow_WithoutSuffix_OmitsInPrefix()
    {
        var future = new DateOnly(2024, 1, 8);
        var reference = new DateOnly(2024, 1, 1);
        reference.To(future, true, Invariant).ShouldBe("7 days");
    }

    [Test]
    public void FromNow_CallsThroughToRelativeTime()
    {
        new DateOnly(2000, 1, 1).FromNow(Invariant).ShouldContain("year");
    }

    [Test]
    public void ToNow_CallsThroughToRelativeTime()
    {
        new DateOnly(2099, 12, 31).ToNow(Invariant).ShouldContain("year");
    }

    [Test]
    public void From_WithSpanishCulture_ReturnsLocalisedString()
    {
        var earlier = new DateOnly(2024, 1, 1);
        var later = new DateOnly(2024, 1, 6);
        earlier.From(later, CultureInfo.GetCultureInfo("es")).ShouldContain("días");
    }

    [Test]
    public void To_WithSpanishCulture_ReturnsLocalisedString()
    {
        var start = new DateOnly(2024, 1, 1);
        var end = new DateOnly(2024, 1, 6);
        start.To(end, CultureInfo.GetCultureInfo("es")).ShouldContain("días");
    }
}
