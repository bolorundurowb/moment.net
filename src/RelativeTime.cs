using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

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

            if (totalTimeInSeconds > 44.0 && totalTimeInSeconds<= 89.0)
            {
                return $"{Math.Ceiling(totalTimeInSeconds)} seconds ago";
            }

            var totalTimeInMinutes = timeSpan.TotalMinutes;

            if (totalTimeInSeconds > 89 && totalTimeInMinutes <= 44)
            {
                return $"{Math.Ceiling(totalTimeInMinutes)} minutes ago";
            }

            if (totalTimeInSeconds > 44 && totalTimeInMinutes <= 89)
            {
                return "an hour ago";
            }
        }
    }
}