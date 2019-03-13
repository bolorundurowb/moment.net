using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    [ExcludeFromCodeCoverage]
    public class RelativeTimeTests
    {
        [Test]
        public void SmallSecondsTest()
        {
            var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
            aFewSecondsAgo.FromNow().ShouldBe("a few seconds ago");
        }
        
        [Test]
        public void LargeSecondsTest()
        {
            var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
            largeSecondsAgo.FromNow().ShouldBe("a minute ago");
        }
        
        [Test]
        public void SmallMinutesTest()
        {
            var afewMinutesAgo = DateTime.Now.AddMinutes(-1);
            afewMinutesAgo.FromNow().ShouldBe("a minute ago");
        }
        
        [Test]
        public void LargeMinutesTest()
        {
            var minutesAgo = DateTime.Now.AddMinutes(-15);
            minutesAgo.FromNow().ShouldBe("15 minutes ago");
        }
        
        [Test]
        public void HourTest()
        {
            var dateTime = DateTime.UtcNow.AddMinutes(-65);
            dateTime.FromNow().ShouldBe("an hour ago");
        }
        
        [Test]
        public void MultipleHoursTest()
        {
            var dateTime = DateTime.UtcNow.AddHours(-20);
            dateTime.FromNow().ShouldBe("20 hours ago");
        }
        
        [Test]
        public void DayTest()
        {
            var dateTime = DateTime.UtcNow.AddHours(-25);
            dateTime.FromNow().ShouldBe("a day ago");
        }
        
        [Test]
        public void MultipleDaysTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(-4);
            dateTime.FromNow().ShouldBe("4 days ago");
        }
        
        [Test]
        public void MonthTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(-27);
            dateTime.FromNow().ShouldBe("a month ago");
        }
        
        [Test]
        public void MultipleMonthsTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(-60);
            dateTime.FromNow().ShouldBe("2 months ago");
        }
        
        [Test]
        public void YearTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(-360);
            dateTime.FromNow().ShouldBe("a year ago");
        }
        
        [Test]
        public void TwoYearsTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(-570);
            dateTime.FromNow().ShouldBe("2 years ago");
        }
        
        [Test]
        public void MultipleYearsTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(-3650);
            dateTime.FromNow().ShouldBe("10 years ago");
        }
        
        [Test]
        public void AFewSecondsInTheFutureTest()
        {
            var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(20);
            aFewSecondsAgo.ToNow().ShouldBe("in a few seconds");
        }
        
        [Test]
        public void MoreSecondsInTheFutureTest()
        {
            var largeSecondsAgo = DateTime.UtcNow.AddSeconds(50);
            largeSecondsAgo.ToNow().ShouldBe("in a minute");
        }
        
        [Test]
        public void AFewMinutesInTheFutureTest()
        {
            var afewMinutesAgo = DateTime.Now.AddMinutes(1);
            afewMinutesAgo.ToNow().ShouldBe("in a minute");
        }
        
        [Test]
        public void MoreMinutesInTheFutureTest()
        {
            var minutesAgo = DateTime.Now.AddMinutes(15);
            minutesAgo.ToNow().ShouldBe("in 15 minutes");
        }
        
        [Test]
        public void ToHourTest()
        {
            var dateTime = DateTime.UtcNow.AddMinutes(65);
            dateTime.ToNow().ShouldBe("in an hour");
        }
        
        [Test]
        public void ToMultipleHoursTest()
        {
            var dateTime = DateTime.UtcNow.AddHours(20);
            dateTime.ToNow().ShouldBe("in 20 hours");
        }
        
        [Test]
        public void ToDayTest()
        {
            var dateTime = DateTime.UtcNow.AddHours(25);
            dateTime.ToNow().ShouldBe("in a day");
        }
        
        [Test]
        public void ToMultipleDaysTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(4);
            dateTime.ToNow().ShouldBe("in 4 days");
        }
        
        [Test]
        public void ToMonthTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(27);
            dateTime.ToNow().ShouldBe("in a month");
        }
        
        [Test]
        public void ToMultipleMonthsTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(60);
            dateTime.ToNow().ShouldBe("in 2 months");
        }
        
        [Test]
        public void ToYearTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(360);
            dateTime.ToNow().ShouldBe("in a year");
        }
        
        [Test]
        public void ToTwoYearsTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(570);
            dateTime.ToNow().ShouldBe("in 2 years");
        }
        
        [Test]
        public void ToMultipleYearsTest()
        {
            var dateTime = DateTime.UtcNow.AddDays(3650);
            dateTime.ToNow().ShouldBe("in 10 years");
        }
        
        [Test]
        public void FromSpecifiedDateTest()
        {
            var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            
            twoThousandAndTwelve.From(twoThousandAndEighteen).ShouldBe("6 years ago");
        }
    }
}