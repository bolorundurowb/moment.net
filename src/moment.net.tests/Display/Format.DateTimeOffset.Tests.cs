using System;
using System.Globalization;
using NUnit.Framework;

namespace MomentNet.Tests.Display;

[TestFixture]
public class FormatDateTimeOffsetTests
{
    [Test]
    public void Format_NoFormatString_ReturnsIso8601StringWithOffset()
    {
        var date = new DateTimeOffset(2020, 10, 4, 0, 0, 0, TimeSpan.Zero);
        (date.Format() == "2020-10-04T00:00:00+00:00").VerifyExpression();
    }

    [Test]
    public void Format_NoFormatString_NonZeroOffset_IncludesOffset()
    {
        var date = new DateTimeOffset(2020, 10, 4, 12, 0, 0, TimeSpan.FromHours(5));
        (date.Format() == "2020-10-04T12:00:00+05:00").VerifyExpression();
    }

    [Test]
    public void Format_WithFormatString_ReturnsFormattedString()
    {
        var date = new DateTimeOffset(2020, 10, 4, 0, 0, 0, TimeSpan.Zero);
        (date.Format("yyyy MMM dd", CultureInfo.InvariantCulture) == "2020 Oct 04").VerifyExpression();
    }

    [Test]
    public void Format_WithFormatStringAndCulture_ReturnsLocalisedString()
    {
        var date = new DateTimeOffset(2020, 10, 4, 0, 0, 0, TimeSpan.Zero);
        var culture = new CultureInfo("fr-CA", false);
        date.Format("yyyy MMM dd", culture).Verify().ToBeIgnoringCase("2020 oct. 04");
    }
}
