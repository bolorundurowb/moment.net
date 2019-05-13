using System;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class FirstLastDateInWeek
    {
        string dateString = "5/1/2008 8:30:52 AM";

        [Test]
        public void FirstDateInWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.FirstDateInWeek().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("27/04/2008 00:00:00");
        }

        [Test]
        public void LastDateInWeekTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.LastDateInWeek().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("03/05/2008 00:00:00");
        }

    }
}
