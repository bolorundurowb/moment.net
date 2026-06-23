using System;
using NUnit.Framework;

namespace MomentNet.Tests.Query;

[TestFixture]
public class DstTests
{
    [Test]
    public void IsDaylightSavingTime_WithTimeZone_ReturnsTrueForSummerDate()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateTime(2024, 7, 1, 12, 0, 0);

        summer.IsDaylightSavingTime(timeZone).Verify().ToBeTrue();
    }

    [Test]
    public void IsDaylightSavingTime_WithTimeZone_ReturnsFalseForWinterDate()
    {
        var timeZone = GetNewYorkTimeZone();
        var winter = new DateTime(2024, 1, 1, 12, 0, 0);

        winter.IsDaylightSavingTime(timeZone).Verify().ToBeFalse();
    }

    [Test]
    public void IsDaylightSavingTime_NoArgs_UsesLocalTimeZone()
    {
        var summer = new DateTime(2024, 7, 1, 12, 0, 0, DateTimeKind.Local);
        (summer.IsDaylightSavingTime() == TimeZoneInfo.Local.IsDaylightSavingTime(summer)).VerifyExpression();
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateTimeOffsetAndNoTimeZone_UsesLocalTimeZone()
    {
        var summer = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero);
        (summer.IsDaylightSavingTime() == TimeZoneInfo.Local.IsDaylightSavingTime(summer.DateTime)).VerifyExpression();
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateTimeOffsetAndTimeZone_UsesSuppliedTimeZone()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.FromHours(-4));

        summer.IsDaylightSavingTime(timeZone).Verify().ToBeTrue();
    }

    [Test]
    public void IsDaylightSavingTime_NullTimeZone_ThrowsArgumentNullException()
    {
        var date = new DateTime(2024, 7, 1, 12, 0, 0);
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { date.IsDaylightSavingTime(null!); });
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateTimeOffsetAndNullTimeZone_ThrowsArgumentNullException()
    {
        var date = new DateTimeOffset(2024, 7, 1, 12, 0, 0, TimeSpan.Zero);
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { date.IsDaylightSavingTime(null!); });
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateOnlyAndSummer_ReturnsTrue()
    {
        var timeZone = GetNewYorkTimeZone();
        var summer = new DateOnly(2024, 7, 1);

        summer.IsDaylightSavingTime(timeZone).Verify().ToBeTrue();
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateOnlyAndWinter_ReturnsFalse()
    {
        var timeZone = GetNewYorkTimeZone();
        var winter = new DateOnly(2024, 1, 1);

        winter.IsDaylightSavingTime(timeZone).Verify().ToBeFalse();
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateOnlyAndNullTimeZone_ThrowsArgumentNullException()
    {
        var date = new DateOnly(2024, 7, 1);
        OmniAssert.Assert.Throws<ArgumentNullException>(() => { date.IsDaylightSavingTime(null!); });
    }

    [Test]
    public void IsDaylightSavingTime_WhenDateOnlyAndNoTimeZone_UsesLocalTimeZone()
    {
        var summer = new DateOnly(2024, 7, 1);
        (summer.IsDaylightSavingTime() == TimeZoneInfo.Local.IsDaylightSavingTime(summer.ToDateTime(default))).VerifyExpression();
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
