using System;
using MomentNet.Plugins.Range;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Plugins.Range;

[TestFixture]
public class DateRangeTests
{
    [Test]
    public void Contains_ReturnsTrueForDateInsideRange()
    {
        var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));

        range.Contains(new DateTime(2024, 1, 15)).ShouldBeTrue();
    }

    [Test]
    public void Contains_WhenExclusive_ReturnsFalseForBoundaryDate()
    {
        var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));

        range.Contains(new DateTime(2024, 1, 1), inclusive: false).ShouldBeFalse();
    }

    [Test]
    public void Contains_ReturnsTrueWhenOtherRangeIsFullyContained()
    {
        var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        var contained = Moment.Range(new DateTime(2024, 1, 10), new DateTime(2024, 1, 20));

        range.Contains(contained).ShouldBeTrue();
    }

    [Test]
    public void Overlaps_ReturnsTrueForPartiallyOverlappingRanges()
    {
        var first = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = Moment.Range(new DateTime(2024, 1, 10), new DateTime(2024, 1, 31));

        first.Overlaps(second).ShouldBeTrue();
    }

    [Test]
    public void Intersect_ReturnsOverlappingRange()
    {
        var first = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = Moment.Range(new DateTime(2024, 1, 10), new DateTime(2024, 1, 31));

        var intersection = first.Intersect(second);

        intersection.ShouldNotBeNull();
        intersection.Start.ShouldBe(new DateTime(2024, 1, 10));
        intersection.End.ShouldBe(new DateTime(2024, 1, 15));
    }

    [Test]
    public void Intersect_ReturnsNullForSeparateRanges()
    {
        var first = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = Moment.Range(new DateTime(2024, 2, 1), new DateTime(2024, 2, 15));

        first.Intersect(second).ShouldBeNull();
    }

    [Test]
    public void Constructor_WhenStartIsAfterEnd_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new MomentRange(new DateTime(2024, 2, 1), new DateTime(2024, 1, 1)));
    }

    [Test]
    public void DateTimeOffsetRange_IntersectsByInstant()
    {
        var first = Moment.Range(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero));
        var second = Moment.Range(
            new DateTimeOffset(2024, 1, 5, 2, 0, 0, TimeSpan.FromHours(2)),
            new DateTimeOffset(2024, 1, 20, 0, 0, 0, TimeSpan.Zero));

        var intersection = first.Intersect(second);

        intersection.ShouldNotBeNull();
        intersection.Start.ShouldBe(new DateTimeOffset(2024, 1, 5, 2, 0, 0, TimeSpan.FromHours(2)));
        intersection.End.ShouldBe(new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero));
    }
}
