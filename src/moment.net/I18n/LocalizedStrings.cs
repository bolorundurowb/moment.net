using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MomentNet.I18n;

internal static class LocalizedStrings
{
#if NET6_0_OR_GREATER
    [DynamicDependency(DynamicallyAccessedMemberTypes.NonPublicProperties, typeof(Strings))]
#endif
    internal static string Get(string key, CultureInfo ci) =>
        key switch
        {
            "TIME_AGO" => Strings.ResourceManager.GetString("TIME_AGO", ci) ?? $"[missing:{key}]",
            "TIME_AT" => Strings.ResourceManager.GetString("TIME_AT", ci) ?? $"[missing:{key}]",
            "TIME_DAYS" => Strings.ResourceManager.GetString("TIME_DAYS", ci) ?? $"[missing:{key}]",
            "TIME_FEW_SECONDS" => Strings.ResourceManager.GetString("TIME_FEW_SECONDS", ci) ?? $"[missing:{key}]",
            "TIME_HOURS" => Strings.ResourceManager.GetString("TIME_HOURS", ci) ?? $"[missing:{key}]",
            "TIME_IN" => Strings.ResourceManager.GetString("TIME_IN", ci) ?? $"[missing:{key}]",
            "TIME_LAST" => Strings.ResourceManager.GetString("TIME_LAST", ci) ?? $"[missing:{key}]",
            "TIME_MINUTES" => Strings.ResourceManager.GetString("TIME_MINUTES", ci) ?? $"[missing:{key}]",
            "TIME_MONTHS" => Strings.ResourceManager.GetString("TIME_MONTHS", ci) ?? $"[missing:{key}]",
            "TIME_ONE_DAY" => Strings.ResourceManager.GetString("TIME_ONE_DAY", ci) ?? $"[missing:{key}]",
            "TIME_ONE_HOUR" => Strings.ResourceManager.GetString("TIME_ONE_HOUR", ci) ?? $"[missing:{key}]",
            "TIME_ONE_MINUTE" => Strings.ResourceManager.GetString("TIME_ONE_MINUTE", ci) ?? $"[missing:{key}]",
            "TIME_ONE_MONTH" => Strings.ResourceManager.GetString("TIME_ONE_MONTH", ci) ?? $"[missing:{key}]",
            "TIME_ONE_YEAR" => Strings.ResourceManager.GetString("TIME_ONE_YEAR", ci) ?? $"[missing:{key}]",
            "TIME_TODAY" => Strings.ResourceManager.GetString("TIME_TODAY", ci) ?? $"[missing:{key}]",
            "TIME_TOMORROW" => Strings.ResourceManager.GetString("TIME_TOMORROW", ci) ?? $"[missing:{key}]",
            "TIME_YEARS" => Strings.ResourceManager.GetString("TIME_YEARS", ci) ?? $"[missing:{key}]",
            "TIME_YESTERDAY" => Strings.ResourceManager.GetString("TIME_YESTERDAY", ci) ?? $"[missing:{key}]",
            _ => $"[missing:{key}]",
        };
}
