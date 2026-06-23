using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class NextDateTimeOffsetTests
{
   
    private static readonly DateTimeOffset Friday =
        new DateTimeOffset(2024, 3, 15, 10, 0, 0, TimeSpan.FromHours(5.5));

    [Test]
    public void Next_Monday_ReturnsFollowingMonday()
    {
        var result = Friday.Next(DayOfWeek.Monday);
        (result.DayOfWeek == DayOfWeek.Monday).VerifyExpression();
        (result.ToString("dd/MM/yyyy") == "18/03/2024").VerifyExpression();
    }

    [Test]
    public void Next_SameDayOfWeek_SkipsToFollowingWeek()
    {
        var result = Friday.Next(DayOfWeek.Friday);
        (result.DayOfWeek == DayOfWeek.Friday).VerifyExpression();
        (result.ToString("dd/MM/yyyy") == "22/03/2024").VerifyExpression();
    }

    [Test]
    public void Next_PreservesOffset()
    {
        var result = Friday.Next(DayOfWeek.Monday);
        (result.Offset == TimeSpan.FromHours(5.5)).VerifyExpression();
    }

    [Test]
    public void Next_WithCount_ReturnsNthOccurrence()
    {
        var result = Friday.Next(DayOfWeek.Monday, 2);
        (result.DayOfWeek == DayOfWeek.Monday).VerifyExpression();
        (result.ToString("dd/MM/yyyy") == "25/03/2024").VerifyExpression();
    }

    [Test]
    public void Next_WithCount_PreservesOffset()
    {
        var result = Friday.Next(DayOfWeek.Monday, 2);
        (result.Offset == TimeSpan.FromHours(5.5)).VerifyExpression();
    }

    [Test]
    public void Next_WithCountOfOne_IsSameAsSingleNext()
    {
        (Friday.Next(DayOfWeek.Monday, 1) == Friday.Next(DayOfWeek.Monday)).VerifyExpression();
    }

    [Test]
    public void Next_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { Friday.Next(DayOfWeek.Monday, 0); });
    }
}
