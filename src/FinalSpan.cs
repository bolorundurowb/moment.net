﻿using System;
using System.Collections.Generic;
using System.Text;

namespace moment.net
{
    public class FinalSpan
    {
        private DateTime _dt;
        private readonly DayOfWeek _dayOfWeek;

        public FinalSpan(DateTime dateTime, DayOfWeek dayOfWeek)
        {
            _dt = dateTime;
            _dayOfWeek = dayOfWeek;
        }

        public DateTime InMonth()
        {
            var m = _dt.Month;
            // only loop through the last seven days of the month
            _dt = new DateTime(_dt.Year, _dt.Month, (DateTime.DaysInMonth(_dt.Year, _dt.Month) - 7), 0, 0, 0, _dt.Kind);
            while (_dt.Month == m)
            {
                if (_dt.DayOfWeek == _dayOfWeek)
                {
                    return _dt;
                }

                _dt = _dt.AddDays(1);
            }

            return DateTime.MaxValue;
        }

        public DateTime InYear()
        {
            var dt = new DateTime(_dt.Year, 12, (DateTime.DaysInMonth(_dt.Year, 12) - 7), 0, 0, 0, _dt.Kind);
            return new FinalSpan(dt, _dayOfWeek).InMonth();
        }
    }
}
