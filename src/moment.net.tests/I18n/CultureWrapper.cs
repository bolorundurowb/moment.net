using System;
using System.Globalization;
using System.Threading;

namespace MomentNet.I18n
{
    internal class CultureWrapper : IDisposable
    {
        private readonly CultureInfo _prevCi;
        private readonly CultureInfo _prevUiCi;

        public CultureWrapper(CultureInfo ci)
        {
            _prevCi = Thread.CurrentThread.CurrentCulture;
            _prevUiCi = Thread.CurrentThread.CurrentUICulture;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _prevCi;
            Thread.CurrentThread.CurrentUICulture = _prevUiCi;
        }
    }
}
