using System;

namespace moment.net
{
    public static class RelativeTime
    {
        private const int _daysInAWeek = 7;
        private const double _daysInAYear = 365.2425; // see https://en.wikipedia.org/wiki/Gregorian_calendar
        private const double _daysInAMonth = _daysInAYear / 12;

        public static string FromNow(this DateTime This)
        {
            if (This.Kind == DateTimeKind.Utc)
            {
                return TimeFromTimeSpan(DateTime.UtcNow - This);
            }

            return TimeFromTimeSpan(DateTime.Now - This);
        }

        private static string TimeFromTimeSpan(TimeSpan timeSpan)
        {
            var totalTimeInSeconds = timeSpan.TotalSeconds;

            if (totalTimeInSeconds <= 44.0)
            {
                return "a few seconds ago";
            }

            if (totalTimeInSeconds > 44.0 && totalTimeInSeconds <= 89.0)
            {
                return $"{Math.Ceiling(totalTimeInSeconds)} seconds ago";
            }

            var totalTimeInMinutes = timeSpan.TotalMinutes;

            if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            {
                return $"{Math.Ceiling(totalTimeInMinutes)} minutes ago";
            }

            if (totalTimeInMinutes > 44 && totalTimeInMinutes <= 89)
            {
                return "an hour ago";
            }

            var totalTimeInHours = timeSpan.TotalHours;

            if (totalTimeInMinutes > 89 && totalTimeInHours <= 21)
            {
                return $"{Math.Ceiling(totalTimeInHours)} hours ago";
            }

            if (totalTimeInHours > 21 && totalTimeInHours <= 35)
            {
                return "a day ago";
            }

            var totalTimeInDays = timeSpan.TotalDays;

            if (totalTimeInHours > 35 && totalTimeInDays <= 25)
            {
                return $"{Math.Ceiling(totalTimeInDays)} days ago";
            }

            if (totalTimeInDays > 25 && totalTimeInDays <= 45)
            {
                return "a month ago";
            }

            if (totalTimeInDays > 45 && totalTimeInDays <= 319)
            {
                return $"{Math.Floor(totalTimeInDays / _daysInAMonth)} months ago";
            }

            if (totalTimeInDays > 319 && totalTimeInDays <= 547)
            {
                return "a year ago";
            }

            if (totalTimeInDays > 547)
            {
                return $"{Math.Floor(totalTimeInDays / _daysInAYear)} years ago";
            }
            
            throw new ArgumentOutOfRangeException(nameof(timeSpan), timeSpan, "The time span sent could not be parsed.");
        }
    }
}