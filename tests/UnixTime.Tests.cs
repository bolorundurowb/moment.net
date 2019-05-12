using System;
using NUnit.Framework;
using Shouldly;

namespace moment.net.Tests
{
    public class UnixTimeTests
    {
        [Test]
        public void UnixTimeInMillisecondsOneYearFromEpoch()
        {
            var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            var millisecondsElapsed = dateTime.UnixTimestampInMilliseconds();
            millisecondsElapsed.ShouldBe(365.0 * 24 * 60 * 60 * 1000);
        }

        [Test]
        public void UnixTimeInSecondsOneYearFromEpoch()
        {
            var dateTime = new DateTime(1971, 01, 01, 0, 0, 0, DateTimeKind.Utc);
            var secondsElapsed = dateTime.UnixTimestampInSeconds();
            secondsElapsed.ShouldBe(365.0 * 24 * 60 * 60);
        }
    }
}