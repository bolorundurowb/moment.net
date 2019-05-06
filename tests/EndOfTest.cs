using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class EndOfTest
    {
        string dateString = "5/1/2008 8:30:52 AM";

        [Test]
        public void EndOfMinuteTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.EndOf(DateTimeAnchor.Minute).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:30:59");
        }

        [Test]
        public void EndOfHourTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.EndOf(DateTimeAnchor.Hour).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 08:59:59");
        }

        [Test]
        public void EndOfDayTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.EndOf(DateTimeAnchor.Day).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("01/05/2008 23:59:59");
        }

        [Test]
        public void EndOfWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.EndOf(DateTimeAnchor.Week).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("03/05/2008 23:59:59");
        }

        [Test]
        public void EndOfMonthTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.EndOf(DateTimeAnchor.Month).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("31/05/2008 23:59:59");
        }

        [Test]
        public void EndOfYearTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.EndOf(DateTimeAnchor.Year).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("31/12/2008 23:59:59");
        }
    }
}
