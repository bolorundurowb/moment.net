using System;
using System.Globalization;
using System.Threading;

namespace moment.net.Localization;

/// <summary>
/// Changes the current thread's CurrentCulture and CurrentUICulture to allow accessing
/// embedded resources from the corresponding culture.
/// </summary>
internal class CultureWrapper : IDisposable
{
    private readonly CultureInfo _prevCi;
    private readonly CultureInfo _prevUiCi;

    private static bool UseCurrentThreadCultureAsDefault => true;

    private static CultureInfo DefaultCulture { get; } = CultureInfo.InvariantCulture;

    public CultureWrapper(CultureInfo ci)
    {
        _prevCi = Thread.CurrentThread.CurrentCulture;
        _prevUiCi = Thread.CurrentThread.CurrentUICulture;
        Thread.CurrentThread.CurrentCulture = ci;
        Thread.CurrentThread.CurrentUICulture = ci;
    }

    public static CultureInfo GetDefaultCulture() =>
        UseCurrentThreadCultureAsDefault ? Thread.CurrentThread.CurrentCulture : DefaultCulture;

    public void Dispose()
    {
        Thread.CurrentThread.CurrentCulture = _prevCi;
        Thread.CurrentThread.CurrentUICulture = _prevUiCi;
    }
}