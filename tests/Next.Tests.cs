using System;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class Next
    {
        string dateString = "5/1/2008 8:30:52 AM";

        [Test]
        public void NextDayOfWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.Next(DayOfWeek.Thursday).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("08/05/2008 08:30:52");
        }

        [Test]
        public void NextNthDayOfWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.Next(DayOfWeek.Thursday,3).ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("22/05/2008 08:30:52");
        }
    }
}
