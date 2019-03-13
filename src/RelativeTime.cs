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

        private static string ParseFromPastTimeSpan(TimeSpan timeSpan)
        {
            var totalTimeInSeconds = timeSpan.TotalSeconds;

            if (totalTimeInSeconds <= 44.0)
            {
                return "a few seconds ago";
            }

            if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            {
                return "a minute ago";
            }

            var totalTimeInMinutes = timeSpan.TotalMinutes;

            if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            {
                return $"{Math.Floor(totalTimeInMinutes)} minutes ago";
            }

            if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            {
                return "an hour ago";
            }

            var totalTimeInHours = timeSpan.TotalHours;

            if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            {
                return $"{Math.Floor(totalTimeInHours)} hours ago";
            }

            if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            {
                return "a day ago";
            }

            var totalTimeInDays = timeSpan.TotalDays;

            if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            {
                return $"{Math.Floor(totalTimeInDays)} days ago";
            }

            if (totalTimeInDays > 25 && totalTimeInDays <= 45)
            {
                return "a month ago";
            }

            if (totalTimeInDays > 45 && totalTimeInDays <= 319)
            {
                return $"{Math.Ceiling(totalTimeInDays / DaysInAMonth)} months ago";
            }

            if (totalTimeInDays > 319 && totalTimeInDays <= 547)
            {
                return "a year ago";
            }

            if (totalTimeInDays > 547)
            {
                return $"{Math.Ceiling(totalTimeInDays / DaysInAYear)} years ago";
            }

            throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
                "The time span sent could not be parsed.");
        }

        private static string ParseFromFutureTimeSpan(TimeSpan timeSpan)
        {
            var totalTimeInSeconds = timeSpan.TotalSeconds;

            if (totalTimeInSeconds <= 44.0)
            {
                return "in a few seconds";
            }

            if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            {
                return "in a minute";
            }

            var totalTimeInMinutes = timeSpan.TotalMinutes;

            if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            {
                return $"in {Math.Ceiling(totalTimeInMinutes)} minutes";
            }

            if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            {
                return "in an hour";
            }

            var totalTimeInHours = timeSpan.TotalHours;

            if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            {
                return $"in {Math.Ceiling(totalTimeInHours)} hours";
            }

            if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            {
                return "in a day";
            }

            var totalTimeInDays = timeSpan.TotalDays;

            if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            {
                return $"in {Math.Ceiling(totalTimeInDays)} days";
            }

            if (totalTimeInDays > 25 && totalTimeInDays <= 45)
            {
                return "in a month";
            }

            if (totalTimeInDays > 45 && totalTimeInDays <= 319)
            {
                return $"in {Math.Ceiling(totalTimeInDays / DaysInAMonth)} months";
            }

            if (totalTimeInDays > 319 && totalTimeInDays <= 547)
            {
                return "in a year";
            }

            if (totalTimeInDays > 547)
            {
                return $"in {Math.Ceiling(totalTimeInDays / DaysInAYear)} years";
            }

            throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
                "in The time span sent could not be parsed.");
        }
    }
}