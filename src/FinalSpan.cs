using System;
using System.Collections.Generic;
using System.Text;

namespace moment.net
{
    public class FinalSpan
    {
        private DateTime _dateTime;
        private readonly DayOfWeek _dayOfWeek;

        public FinalSpan(DateTime dateTime, DayOfWeek dayOfWeek)
        {
            _dateTime = dateTime;
            _dayOfWeek = dayOfWeek;
        }

        public DateTime InMonth()
        {
            var month = _dateTime.Month;
            // only loop through the last seven days of the month
            _dateTime = new DateTime(_dateTime.Year, _dateTime.Month, (DateTime.DaysInMonth(_dateTime.Year, _dateTime.Month) - 7), 0, 0, 0, _dateTime.Kind);
            while (_dateTime.Month == month)
            {
                if (_dateTime.DayOfWeek == _dayOfWeek)
                {
                    return _dateTime;
                }

                _dateTime = _dateTime.AddDays(1);
            }

            return DateTime.MaxValue;
        }

        public DateTime InYear()
        {
            var dateTime = new DateTime(_dateTime.Year, 12, (DateTime.DaysInMonth(_dateTime.Year, 12) - 7), 0, 0, 0, _dateTime.Kind);
            return new FinalSpan(dateTime, _dayOfWeek).InMonth();
        }
    }
}
