using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class LastDateTimeOffsetTests
{
   
    private static readonly DateTimeOffset Friday =
        new DateTimeOffset(2024, 3, 15, 10, 0, 0, TimeSpan.FromHours(-5));

    [Test]
    public void Last_Monday_ReturnsPreviousMonday()
    {
        var result = Friday.Last(DayOfWeek.Monday);
        (result.DayOfWeek == DayOfWeek.Monday).VerifyExpression();
        (result.ToString("dd/MM/yyyy") == "11/03/2024").VerifyExpression();
    }

    [Test]
    public void Last_SameDayOfWeek_SkipsToPreviousWeek()
    {
        var result = Friday.Last(DayOfWeek.Friday);
        (result.DayOfWeek == DayOfWeek.Friday).VerifyExpression();
        (result.ToString("dd/MM/yyyy") == "08/03/2024").VerifyExpression();
    }

    [Test]
    public void Last_PreservesOffset()
    {
        var result = Friday.Last(DayOfWeek.Monday);
        (result.Offset == TimeSpan.FromHours(-5)).VerifyExpression();
    }

    [Test]
    public void Last_WithCount_ReturnsNthPreviousOccurrence()
    {
        var result = Friday.Last(DayOfWeek.Friday, 2);
        (result.DayOfWeek == DayOfWeek.Friday).VerifyExpression();
        (result.ToString("dd/MM/yyyy") == "01/03/2024").VerifyExpression();
    }

    [Test]
    public void Last_WithCount_PreservesOffset()
    {
        var result = Friday.Last(DayOfWeek.Monday, 2);
        (result.Offset == TimeSpan.FromHours(-5)).VerifyExpression();
    }

    [Test]
    public void Last_WithCountOfOne_IsSameAsSingleLast()
    {
        (Friday.Last(DayOfWeek.Monday, 1) == Friday.Last(DayOfWeek.Monday)).VerifyExpression();
    }

    [Test]
    public void Last_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { Friday.Last(DayOfWeek.Monday, 0); });
    }
}
