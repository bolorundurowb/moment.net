using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace MomentNet.Tests;

[TestFixture]
public class FormatDateTimeOffsetTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;

    public FormatDateTimeOffsetTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void Format_NoFormatString_ReturnsIso8601StringWithOffset()
    {
        var date = new DateTimeOffset(2020, 10, 4, 0, 0, 0, TimeSpan.Zero);
        date.Format().ShouldBe("2020-10-04T00:00:00+00:00");
    }

    [Test]
    public void Format_NoFormatString_NonZeroOffset_IncludesOffset()
    {
        var date = new DateTimeOffset(2020, 10, 4, 12, 0, 0, TimeSpan.FromHours(5));
        date.Format().ShouldBe("2020-10-04T12:00:00+05:00");
    }

    [Test]
    public void Format_WithFormatString_ReturnsFormattedString()
    {
        var date = new DateTimeOffset(2020, 10, 4, 0, 0, 0, TimeSpan.Zero);
        date.Format("yyyy MMM dd").ShouldBe("2020 Oct 04");
    }

    [Test]
    public void Format_WithFormatStringAndCulture_ReturnsLocalisedString()
    {
        var date = new DateTimeOffset(2020, 10, 4, 0, 0, 0, TimeSpan.Zero);
        var culture = new CultureInfo("fr-CA", false);
        date.Format("yyyy MMM dd", culture).ShouldBe("2020 oct. 04", StringCompareShould.IgnoreCase);
    }

    public void Dispose() => _cultureWrapper.Dispose();
}
