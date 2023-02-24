using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace moment.net
{
    internal class LocalizationManager : IDisposable
    {
        private CultureWrapper _cw;
        private ResourceManager _rm;

        public LocalizationManager(CultureInfo ci)
        {
            _cw = new CultureWrapper(ci);
            _rm = new ResourceManager(Globals.STRINGS, Assembly.GetExecutingAssembly());
        }

        public string GetString(string key)
        {
            return _rm.GetString(key);
        }

        public void Dispose()
        {
            _cw.Dispose();
        }
    }
}