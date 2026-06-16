using System;
using System.Globalization;
using System.Threading;

namespace MomentNet.I18n
{
    /// <summary>
    /// Changes the current thread's CurrentCulture and CurrentUICulture to allow accessing
    /// embedded resources from the corresponding culture. Uses AsyncLocal to prevent culture
    /// leaks across async/await boundaries.
    /// </summary>
    public class CultureWrapper : IDisposable
    {
        private static readonly AsyncLocal<CultureInfo> _asyncCulture = new();
        private static readonly AsyncLocal<CultureInfo> _asyncUiCulture = new();

        private readonly CultureInfo _prevCi;
        private readonly CultureInfo _prevUiCi;

        public CultureWrapper(CultureInfo ci)
        {
            _prevCi = Thread.CurrentThread.CurrentCulture;
            _prevUiCi = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            _asyncCulture.Value = ci;
            _asyncUiCulture.Value = ci;
        }

        public static bool UseCurrentThreadCultureAsDefault { get; set; } = true;

        public static CultureInfo DefaultCulture { get; set; } = CultureInfo.InvariantCulture;

        public static CultureInfo GetDefaultCulture()
        {
            if (!UseCurrentThreadCultureAsDefault)
                return DefaultCulture;

            return _asyncCulture.Value ?? Thread.CurrentThread.CurrentCulture;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _prevCi;
            Thread.CurrentThread.CurrentUICulture = _prevUiCi;
            _asyncCulture.Value = _prevCi;
            _asyncUiCulture.Value = _prevUiCi;
        }
    }
}