using System;
using System.Collections.Generic;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Core;

[TestFixture]
public class MomentUtilityTests
{
    [Test]
    public void Min_ReturnsEarliestDate()
    {
        var earliest = new DateTime(2024, 1, 1);
        var latest = new DateTime(2024, 3, 1);

        Moment.Min(latest, earliest, new DateTime(2024, 2, 1)).ShouldBe(earliest);
    }

    [Test]
    public void Max_ReturnsLatestDate()
    {
        var earliest = new DateTime(2024, 1, 1);
        var latest = new DateTime(2024, 3, 1);

        Moment.Max(earliest, latest, new DateTime(2024, 2, 1)).ShouldBe(latest);
    }

    [Test]
    public void Min_WithNoDates_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => Moment.Min(Array.Empty<DateTime>()));
    }

    [Test]
    public void Max_WithNoDates_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => Moment.Max(Array.Empty<DateTime>()));
    }

    [Test]
    public void Min_NullDates_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => Moment.Min((IEnumerable<DateTime>)null!));
    }

    [Test]
    public void Max_NullDates_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => Moment.Max((IEnumerable<DateTime>)null!));
    }

    [Test]
    public void DateTimeOffsetMinMax_CompareInstants()
    {
        var earlierInstant = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var laterInstant = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.FromHours(1));

        Moment.Min(laterInstant, earlierInstant).ShouldBe(earlierInstant);
        Moment.Max(earlierInstant, laterInstant).ShouldBe(laterInstant);
    }

    [Test]
    public void DateTimeOffsetMin_NullDates_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => Moment.Min((IEnumerable<DateTimeOffset>)null!));
    }

    [Test]
    public void DateTimeOffsetMax_NullDates_ThrowsArgumentNullException()
    {
        Should.Throw<ArgumentNullException>(() => Moment.Max((IEnumerable<DateTimeOffset>)null!));
    }

    [Test]
    public void DateTimeOffsetMin_EmptyDates_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => Moment.Min(Array.Empty<DateTimeOffset>()));
    }

    [Test]
    public void DateTimeOffsetMax_EmptyDates_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => Moment.Max(Array.Empty<DateTimeOffset>()));
    }

    [Test]
    public void Range_CreatesDateTimeRange()
    {
        var start = new DateTime(2024, 1, 1);
        var end = new DateTime(2024, 1, 31);
        var range = Moment.Range(start, end);
        range.Start.ShouldBe(start);
        range.End.ShouldBe(end);
    }

    [Test]
    public void Range_CreatesDateTimeOffsetRange()
    {
        var start = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var end = new DateTimeOffset(2024, 1, 31, 0, 0, 0, TimeSpan.Zero);
        var range = Moment.Range(start, end);
        range.Start.ShouldBe(start);
        range.End.ShouldBe(end);
    }
}
