using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace moment.net.Models;

public class CalendarTimeFormats
{
    public string SameDay { get; }

    public string NextDay { get; }

    public string NextWeek { get; }

    public string LastDay { get; }

    public string LastWeek { get; }

    public string EverythingElse { get; }

    /// <summary>
    /// Default constructor, sets the time formats to the default
    /// </summary>
    public CalendarTimeFormats(CultureInfo? ci = null)
    {
        if (ci is null)
            ci = CultureWrapper.GetDefaultCulture();

        using (var lm = new LocalizationManager(ci))
        {
            string baseSuffix = $" '{lm.GetString("TIME_AT")}' hh:mm tt";
            SameDay = $"'{lm.GetString("TIME_TODAY")}'" + baseSuffix;
            NextDay = $"'{lm.GetString("TIME_TOMORROW")}'" + baseSuffix;
            NextWeek = "dddd" + baseSuffix;
            LastDay = $"'{lm.GetString("TIME_YESTERDAY")}'" + baseSuffix;
            LastWeek = $"'{lm.GetString("TIME_LAST")}' dddd" + baseSuffix;
            EverythingElse = "MM/dd/yyyy";
        }
    }

    /// <summary>
    /// Overload constructor, allows for setting the format expected for each calendar time group 
    /// </summary>
    /// <param name="sameDay">Format for dates that fall on the same day</param>
    /// <param name="nextDay">Format for dates that fall on the next day</param>
    /// <param name="nextWeek">Format for dates that fall in the next week</param>
    /// <param name="lastDay">Format for dates that fall on the day before</param>
    /// <param name="lastWeek">Format for dates that fall in the preceding week</param>
    /// <param name="everythingElse">Format for dates that do not fall into the predefined categories</param>
    public CalendarTimeFormats(string sameDay, string nextDay, string nextWeek, string lastDay, string lastWeek,
        string everythingElse)
    {
        SameDay = sameDay;
        NextDay = nextDay;
        NextWeek = nextWeek;
        LastDay = lastDay;
        LastWeek = lastWeek;
        EverythingElse = everythingElse;
    }
}