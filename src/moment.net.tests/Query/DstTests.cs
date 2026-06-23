using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Query;

[TestFixture]
public class DstTests
{
    [Test]
    public void IsDaylightSavingTime_WithTimeZone_ReturnsTrueForSummerDate()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateTime(2024, 7, 1, 12, 0, 0);

        summer.IsDaylightSavingTime(timeZone).ShouldBeTrue();
    }

    [Test]
    public void IsDaylightSavingTime_WithTimeZone_ReturnsFalseForWinterDate()
    {
        var timeZone = GetNewYorkTimeZone();
        var winter = new DateTime(2024, 1, 1, 12, 0, 0);

        winter.IsDaylightSavingTime(timeZone).ShouldBeFalse();
    }

    [Test]
    public void IsDaylightSavingTime_NoArgs_UsesLocalTimeZone()
    {
        var summer = new DateTime(2024, 7, 1, 12, 0, 0, DateTimeKind.Local);
        summer.IsDaylightSavingTime().ShouldBe(TimeZoneInfo.Local.IsDaylightSavingTime(summer));
    }

    [Test]
    public void DateTimeOffsetIsDaylightSavingTime_NoArgs_UsesLocalTimeZone()
    {
        var summer = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero);
        summer.IsDaylightSavingTime().ShouldBe(TimeZoneInfo.Local.IsDaylightSavingTime(summer.DateTime));
    }

    [Test]
    public void DateTimeOffsetIsDaylightSavingTime_WithTimeZone_UsesSuppliedTimeZone()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.FromHours(-4));

        summer.IsDaylightSavingTime(timeZone).ShouldBeTrue();
    }

    [Test]
    public void IsDaylightSavingTime_NullTimeZone_ThrowsArgumentNullException()
    {
        var date = new DateTime(2024, 7, 1, 12, 0, 0);
        Should.Throw<ArgumentNullException>(() => date.IsDaylightSavingTime(null!));
    }

    [Test]
    public void DateTimeOffsetIsDaylightSavingTime_NullTimeZone_ThrowsArgumentNullException()
    {
        var date = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero);
        Should.Throw<ArgumentNullException>(() => date.IsDaylightSavingTime(null!));
    }

    [Test]
    public void DateOnlyIsDaylightSavingTime_WithTimeZone_ReturnsTrueForSummerDate()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateOnly(2024, 7, 1);

        summer.IsDaylightSavingTime(timeZone).ShouldBeTrue();
    }

    [Test]
    public void DateOnlyIsDaylightSavingTime_WithTimeZone_ReturnsFalseForWinterDate()
    {
        var timeZone = GetNewYorkTimeZone();
        var winter = new DateOnly(2024, 1, 1);

        winter.IsDaylightSavingTime(timeZone).ShouldBeFalse();
    }

    [Test]
    public void DateOnlyIsDaylightSavingTime_NullTimeZone_ThrowsArgumentNullException()
    {
        var date = new DateOnly(2024, 7, 1);
        Should.Throw<ArgumentNullException>(() => date.IsDaylightSavingTime(null!));
    }

    [Test]
    public void DateOnlyIsDaylightSavingTime_NoArgs_UsesLocalTimeZone()
    {
        var summer = new DateOnly(2024, 7, 1);
        summer.IsDaylightSavingTime().ShouldBe(TimeZoneInfo.Local.IsDaylightSavingTime(summer.ToDateTime(default)));
    }

    private static TimeZoneInfo GetNewYorkTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
        }
    }
}
