using System;
using NUnit.Framework;

namespace MomentNet.Tests.Plugins.Range;

[TestFixture]
public class DateRangeTests
{
    [Test]
    public void Contains_ReturnsTrueForDateInsideRange()
    {
        var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));

        range.Contains(new DateTime(2024, 1, 15)).Verify().ToBeTrue();
    }

    [Test]
    public void Contains_WhenExclusive_ReturnsFalseForBoundaryDate()
    {
        var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));

        range.Contains(new DateTime(2024, 1, 1), inclusive: false).Verify().ToBeFalse();
    }

    [Test]
    public void Contains_ReturnsTrueWhenOtherRangeIsFullyContained()
    {
        var range = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        var contained = Moment.Range(new DateTime(2024, 1, 10), new DateTime(2024, 1, 20));

        range.Contains(contained).Verify().ToBeTrue();
    }

    [Test]
    public void Overlaps_ReturnsTrueForPartiallyOverlappingRanges()
    {
        var first = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = Moment.Range(new DateTime(2024, 1, 10), new DateTime(2024, 1, 31));

        first.Overlaps(second).Verify().ToBeTrue();
    }

    [Test]
    public void Intersect_ReturnsOverlappingRange()
    {
        var first = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = Moment.Range(new DateTime(2024, 1, 10), new DateTime(2024, 1, 31));

        var intersection = first.Intersect(second);

        intersection.Verify().NotToBeNull();
        (intersection.Start == new DateTime(2024, 1, 10)).VerifyExpression();
        (intersection.End == new DateTime(2024, 1, 15)).VerifyExpression();
    }

    [Test]
    public void Intersect_ReturnsNullForSeparateRanges()
    {
        var first = Moment.Range(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = Moment.Range(new DateTime(2024, 2, 1), new DateTime(2024, 2, 15));

        first.Intersect(second).Verify().ToBeNull();
    }

    [Test]
    public void Constructor_WhenStartIsAfterEnd_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { new MomentRange(new DateTime(2024, 2, 1), new DateTime(2024, 1, 1)); });
    }

    [Test]
    public void Constructor_WhenDateTimeOffsetStartAfterEnd_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { new MomentRangeOffset(
            new DateTimeOffset(2024, 2, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)); });
    }

    [Test]
    public void Duration_ReturnsCorrectTimeSpan()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 2));
        (range.Duration == TimeSpan.FromDays(1)).VerifyExpression();
    }

    [Test]
    public void Duration_WhenDateTimeOffset_ReturnsCorrectTimeSpan()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero));
        (range.Duration == TimeSpan.FromHours(12)).VerifyExpression();
    }

    [Test]
    public void Contains_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Contains((MomentRange)null!); });
    }

    [Test]
    public void Contains_WhenDateTimeOffsetRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Contains((MomentRangeOffset)null!); });
    }

    [Test]
    public void Overlaps_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Overlaps(null!); });
    }

    [Test]
    public void Overlaps_WhenDateTimeOffsetRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Overlaps(null!); });
    }

    [Test]
    public void Intersect_OtherRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Intersect(null!); });
    }

    [Test]
    public void Intersect_WhenDateTimeOffsetRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentRangeOffset(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Intersect(null!); });
    }

    [Test]
    public void Contains_Exclusive_ReturnsFalseForEndBoundary()
    {
        var range = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        range.Contains(new DateTime(2024, 1, 31), inclusive: false).Verify().ToBeFalse();
    }

    [Test]
    public void Overlaps_Exclusive_ReturnsFalseForTouchingRanges()
    {
        var first = new MomentRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 15));
        var second = new MomentRange(new DateTime(2024, 1, 15), new DateTime(2024, 1, 31));
        first.Overlaps(second, inclusive: false).Verify().ToBeFalse();
    }

    [Test]
    public void Intersect_WhenDateTimeOffset_ComparesByInstant()
    {
        var first = Moment.Range(
            new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
            new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero));
        var second = Moment.Range(
            new DateTimeOffset(2024, 1, 5, 2, 0, 0, TimeSpan.FromHours(2)),
            new DateTimeOffset(2024, 1, 20, 0, 0, 0, TimeSpan.Zero));

        var intersection = first.Intersect(second);

        intersection.Verify().NotToBeNull();
        (intersection.Start == new DateTimeOffset(2024, 1, 5, 2, 0, 0, TimeSpan.FromHours(2))).VerifyExpression();
        (intersection.End == new DateTimeOffset(2024, 1, 10, 0, 0, 0, TimeSpan.Zero)).VerifyExpression();
    }

    [Test]
    public void Contains_WhenDateOnly_ReturnsTrueForDateInsideRange()
    {
        var range = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));

        range.Contains(new DateOnly(2024, 1, 15)).Verify().ToBeTrue();
    }

    [Test]
    public void Contains_WhenDateOnlyAndExclusive_ReturnsFalseForBoundaryDate()
    {
        var range = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));

        range.Contains(new DateOnly(2024, 1, 1), inclusive: false).Verify().ToBeFalse();
    }

    [Test]
    public void Overlaps_WhenDateOnly_ReturnsTrueForPartiallyOverlappingRanges()
    {
        var first = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 15));
        var second = Moment.Range(new DateOnly(2024, 1, 10), new DateOnly(2024, 1, 31));

        first.Overlaps(second).Verify().ToBeTrue();
    }

    [Test]
    public void Intersect_WhenDateOnly_ReturnsOverlappingRange()
    {
        var first = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 15));
        var second = Moment.Range(new DateOnly(2024, 1, 10), new DateOnly(2024, 1, 31));

        var intersection = first.Intersect(second);

        intersection.Verify().NotToBeNull();
        (intersection!.Start == new DateOnly(2024, 1, 10)).VerifyExpression();
        (intersection.End == new DateOnly(2024, 1, 15)).VerifyExpression();
    }

    [Test]
    public void Constructor_WhenDateOnlyStartAfterEnd_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { new MomentDateOnlyRange(new DateOnly(2024, 2, 1), new DateOnly(2024, 1, 1)); });
    }

    [Test]
    public void Duration_WhenDateOnly_ReturnsCorrectTimeSpan()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 2));
        (range.Duration == TimeSpan.FromDays(1)).VerifyExpression();
    }

    [Test]
    public void Contains_WhenDateOnlyRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Contains((MomentDateOnlyRange)null!); });
    }

    [Test]
    public void Contains_WhenDateOnlyAndExclusiveRange_ReturnsFalseWhenNotFullyInside()
    {
        var outer = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        var inner = Moment.Range(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 15));

        outer.Contains(inner, inclusive: false).Verify().ToBeFalse();
    }

    [Test]
    public void Overlaps_WhenDateOnlyRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Overlaps(null!); });
    }

    [Test]
    public void Intersect_WhenDateOnlyRangeNull_ThrowsArgumentNullException()
    {
        var range = new MomentDateOnlyRange(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 31));
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { range.Intersect(null!); });
    }
}
