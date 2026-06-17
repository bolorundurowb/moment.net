using System.Globalization;
using MomentNet.I18n;

namespace MomentNet.Display.Models;

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
        ci ??= CultureInfo.CurrentCulture;
        var rm = Strings.ResourceManager;
        string baseSuffix = $" '{rm.GetString("TIME_AT", ci)}' hh:mm tt";
        SameDay = $"'{rm.GetString("TIME_TODAY", ci)}'" + baseSuffix;
        NextDay = $"'{rm.GetString("TIME_TOMORROW", ci)}'" + baseSuffix;
        NextWeek = "dddd" + baseSuffix;
        LastDay = $"'{rm.GetString("TIME_YESTERDAY", ci)}'" + baseSuffix;
        LastWeek = $"'{rm.GetString("TIME_LAST", ci)}' dddd" + baseSuffix;
        EverythingElse = "MM/dd/yyyy";
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