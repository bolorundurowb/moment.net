using System;
using System.Globalization;

namespace moment.net
{
    public static class RelativeTime
    {
        private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
        private const double DaysInAMonth = DaysInAYear / 12;

        /// <summary>
        /// Returns the start of the year, month, week, day or hour for specified date time
        /// This implementation uses the current culture
        /// </summary>
        /// <param name="timeAnchor">Anchors the returned value to a starting point (year/month/week/day/hour)</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException">Thrown when an invalid  value is casted to timeAnchor</exception>
        public static DateTime StartOf(this DateTime This, DateTimeAnchor timeAnchor)
        {
            return This.StartOf(timeAnchor, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns the start of the year, month, week, day or hour for specified date time
        /// This implementation requires culture information to be provided
        /// </summary>
        /// <param name="timeAnchor">Anchors the returned value to a starting point (year/month/week/day/hour)</param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException">Thrown when an invalid  value is casted to timeAnchor</exception>
        public static DateTime StartOf(this DateTime This, DateTimeAnchor timeAnchor, CultureInfo cultureInfo)
        {
            switch (timeAnchor)
            {
                case DateTimeAnchor.Minute:
                    return new DateTime(This.Year, This.Month, This.Day, This.Hour, This.Minute, 0);
                case DateTimeAnchor.Hour:
                    return new DateTime(This.Year, This.Month, This.Day, This.Hour,0,0);
                case DateTimeAnchor.Day:
                    return new DateTime(This.Year, This.Month, This.Day);
                case DateTimeAnchor.Week:
                    var tmp = GetFirstDateInWeek(This, cultureInfo);
                    return new DateTime(tmp.Year, tmp.Month, tmp.Day);
                case DateTimeAnchor.Month:
                    return new DateTime(This.Year, This.Month, 1);
                case DateTimeAnchor.Year:
                    return new DateTime(This.Year, 1, 1);
                default:
                    throw new InvalidCastException();
            }
        }

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


        /// <summary>
        /// Returns the first day of the week for the given date for the given
        /// The returned first day of the week will vary based on the supplied culture info
        /// </summary>
        /// <param name="dayInWeek">A day in the week of interest</param>
        /// <param name="cultureInfo">The culture infoormation to be formatted with</param>
        /// <returns></returns>
        private static DateTime GetFirstDateInWeek(DateTime dayInWeek, CultureInfo cultureInfo)
        {
            DayOfWeek firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            DateTime firstDateInWeek = dayInWeek.Date;
            int diff = (int)firstDateInWeek.DayOfWeek - (int)firstDayOfWeek;
            var value = firstDateInWeek.AddDays(-(Math.Abs(diff)));
            return value;
        }
    }
}