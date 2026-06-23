using System;
using System.Globalization;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class FormatDateOnlyTests
{
    [Test]
    public void Format_NoFormatString_ReturnsIso8601DateString()
    {
        (new DateOnly(2020, 10, 4).Format() == "2020-10-04").VerifyExpression();
    }

    [Test]
    public void Format_WithFormatStringAndCulture_ReturnsFormattedString()
    {
        var culture = new CultureInfo("fr-CA", false);
        new DateOnly(2020, 10, 4).Format("yyyy MMM dd", culture)
            .Verify().ToBeIgnoringCase("2020 oct. 04");
    }
}

[TestFixture]
public class FormatTimeOnlyTests
{
    [Test]
    public void Format_NoFormatString_ReturnsIso8601TimeString()
    {
        (new TimeOnly(14, 30, 15).Format() == "14:30:15").VerifyExpression();
    }

    [Test]
    public void Format_WithFormatString_ReturnsFormattedString()
    {
        (new TimeOnly(14, 30, 15).Format("hh:mm tt", CultureInfo.InvariantCulture) == "02:30 PM").VerifyExpression();
    }
}
