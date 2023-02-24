using System;
using System.Globalization;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class FormatTests : IDisposable
{
    private readonly CultureWrapper _cultureWrapper;
    public FormatTests() => _cultureWrapper = new CultureWrapper(CultureInfo.InvariantCulture);

    [Test]
    public void FormatShouldReturnIsoStringWithNoSpecifiedFormatString()
    {
        var date = new DateTime(2020, 10, 04, 0, 0, 0, DateTimeKind.Utc);
        var formattedDate = date.Format();

        formattedDate.ShouldBe("2020-10-04T00:00:00+00:00");
    }

    [Test]
    public void FormatShouldReturnIsoStringWithSpecifiedFormatString()
    {
        var date = new DateTime(2020, 10, 04, 0, 0, 0, DateTimeKind.Utc);
        var formattedDate = date.ToUniversalTime().Format("yyy MMM hh");

        formattedDate.ShouldBe("2020 Oct 12");
    }

    [Test]
    public void FormatShouldReturnIsoStringWithSpecifiedFormatStringAndCulture()
    {
        var date = new DateTime(2020, 10, 04, 0, 0, 0, DateTimeKind.Utc);
        var culture = new CultureInfo("fr-CA", false);
        var formattedDate = date.Format("yyy MMM hh", culture);

        formattedDate.ShouldBe("2020 oct. 12", StringCompareShould.IgnoreCase);
    }

    public void Dispose()
    {
        _cultureWrapper.Dispose();
    }
}