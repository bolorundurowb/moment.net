using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MomentNet
{
    internal class LocalizationManager : IDisposable
    {
        private readonly CultureWrapper _cw;
        private readonly ResourceManager _rm;

        public LocalizationManager(CultureInfo ci)
        {
            _cw = new CultureWrapper(ci);
            _rm = new ResourceManager(Globals.STRINGS, Assembly.GetExecutingAssembly());
        }

        public string GetString(string key) => _rm.GetString(key);

        public void Dispose()
        {
            _cw.Dispose();
        }
    }
}