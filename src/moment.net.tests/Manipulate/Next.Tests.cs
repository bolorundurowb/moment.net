using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class NextTests
{
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void Next_DayOfWeek_ReturnsNextOccurrence()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.Next(DayOfWeek.Thursday).ToString("dd/MM/yyyy HH:mm:ss") == "08/05/2008 08:30:52").VerifyExpression();
        (date.Next(DayOfWeek.Thursday).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void Next_NthDayOfWeek_ReturnsNthFutureOccurrence()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.Next(DayOfWeek.Thursday, 3).ToString("dd/MM/yyyy HH:mm:ss") == "22/05/2008 08:30:52").VerifyExpression();
        (date.Next(DayOfWeek.Thursday, 3).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void Next_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { date.Next(DayOfWeek.Thursday, 0); });
    }
}
