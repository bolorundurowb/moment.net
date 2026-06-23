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
    public void ConstructorOffset_WhenStartIsAfterEnd_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new MomentRangeOffset(
            new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test]
    public void Duration_ReturnsCorrectTimeSpan()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 2));
        range.Duration.ShouldBe(TimeSpan.FromDays(1));
    }

    [Test]
    public void DurationOffset_ReturnsCorrectTimeSpan()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero));
        range.Duration.ShouldBe(TimeSpan.FromHours(12));
    }

    [Test]
    public void Contains_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        Should.Throw<ArgumentNullException>(() => range.Contains((MomentRange)null!));
    }

    [Test]
    public void ContainsOffset_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));
        Should.Throw<ArgumentNullException>(() => range.Contains((MomentRangeOffset)null!));
    }

    [Test]
    public void Overlaps_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        Should.Throw<ArgumentNullException>(() => range.Overlaps(null!));
    }

    [Test]
    public void OverlapsOffset_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));
        Should.Throw<ArgumentNullException>(() => range.Overlaps(null!));
    }

    [Test]
    public void Intersect_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        Should.Throw<ArgumentNullException>(() => range.Intersect(null!));
    }

    [Test]
    public void IntersectOffset_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));
        Should.Throw<ArgumentNullException>(() => range.Intersect(null!));
    }

    [Test]
    public void Contains_Exclusive_ReturnsFalseForEndBoundary()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        range.Contains(new DateTime(2024, 1, 31), inclusive: false).ShouldBeFalse();
    }

    [Test]
    public void Overlaps_Exclusive_ReturnsFalseForTouchingRanges()
    {
        var first = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = new MomentRange(new DateTime(2024, 1, 15), new DateTime(2024, 1, 31));
        first.Overlaps(second, inclusive: false).ShouldBeFalse();
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

    [Test]
    public void DateOnlyRange_Contains_ReturnsTrueForDateInsideRange()
    {
        var range = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));

        range.Contains(new DateOnly(2024, 1, 15)).ShouldBeTrue();
    }

    [Test]
    public void DateOnlyRange_Contains_WhenExclusive_ReturnsFalseForBoundaryDate()
    {
        var range = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));

        range.Contains(new DateOnly(2024, 1, 1), inclusive: false).ShouldBeFalse();
    }

    [Test]
    public void DateOnlyRange_Overlaps_ReturnsTrueForPartiallyOverlappingRanges()
    {
        var first = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 15));
        var second = Moment.Range(new DateOnly(2024, 1, 10), new DateOnly(2024, 1, 31));

        first.Overlaps(second).ShouldBeTrue();
    }

    [Test]
    public void DateOnlyRange_Intersect_ReturnsOverlappingRange()
    {
        var first = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 15));
        var second = Moment.Range(new DateOnly(2024, 1, 10), new DateOnly(2024, 1, 31));

        var intersection = first.Intersect(second);

        intersection.ShouldNotBeNull();
        intersection!.Start.ShouldBe(new DateOnly(2024, 1, 10));
        intersection.End.ShouldBe(new DateOnly(2024, 1, 15));
    }

    [Test]
    public void DateOnlyRange_Constructor_WhenStartIsAfterEnd_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => new MomentDateOnlyRange(new DateOnly(2024, 2, 1), new DateOnly(2024, 1, 1)));
    }

    [Test]
    public void DateOnlyRange_Duration_ReturnsCorrectTimeSpan()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        range.Duration.ShouldBe(TimeSpan.FromDays(1));
    }

    [Test]
    public void DateOnlyRange_Contains_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        Should.Throw<ArgumentNullException>(() => range.Contains((MomentDateOnlyRange)null!));
    }

    [Test]
    public void DateOnlyRange_Contains_ExclusiveRange_ReturnsFalseWhenNotFullyInside()
    {
        var outer = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        var inner = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 15));

        outer.Contains(inner, inclusive: false).ShouldBeFalse();
    }

    [Test]
    public void DateOnlyRange_Overlaps_NullOther_ThrowsArgumentNullException()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        Should.Throw<ArgumentNullException>(() => range.Overlaps(null!));
    }

    [Test]
    public void DateOnlyRange_Intersect_NullOther_ThrowsArgumentNullException()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        Should.Throw<ArgumentNullException>(() => range.Intersect(null!));
    }
}
