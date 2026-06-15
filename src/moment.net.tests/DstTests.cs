using System;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

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
    public void DateTimeOffsetIsDaylightSavingTime_WithTimeZone_UsesSuppliedTimeZone()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.FromHours(-4));

        summer.IsDaylightSavingTime(timeZone).ShouldBeTrue();
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
