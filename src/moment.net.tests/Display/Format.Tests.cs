using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class FormatTests
{
    [Test]
    public void Format_NoFormatString_ReturnsIso8601String()
    {
        var date = new DateTime(2020, 10, 04, 0, 0, 0, DateTimeKind.Utc);
        var formattedDate = date.Format();

        formattedDate.ShouldBe("2020-10-04T00:00:00+00:00");
    }

    [Test]
    public void Format_WithFormatString_ReturnsFormattedString()
    {
        var date = new DateTime(2020, 10, 04, 0, 0, 0, DateTimeKind.Utc);
        var formattedDate = date.Format("yyy MMM hh", CultureInfo.InvariantCulture);

        formattedDate.ShouldBe("2020 Oct 12");
    }

    [Test]
    public void Format_WithFormatStringAndCulture_ReturnsLocalisedFormattedString()
    {
        var date = new DateTime(2020, 10, 04, 0, 0, 0, DateTimeKind.Utc);
        var culture = new CultureInfo("fr-CA", false);
        var formattedDate = date.Format("yyy MMM hh", culture);

        formattedDate.ShouldBe("2020 oct. 12", StringCompareShould.IgnoreCase);
    }
}
