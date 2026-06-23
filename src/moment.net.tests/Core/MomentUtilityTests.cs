using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MomentNet.Tests.Core;

[TestFixture]
public class MomentUtilityTests
{
    [Test]
    public void Min_ReturnsEarliestDate()
    {
        var earliest = new DateTime(2024, 1, 1);
        var latest = new DateTime(2024, 3, 1);

        (Moment.Min(latest, earliest, new DateTime(2024, 2, 1)) == earliest).VerifyExpression();
    }

    [Test]
    public void Max_ReturnsLatestDate()
    {
        var earliest = new DateTime(2024, 1, 1);
        var latest = new DateTime(2024, 3, 1);

        (Moment.Max(earliest, latest, new DateTime(2024, 2, 1)) == latest).VerifyExpression();
    }

    [Test]
    public void Min_WithNoDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Min(Array.Empty<DateTime>()); });
    }

    [Test]
    public void Max_WithNoDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Max(Array.Empty<DateTime>()); });
    }

    [Test]
    public void Min_NullDates_ThrowsArgumentNullException()
    {
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { Moment.Min((IEnumerable<DateTime>)null!); });
    }

    [Test]
    public void Max_NullDates_ThrowsArgumentNullException()
    {
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { Moment.Max((IEnumerable<DateTime>)null!); });
    }

    [Test]
    public void MinMax_WhenDateTimeOffset_ComparesInstants()
    {
        var earlierInstant = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var laterInstant = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.FromHours(1));

        (Moment.Min(laterInstant, earlierInstant) == earlierInstant).VerifyExpression();
        (Moment.Max(earlierInstant, laterInstant) == laterInstant).VerifyExpression();
    }

    [Test]
    public void Min_WhenDateTimeOffsetAndNull_ThrowsArgumentNullException()
    {
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { Moment.Min((IEnumerable<DateTimeOffset>)null!); });
    }

    [Test]
    public void Max_WhenDateTimeOffsetAndNull_ThrowsArgumentNullException()
    {
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { Moment.Max((IEnumerable<DateTimeOffset>)null!); });
    }

    [Test]
    public void Min_WhenDateTimeOffsetAndEmpty_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Min(Array.Empty<DateTimeOffset>()); });
    }

    [Test]
    public void Max_WhenDateTimeOffsetAndEmpty_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Max(Array.Empty<DateTimeOffset>()); });
    }

    [Test]
    public void Range_WhenDateTime_CreatesInclusiveRange()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 31);
        var range = Moment.Range(start, end);
        (range.Start == start).VerifyExpression();
        (range.End == end).VerifyExpression();
    }

    [Test]
    public void Range_WhenDateTimeOffset_CreatesInclusiveRange()
    {
        var start = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero);
        var range = Moment.Range(start, end);
        (range.Start == start).VerifyExpression();
        (range.End == end).VerifyExpression();
    }

    [Test]
    public void Min_WhenDateOnly_ReturnsEarliestDate()
    {
        var earliest = new DateOnly(2024, 1, 1);
        var latest = new DateOnly(2024, 3, 1);

        (Moment.Min(latest, earliest, new DateOnly(2024, 2, 1)) == earliest).VerifyExpression();
    }

    [Test]
    public void Max_WhenDateOnly_ReturnsLatestDate()
    {
        var earliest = new DateOnly(2024, 1, 1);
        var latest = new DateOnly(2024, 3, 1);

        (Moment.Max(earliest, latest, new DateOnly(2024, 2, 1)) == latest).VerifyExpression();
    }

    [Test]
    public void Min_WhenDateOnlyAndEmpty_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Min(Array.Empty<DateOnly>()); });
    }

    [Test]
    public void Min_WhenDateOnlyAndNull_ThrowsArgumentNullException()
    {
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { Moment.Min((IEnumerable<DateOnly>)null!); });
    }

    [Test]
    public void Max_WhenDateOnlyAndEmpty_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Max(Array.Empty<DateOnly>()); });
    }

    [Test]
    public void Max_WhenDateOnlyAndNull_ThrowsArgumentNullException()
    {
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { Moment.Max((IEnumerable<DateOnly>)null!); });
    }

    [Test]
    public void Range_WhenDateOnly_CreatesInclusiveRange()
    {
        var start = new DateOnly(2024, 1, 1);
        var end = new DateOnly(2024, 1, 31);
        var range = Moment.Range(start, end);
        (range.Start == start).VerifyExpression();
        (range.End == end).VerifyExpression();
    }

    [Test]
    public void Min_ReadOnlySpan_ReturnsEarliestDate()
    {
        var earliest = new DateTime(2024, 1, 1);
        var latest = new DateTime(2024, 3, 1);
        var middle = new DateTime(2024, 2, 1);

        (Moment.Min(latest, earliest, middle) == earliest).VerifyExpression();
        (Moment.Min(new[] { latest, earliest, middle }.AsSpan()) == earliest).VerifyExpression();
    }

    [Test]
    public void Max_ReadOnlySpan_ReturnsLatestDate()
    {
        var earliest = new DateTime(2024, 1, 1);
        var latest = new DateTime(2024, 3, 1);
        var middle = new DateTime(2024, 2, 1);

        (Moment.Max(earliest, latest, middle) == latest).VerifyExpression();
        (Moment.Max(new[] { earliest, latest, middle }.AsSpan()) == latest).VerifyExpression();
    }

    [Test]
    public void Min_ReadOnlySpan_EmptyDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Min(ReadOnlySpan<DateTime>.Empty); });
    }

    [Test]
    public void Max_ReadOnlySpan_EmptyDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Max(ReadOnlySpan<DateTime>.Empty); });
    }

    [Test]
    public void DateTimeOffsetMinMax_ReadOnlySpan_CompareInstants()
    {
        var earlierInstant = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var laterInstant = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.FromHours(1));

        (Moment.Min(laterInstant, earlierInstant) == earlierInstant).VerifyExpression();
        (Moment.Max(earlierInstant, laterInstant) == laterInstant).VerifyExpression();
        (Moment.Min(new[] { laterInstant, earlierInstant }.AsSpan()) == earlierInstant).VerifyExpression();
        (Moment.Max(new[] { earlierInstant, laterInstant }.AsSpan()) == laterInstant).VerifyExpression();
    }

    [Test]
    public void DateTimeOffsetMin_ReadOnlySpan_EmptyDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Min(ReadOnlySpan<DateTimeOffset>.Empty); });
    }

    [Test]
    public void DateTimeOffsetMax_ReadOnlySpan_EmptyDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Max(ReadOnlySpan<DateTimeOffset>.Empty); });
    }

    [Test]
    public void DateOnlyMinMax_ReadOnlySpan_ReturnsEarliestAndLatest()
    {
        var earliest = new DateOnly(2024, 1, 1);
        var latest = new DateOnly(2024, 3, 1);
        var middle = new DateOnly(2024, 2, 1);

        Moment.Min(latest, earliest, middle).Verify().ToBe(earliest);
        Moment.Max(earliest, latest, middle).Verify().ToBe(latest);
        Moment.Min(new[] { latest, earliest, middle }.AsSpan()).Verify().ToBe(earliest);
        Moment.Max(new[] { earliest, latest, middle }.AsSpan()).Verify().ToBe(latest);
    }

    [Test]
    public void DateOnlyMin_ReadOnlySpan_EmptyDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Min(ReadOnlySpan<DateOnly>.Empty); });
    }

    [Test]
    public void DateOnlyMax_ReadOnlySpan_EmptyDates_ThrowsArgumentException()
    {
        OmniAssert.Assert.Throws<ArgumentException>(() => { Moment.Max(ReadOnlySpan<DateOnly>.Empty); });
    }
}
