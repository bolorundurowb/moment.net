using System;
using moment.net.Enums;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class Final
    {
        string dateString = "5/1/2008 8:30:52 AM";

        [Test]
        public void FinalInMonthTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.Final().Monday().InMonth().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("26/05/2008 00:00:00");
        }

        [Test]
        public void FinalInYearTest()
        {
            DateTime date = DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
            date.Final().Sunday().InYear().ToString("dd/MM/yyyy HH:mm:ss").ShouldBe("28/12/2008 00:00:00");
        }
    }
}
