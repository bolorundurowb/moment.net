using System;
using System.Globalization;
using System.Threading;
using NUnit.Framework;

namespace MomentNet.Tests.GetSet;

[TestFixture]
public class QuarterAndWeekDateOnlyTests
{
    [Test]
    public void Quarter_ReturnsCorrectQuarter()
    {
        (new DateOnly(2024, 5, 15).Quarter() == 2).VerifyExpression();
    }

    [Test]
    public void Week_UsesSuppliedCulture()
    {
        (new DateOnly(2024, 1, 7).Week(CultureInfo.GetCultureInfo("en-US")) == 2).VerifyExpression();
    }

    [Test]
    public void Week_NoArgs_UsesCurrentCulture()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            (new DateOnly(2024, 1, 7).Week() == 2).VerifyExpression();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    [Test]
    public void IsoWeek_ReturnsCorrectWeekNumber()
    {
        (new DateOnly(2024, 1, 1).IsoWeek() == 1).VerifyExpression();
    }

    [Test]
    public void IsoWeekYear_ReturnsCorrectYear()
    {
        (new DateOnly(2021, 1, 1).IsoWeekYear() == 2020).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_UsesSuppliedCulture()
    {
        (new DateOnly(2024, 1, 10).FirstDateInWeek(CultureInfo.GetCultureInfo("en-US")) == new DateOnly(2024, 1, 7)).VerifyExpression();
    }

    [Test]
    public void LastDateInWeek_UsesSuppliedCulture()
    {
        (new DateOnly(2024, 1, 10).LastDateInWeek(CultureInfo.GetCultureInfo("en-US")) == new DateOnly(2024, 1, 13)).VerifyExpression();
    }

    [Test]
    public void FirstDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            (new DateOnly(2024, 1, 10).FirstDateInWeek() == new DateOnly(2024, 1, 7)).VerifyExpression();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }

    [Test]
    public void LastDateInWeek_NoArgs_UsesCurrentCulture()
    {
        var originalCulture = Thread.CurrentThread.CurrentCulture;
        try
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            (new DateOnly(2024, 1, 10).LastDateInWeek() == new DateOnly(2024, 1, 13)).VerifyExpression();
        }
        finally
        {
            Thread.CurrentThread.CurrentCulture = originalCulture;
        }
    }
}
