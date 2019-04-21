using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class TimeToTests
    {
        [Test]
        public void AFewSecondsInTheFutureTest()
        {
            var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(20);
            aFewSecondsAgo.ToNow().ShouldBe("in few seconds");
        }

        [Test]
        public void MoreSecondsInTheFutureTest()
        {
            var largeSecondsAgo = DateTime.UtcNow.AddSeconds(50);
            largeSecondsAgo.ToNow().ShouldBe("in one minute");
        }

        [Test]
        public void AFewMinutesInTheFutureTest()
        {
            var afewMinutesAgo = DateTime.Now.AddMinutes(1);
            afewMinutesAgo.ToNow().ShouldBe("in one minute");
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
            dateTime.ToNow().ShouldBe("in one hour");
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
            dateTime.ToNow().ShouldBe("in one day");
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
            dateTime.ToNow().ShouldBe("in one month");
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
            dateTime.ToNow().ShouldBe("in one year");
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

        [Test]
        public void ToSpecifiedDateTest()
        {
            var twoThousandAndTwelve = new DateTime(2012, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var twoThousandAndEighteen = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            twoThousandAndTwelve.To(twoThousandAndEighteen).ShouldBe("in 6 years");
        }
    }
}