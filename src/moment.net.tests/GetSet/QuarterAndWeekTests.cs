using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.GetSet;

[TestFixture]
public class QuarterAndWeekTests
{
    [Test]
    public void Quarter_ReturnsCalendarQuarter()
    {
        (new DateTime(2024, 1, 15).Quarter() == 1).VerifyExpression();
        (new DateTime(2024, 5, 15).Quarter() == 2).VerifyExpression();
        (new DateTime(2024, 8, 15).Quarter() == 3).VerifyExpression();
        (new DateTime(2024, 12, 15).Quarter() == 4).VerifyExpression();
    }

    [Test]
    public void StartOfQuarter_ReturnsFirstDayOfQuarter()
    {
        var date = new DateTime(2024, 5, 15, 13, 45, 30, DateTimeKind.Utc);

        var result = date.StartOf(DateTimeAnchor.Quarter);

        (result == new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc)).VerifyExpression();
        (result.Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void EndOfQuarter_ReturnsLastMillisecondOfQuarter()
    {
        var date = new DateTime(2024, 5, 15, 13, 45, 30, DateTimeKind.Utc);

        (date.EndOf(DateTimeAnchor.Quarter) == new DateTime(2024, 6, 30, 23, 59, 59, 999, DateTimeKind.Utc)).VerifyExpression();
    }

    [Test]
    public void DiffInQuarters_ReturnsMonthDifferenceDividedByThree()
    {
        var later = new DateTime(2024, 7, 1);
        var earlier = new DateTime(2024, 1, 1);

        (later.DiffInQuarters(earlier) == 2.0).VerifyExpression();
    }

    [Test]
    public void IsoWeek_HandlesDatesThatBelongToPreviousIsoYear()
    {
        var date = new DateTime(2021, 1, 1);

        (date.IsoWeek() == 53).VerifyExpression();
        (date.IsoWeekYear() == 2020).VerifyExpression();
    }

    [Test]
    public void StartOfIsoWeek_ReturnsMondayAtMidnight()
    {
        var date = new DateTime(2024, 1, 3, 10, 30, 0, DateTimeKind.Utc);

        (date.StartOf(DateTimeAnchor.IsoWeek) == new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)).VerifyExpression();
    }

    [Test]
    public void EndOfIsoWeek_ReturnsSundayAtEndOfDay()
    {
        var date = new DateTime(2024, 1, 3, 10, 30, 0, DateTimeKind.Utc);

        (date.EndOf(DateTimeAnchor.IsoWeek) == new DateTime(2024, 1, 7, 23, 59, 59, 999, DateTimeKind.Utc)).VerifyExpression();
    }

    [Test]
    public void Week_UsesSuppliedCultureCalendarSettings()
    {
        var date = new DateTime(2024, 1, 7);

        (date.Week(CultureInfo.GetCultureInfo("en-US")) == 2).VerifyExpression();
    }

    [Test]
    public void DateTimeOffset_QuarterAndIsoWeek_PreserveOffsetWhenAnchored()
    {
        var date = new DateTimeOffset(2024, 5, 15, 13, 45, 30, TimeSpan.FromHours(2));

        (date.Quarter() == 2).VerifyExpression();
        (date.IsoWeek() == 20).VerifyExpression();
        (date.StartOf(DateTimeAnchor.Quarter) == new DateTimeOffset(2024, 4, 1, 0, 0, 0, TimeSpan.FromHours(2))).VerifyExpression();
    }

    [Test]
    public void Week_WhenDateTimeAndCurrentCulture_ReturnsCorrectWeek()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var date = new DateTime(2024, 1, 7);
            (date.Week() == 2).VerifyExpression();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    [Test]
    public void Week_WhenDateTimeAndFrenchCulture_ReturnsCorrectWeek()
    {
        var date = new DateTime(2024, 1, 7);
        (date.Week(CultureInfo.GetCultureInfo("fr-FR")) == 1).VerifyExpression();
    }

    [Test]
    public void Week_WhenDateTimeOffsetAndCurrentCulture_ReturnsCorrectWeek()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            var date = new DateTimeOffset(2024, 1, 7, 0, 0, 0, TimeSpan.Zero);
            (date.Week() == 2).VerifyExpression();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    [Test]
    public void Week_WhenDateTimeOffsetAndFrenchCulture_ReturnsCorrectWeek()
    {
        var date = new DateTimeOffset(2024, 1, 7, 0, 0, 0, TimeSpan.Zero);
        (date.Week(CultureInfo.GetCultureInfo("fr-FR")) == 1).VerifyExpression();
    }

    [Test]
    public void IsoWeekYear_WhenDateTimeOffset_ReturnsCorrectYear()
    {
        var date = new DateTimeOffset(2021, 1, 1, 0, 0, 0, TimeSpan.Zero);
        (date.IsoWeekYear() == 2020).VerifyExpression();
    }
}
