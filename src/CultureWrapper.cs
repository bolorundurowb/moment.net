﻿using System;
using System.Globalization;
using System.Threading;

namespace moment.net
{
    /// <summary>
    /// Changes the current thread's CurrentCulture and CurrentUICulture to allow accessing
    /// embedded resources from the corresponding culture.
    /// </summary>
    public class CultureWrapper : IDisposable
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

        public static bool UseCurrentThreadCultureAsDefault { get; set; } = true;

        public static CultureInfo DefaultCulture { get; set; } = CultureInfo.InvariantCulture;

        public static CultureInfo GetDefaultCulture()
        {
            return UseCurrentThreadCultureAsDefault ? Thread.CurrentThread.CurrentCulture : DefaultCulture;
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _prevCi;
            Thread.CurrentThread.CurrentUICulture = _prevUiCi;
        }
    }
}