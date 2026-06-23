using System;
using NUnit.Framework;

namespace MomentNet.Tests.Manipulate;

[TestFixture]
public class LastTests
{
    readonly string dateString = "5/1/2008 8:30:52Z AM";

    [Test]
    public void Last_DayOfWeek_ReturnsPreviousOccurrence()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.Last(DayOfWeek.Thursday).ToString("dd/MM/yyyy HH:mm:ss") == "24/04/2008 08:30:52").VerifyExpression();
        (date.Last(DayOfWeek.Thursday).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void Last_NthDayOfWeek_ReturnsNthPreviousOccurrence()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        (date.Last(DayOfWeek.Thursday, 3).ToString("dd/MM/yyyy HH:mm:ss") == "10/04/2008 08:30:52").VerifyExpression();
        (date.Last(DayOfWeek.Thursday, 3).Kind == DateTimeKind.Utc).VerifyExpression();
    }

    [Test]
    public void Last_WithInvalidCount_ThrowsArgumentOutOfRangeException()
    {
        var date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
        OmniAssert.Assert.Throws<ArgumentOutOfRangeException>(() => { date.Last(DayOfWeek.Thursday, 0); });
    }
}
