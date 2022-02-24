using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests;

[TestFixture]
public class PositionalTimeTests
{
    [Test]
    public void IsLeapYearShouldReturnFalseWhenYearIsNot()
    {
        var date = DateTime.Parse("1900-01-02");
        var result = date.IsLeapYear();
        
        result.ShouldBeFalse();
    }
    
    [Test]
    public void IsLeapYearShouldReturnTrueWhenYearIs()
    {
        var date = DateTime.Parse("1992-01-02");
        var result = date.IsLeapYear();
        
        result.ShouldBeTrue();
        
         date = DateTime.Parse("2000-01-02");
         result = date.IsLeapYear();
        
        result.ShouldBeTrue();
    }
}