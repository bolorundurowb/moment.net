using System;
using moment.net.Enums;

namespace moment.net
{
    public static class RelativeTime
    {
        private const double DaysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
        private const double DaysInAMonth = DaysInAYear / 12;

        public static string FromNow(this DateTime This)
        {
            return This.Kind == DateTimeKind.Utc
                ? TimeFromTimeSpan(DateTime.UtcNow - This, RelativityDirection.From)
                : TimeFromTimeSpan(DateTime.Now - This, RelativityDirection.From);
        }

        public static string ToNow(this DateTime This)
        {
            return This.Kind == DateTimeKind.Utc
                ? TimeFromTimeSpan(This - DateTime.UtcNow, RelativityDirection.To)
                : TimeFromTimeSpan(This - DateTime.Now, RelativityDirection.To);
        }

        private static string TimeFromTimeSpan(TimeSpan timeSpan, RelativityDirection direction)
        {
            var toPreScript = "in";
            var fromPostScript = "ago";
            var response = string.Empty;
            var totalTimeInSeconds = timeSpan.TotalSeconds;

            if (totalTimeInSeconds <= 44.0)
            {
                response = "a few seconds";
            }

            if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            {
                response = "a minute";
            }

            var totalTimeInMinutes = timeSpan.TotalMinutes;

            if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            {
                response = $"{Math.Floor(totalTimeInMinutes)} minutes";
            }

            if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            {
                response = "an hour";
            }

            var totalTimeInHours = timeSpan.TotalHours;

            if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            {
                response = $"{Math.Floor(totalTimeInHours)} hours";
            }

            if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            {
                response = "a day";
            }

            var totalTimeInDays = timeSpan.TotalDays;

            if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            {
                response = $"{Math.Floor(totalTimeInDays)} days";
            }

            if (totalTimeInDays > 25 && totalTimeInDays <= 45)
            {
                response = "a month";
            }

            if (totalTimeInDays > 45 && totalTimeInDays <= 319)
            {
                response = $"{Math.Ceiling(totalTimeInDays / DaysInAMonth)} months";
            }

            if (totalTimeInDays > 319 && totalTimeInDays <= 547)
            {
                response = "a year";
            }

            if (totalTimeInDays > 547)
            {
                response = $"{Math.Ceiling(totalTimeInDays / DaysInAYear)} years";
            }

            if (direction == RelativityDirection.From)
            {
                return $"{response} {fromPostScript}";
            }

            if (direction == RelativityDirection.To)
            {
                return $"{toPreScript} {response}";
            }

            throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan,
                "The time span sent could not be parsed.");
        }
    }
}