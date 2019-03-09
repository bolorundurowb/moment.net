using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
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
    }
}