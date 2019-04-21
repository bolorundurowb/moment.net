using System;
using moment.net.Enums;
using moment.net.Models;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class RelativeTimeTests
    {
        string dateString = "5/1/2008 8:30:52 AM";

        [Test]
        public void StartOfMinuteTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.StartOf(DateTimeAnchor.Minute).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:00");
        }

        [Test]
        public void StartOfHourTest()
        {
            DateTime date = DateTime.Parse(dateString,System.Globalization.CultureInfo.InvariantCulture);
            date.StartOf(DateTimeAnchor.Hour).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:00:00"); 
        }

        [Test]
        public void StartOfDayTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.StartOf(DateTimeAnchor.Day).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
        }

        [Test]
        public void StartOfWeekTest()
        {

            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.StartOf(DateTimeAnchor.Week).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
        }

        [Test]
        public void StartOfMonthTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.StartOf(DateTimeAnchor.Month).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 00:00:00");
        }

        [Test]
        public void StartOfYearTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.StartOf(DateTimeAnchor.Year).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/01/2008 00:00:00");
        }

        [Test]
        public void SmallSecondsTest()
        {
            var aFewSecondsAgo = DateTime.UtcNow.AddSeconds(-20);
            aFewSecondsAgo.FromNow().ShouldBe("few seconds ago");
        }

        [Test]
        public void LargeSecondsTest()
        {
            var largeSecondsAgo = DateTime.UtcNow.AddSeconds(-50);
            largeSecondsAgo.FromNow().ShouldBe("one minute ago");
        }

        [Test]
        public void SmallMinutesTest()
        {
            var afewMinutesAgo = DateTime.Now.AddMinutes(-1);
            afewMinutesAgo.FromNow().ShouldBe("one minute ago");
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
            dateTime.FromNow().ShouldBe("one hour ago");
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
            dateTime.FromNow().ShouldBe("one day ago");
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
            dateTime.FromNow().ShouldBe("one month ago");
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
            dateTime.FromNow().ShouldBe("one year ago");
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

        [Test]
        public void CalendarTimeSameDay()
        {
            var today = DateTime.Now.Date.AddHours(2);
            today.CalendarTime().ShouldStartWith("Today at ");
        }

        [Test]
        public void CalendarTimeFromYesterday()
        {
            var yesterday = DateTime.Now.AddDays(-1);
            yesterday.CalendarTime().ShouldStartWith("Tomorrow at ");
        }

        [Test]
        public void CalendarTimeFromTomorrow()
        {
            var tomorrow = DateTime.Now.AddDays(1);
            tomorrow.CalendarTime().ShouldStartWith("Yesterday at ");
        }

        [Test]
        public void CalendarTimeFromTwoFixedDates()
        {
            var initialDate = new DateTime(2012,12,12);
            var nextDate = new DateTime(2012,12,18);
            initialDate.CalendarTime(nextDate).ShouldStartWith(nextDate.ToString("dddd 'at' "));
        }

        [Test]
        public void CalendarTimeToTwoFixedDates()
        {
            var earlierDate = new DateTime(2012,12,12);
            var laterDate = new DateTime(2012,12,18);
            laterDate.CalendarTime(earlierDate).ShouldStartWith(earlierDate.ToString("'Last' dddd 'at' "));
        }

        [Test]
        public void CalendarTimeForEcessiveTimeSpanWithSpecifiedFormat()
        {
            var initialDate = new DateTime(2012,12,12);
            var nextDate = new DateTime(2018,12,12);
            initialDate.CalendarTime(nextDate, new CalendarTimeFormats("", "", "", "", "", "dd/MM/yyyy")).ShouldBe(nextDate.ToString("dd/MM/yyyy"));
        }
    }
}