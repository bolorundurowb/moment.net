using System;
using moment.net.Models;

namespace moment.net
{
    public static class RelativeTime
    {
        private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
        private const double DaysInAMonth = DaysInAYear / 12;

        /// <summary>
        /// Get the relative time from a given date time to the current time
        /// </summary>
        /// <param name="This">A time frame in the past</param>
        /// <returns>A string representing the time span in human readable format</returns>
        public static string FromNow(this DateTime This)
        {
            return This.Kind == DateTimeKind.Utc
                ? ParseFromPastTimeSpan(DateTime.UtcNow - This)
                : ParseFromPastTimeSpan(DateTime.Now - This);
        }

        /// <summary>
        /// Get the relative time from a given date time to another date time instance
        /// </summary>
        /// <param name="This">A time frame in the past</param>
        /// <param name="dateTime">A time frame sometime after the one being compared to</param>
        /// <returns>A string representing the time span in human readable format</returns>
        public static string From(this DateTime This, DateTime dateTime)
        {
            var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
            var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            return ParseFromPastTimeSpan(endDate - startDate);
        }

        /// <summary>
        /// Get the relative time from the current date time instance to a time frame in the future
        /// </summary>
        /// <param name="This">A time frame in the future</param>
        /// <returns>A string representing the time span in human readable format</returns>
        public static string ToNow(this DateTime This)
        {
            return This.Kind == DateTimeKind.Utc
                ? ParseFromFutureTimeSpan(This - DateTime.UtcNow)
                : ParseFromFutureTimeSpan(This - DateTime.Now);
        }

        /// <summary>
        /// Get the relative time from the a date time instance to a time frame in the future
        /// </summary>
        /// <param name="This">A time frame to be compared to</param>
        /// <param name="dateTime">A time frame in the future</param>
        /// <returns>A string representing the time span in human readable format</returns>
        public static string To(this DateTime This, DateTime dateTime)
        {
            var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
            var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            return ParseFromFutureTimeSpan(endDate - startDate);
        }

        public static string CalendarTime(this DateTime This, CalendarTimeFormats formats = null)
        {
            return CalendarTime(This, DateTime.UtcNow, formats);
        }

        public static string CalendarTime(this DateTime This, DateTime dateTime,
            CalendarTimeFormats formats = null)
        {
            formats = formats ?? new CalendarTimeFormats();
            var startDate = This.Kind == DateTimeKind.Local ? This : This.ToLocalTime();
            var endDate = dateTime.Kind == DateTimeKind.Local ? dateTime : dateTime.ToLocalTime();
            var timeDiff = endDate - startDate;

            if (startDate.Date == endDate.Date)
            {
                return endDate.ToString(formats.SameDay);
            }

            if (startDate.AddDays(1).Date == endDate.Date)
            {
                return endDate.ToString(formats.NextDay);
            }
            if (startDate.AddDays(-1).Date == endDate.Date)
            {
                return endDate.ToString(formats.LastDay);
            }
            if (timeDiff.TotalDays > 1 && timeDiff.TotalDays < 7)
            {
                return endDate.ToString(formats.NextWeek);
            }
            if (timeDiff.TotalDays >= -6 && timeDiff.TotalDays < -1)
            {
                return endDate.ToString(formats.LastWeek);
            }
            return endDate.ToString(formats.EverythingElse);
        }

        private static string ParseFromPastTimeSpan(TimeSpan timeSpan)
        {
            return $"{ParseTimeDifference(timeSpan)} ago";
        }

        private static string ParseFromFutureTimeSpan(TimeSpan timeSpan)
        {
            return $"in {ParseTimeDifference(timeSpan)}";
        }

        private static string ParseTimeDifference(TimeSpan timeSpan)
        {
            var totalTimeInSeconds = timeSpan.TotalSeconds;

            if (totalTimeInSeconds <= 44.0)
            {
                return "few seconds";
            }

            if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            {
                return "one minute";
            }

            var totalTimeInMinutes = timeSpan.TotalMinutes;

            if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            {
                return $"{Math.Round(totalTimeInMinutes)} minutes";
            }

            if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            {
                return "one hour";
            }

            var totalTimeInHours = timeSpan.TotalHours;

            if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            {
                return $"{Math.Round(totalTimeInHours)} hours";
            }

            if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            {
                return "one day";
            }

            var totalTimeInDays = timeSpan.TotalDays;

            if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            {
                return $"{Math.Round(totalTimeInDays)} days";
            }

            if (totalTimeInDays > 25 && totalTimeInDays <= 45)
            {
                return "one month";
            }

            if (totalTimeInDays > 45 && totalTimeInDays <= 319)
            {
                return $"{Math.Round(totalTimeInDays / DaysInAMonth)} months";
            }

            if (totalTimeInDays > 319 && totalTimeInDays <= 547)
            {
                return "one year";
            }

            if (totalTimeInDays > 547)
            {
                return $"{Math.Round(totalTimeInDays / DaysInAYear)} years";
            }

            throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
                "in The time span sent could not be parsed.");
        }
    }
}