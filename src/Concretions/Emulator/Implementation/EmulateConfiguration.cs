namespace Applinate
{
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
    using System;

    public static class EmulateConfiguration
    {
        private static Dictionary<string, string> _GlobalValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static AsyncLocal<Dictionary<string, string>> _LocalValues = new AsyncLocal<Dictionary<string, string>>();

        /// <summary>
        /// Sets an emulated config value.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="global">if <b>true</b>, sets the value for the lifetime of the application, otherwise the default is to only keep the value on the executing thread to avoid conflicts when running unit tests.</param>
        public static void SetValue(string key, string value, bool global = false)
        {
            if(global)
            {
                if(_GlobalValues.ContainsKey(key))
                {
                    _GlobalValues[key] = value;
                    return;
                }

                _GlobalValues.Add(key, value);
                return;
            }

            _LocalValues.Value ??= new(StringComparer.OrdinalIgnoreCase);

            if(_LocalValues.Value.ContainsKey(key))
            {
                _LocalValues.Value[key] = value;
                return;
            }

            _LocalValues.Value.Add(key, value);
        }
    
    
        public static string GetValue(string key)
        {
            if(_LocalValues.Value is not null && _LocalValues.Value.TryGetValue(key, out var localValue))
            {
                return localValue;
            }

            if(_GlobalValues.TryGetValue(key, out var globalValue))
            {
                return globalValue;
            }

            return null;
        }
    }

}