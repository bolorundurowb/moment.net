using System.Globalization;
using moment.net.Localization;

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
    /// Initialises a new instance and sets the time formats to standard defaults based on culture.
    /// </summary>
    public CalendarTimeFormats(CultureInfo? ci = null)
    {
        ci ??= CultureWrapper.GetDefaultCulture();

        using var lm = new LocalizationManager(ci);
        var baseSuffix = $" '{lm.GetString("TIME_AT")}' hh:mm tt";
        SameDay = $"'{lm.GetString("TIME_TODAY")}'" + baseSuffix;
        NextDay = $"'{lm.GetString("TIME_TOMORROW")}'" + baseSuffix;
        NextWeek = "dddd" + baseSuffix;
        LastDay = $"'{lm.GetString("TIME_YESTERDAY")}'" + baseSuffix;
        LastWeek = $"'{lm.GetString("TIME_LAST")}' dddd" + baseSuffix;
        EverythingElse = "MM/dd/yyyy";
    }

    /// <summary>
    /// Initialises a new instance, allowing for manual provisioning of format strings expected for each calendar time group.
    /// </summary>
    /// <param name="sameDay">Format applied to dates evaluating to the same day.</param>
    /// <param name="nextDay">Format applied to dates evaluating to the subsequent day.</param>
    /// <param name="nextWeek">Format applied to dates evaluating to the upcoming week.</param>
    /// <param name="lastDay">Format applied to dates evaluating to the preceding day.</param>
    /// <param name="lastWeek">Format applied to dates evaluating to the preceding week.</param>
    /// <param name="everythingElse">Format applied to dates sitting outside the predefined bounds.</param>
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