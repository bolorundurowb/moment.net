using System;

namespace moment.net
{
    public static class RelativeTime
    {
        private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
        private const double DaysInAMonth = DaysInAYear / 12;

        public static string FromNow(this DateTime This)
        {
            return This.Kind == DateTimeKind.Utc
                ? ParseFromPastTimeSpan(DateTime.UtcNow - This)
                : ParseFromPastTimeSpan(DateTime.Now - This);
        }

        public static string From(this DateTime This, DateTime dateTime)
        {
            var startDate = This.Kind == DateTimeKind.Utc ? This : This.ToUniversalTime();
            var endDate = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            return ParseFromPastTimeSpan(endDate - startDate);
        }

        public static string ToNow(this DateTime This)
        {
            return This.Kind == DateTimeKind.Utc
                ? ParseFromFutureTimeSpan(This - DateTime.UtcNow)
                : ParseFromFutureTimeSpan(This - DateTime.Now);
        }

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
    }
}