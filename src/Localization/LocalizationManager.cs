using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace moment.net.Localization;

internal class LocalizationManager : IDisposable
{
    private const string LocalisationResourceBaseName = "moment.net.Strings";

    private readonly CultureWrapper _cw;
    private readonly ResourceManager _rm;

    public LocalizationManager(CultureInfo ci)
    {
        _cw = new CultureWrapper(ci);
        _rm = new ResourceManager(LocalisationResourceBaseName, Assembly.GetExecutingAssembly());
    }

    public string? GetString(string key) => _rm.GetString(key);

    public void Dispose() => _cw.Dispose();
}