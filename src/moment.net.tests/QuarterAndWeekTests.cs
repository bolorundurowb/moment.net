using System;
using System.Globalization;
using MomentNet.Enums;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

[TestFixture]
public class QuarterAndWeekTests
{
    [Test]
    public void Quarter_ReturnsCalendarQuarter()
    {
        new DateTime(2024, 1, 15).Quarter().ShouldBe(1);
        new DateTime(2024, 5, 15).Quarter().ShouldBe(2);
        new DateTime(2024, 8, 15).Quarter().ShouldBe(3);
        new DateTime(2024, 12, 15).Quarter().ShouldBe(4);
    }

    [Test]
    public void StartOfQuarter_ReturnsFirstDayOfQuarter()
    {
        var date = new DateTime(2024, 5, 15, 13, 45, 30, DateTimeKind.Utc);

        var result = date.StartOf(DateTimeAnchor.Quarter);

        result.ShouldBe(new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc));
        result.Kind.ShouldBe(DateTimeKind.Utc);
    }

    [Test]
    public void EndOfQuarter_ReturnsLastMillisecondOfQuarter()
    {
        var date = new DateTime(2024, 5, 15, 13, 45, 30, DateTimeKind.Utc);

        date.EndOf(DateTimeAnchor.Quarter)
            .ShouldBe(new DateTime(2024, 6, 30, 23, 59, 59, 999, DateTimeKind.Utc));
    }

    [Test]
    public void DiffInQuarters_ReturnsMonthDifferenceDividedByThree()
    {
        var later = new DateTime(2024, 7, 1);
        var earlier = new DateTime(2024, 1, 1);

        later.DiffInQuarters(earlier).ShouldBe(2.0);
    }

    [Test]
    public void IsoWeek_HandlesDatesThatBelongToPreviousIsoYear()
    {
        var date = new DateTime(2021, 1, 1);

        date.IsoWeek().ShouldBe(53);
        date.IsoWeekYear().ShouldBe(2020);
    }

    [Test]
    public void StartOfIsoWeek_ReturnsMondayAtMidnight()
    {
        var date = new DateTime(2024, 1, 3, 10, 30, 0, DateTimeKind.Utc);

        date.StartOf(DateTimeAnchor.IsoWeek)
            .ShouldBe(new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc));
    }

    [Test]
    public void EndOfIsoWeek_ReturnsSundayAtEndOfDay()
    {
        var date = new DateTime(2024, 1, 3, 10, 30, 0, DateTimeKind.Utc);

        date.EndOf(DateTimeAnchor.IsoWeek)
            .ShouldBe(new DateTime(2024, 1, 7, 23, 59, 59, 999, DateTimeKind.Utc));
    }

    [Test]
    public void Week_UsesSuppliedCultureCalendarSettings()
    {
        var date = new DateTime(2024, 1, 7);

        date.Week(CultureInfo.GetCultureInfo("en-US")).ShouldBe(2);
    }

    [Test]
    public void DateTimeOffset_QuarterAndIsoWeek_PreserveOffsetWhenAnchored()
    {
        var date = new DateTimeOffset(2024, 5, 15, 13, 45, 30, TimeSpan.FromHours(2));

        date.Quarter().ShouldBe(2);
        date.IsoWeek().ShouldBe(20);
        date.StartOf(DateTimeAnchor.Quarter)
            .ShouldBe(new DateTimeOffset(2024, 4, 1, 0, 0, 0, TimeSpan.FromHours(2)));
    }
}
