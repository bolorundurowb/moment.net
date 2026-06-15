using System;
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
    public void DateTimeOffsetMinMax_CompareInstants()
    {
        var earlierInstant = new DateTimeOffset(2024, 1, 1, 10, 0, 0, TimeSpan.Zero);
        var laterInstant = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.FromHours(1));

        Moment.Min(laterInstant, earlierInstant).ShouldBe(earlierInstant);
        Moment.Max(earlierInstant, laterInstant).ShouldBe(laterInstant);
    }

    [Test]
    public void Min_WithNoDates_ThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => Moment.Min(Array.Empty<DateTime>()));
    }
}
