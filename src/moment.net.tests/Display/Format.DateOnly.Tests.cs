using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests.Display;

[TestFixture]
public class FormatDateOnlyTests
{
    [Test]
    public void Format_NoFormatString_ReturnsIso8601DateString()
    {
        new DateOnly(2020, 10, 4).Format().ShouldBe("2020-10-04");
    }

    [Test]
    public void Format_WithFormatStringAndCulture_ReturnsFormattedString()
    {
        var culture = new CultureInfo("fr-CA", false);
        new DateOnly(2020, 10, 4).Format("yyyy MMM dd", culture)
            .ShouldBe("2020 oct. 04", StringCompareShould.IgnoreCase);
    }
}

[TestFixture]
public class FormatTimeOnlyTests
{
    [Test]
    public void Format_NoFormatString_ReturnsIso8601TimeString()
    {
        new TimeOnly(14, 30, 15).Format().ShouldBe("14:30:15");
    }

    [Test]
    public void Format_WithFormatString_ReturnsFormattedString()
    {
        new TimeOnly(14, 30, 15).Format("hh:mm tt", CultureInfo.InvariantCulture).ShouldBe("02:30 PM");
    }
}
