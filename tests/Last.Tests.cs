using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class Last
    {
        string dateString = "5/1/2008 8:30:52 AM";

        [Test]
        public void LastDayOfWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.Last(DayOfWeek.Thursday).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("24/04/2008 08:30:52");
        }

        [Test]
        public void LastNthDayOfWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.Last(DayOfWeek.Thursday,3).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("10/04/2008 08:30:52");
        }
    }
}
