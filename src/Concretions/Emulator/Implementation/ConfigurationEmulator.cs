namespace Applinate.Configuration
{
    internal sealed class ConfigurationEmulator : IConfiguration
    {
        public T Get<T>() where T : new()
        {
            var response = new T();
            var responseProperties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            foreach (var prop in responseProperties)
            {
                string key = prop.Name;

                var value = EmulateConfiguration.GetValue(key);

                if (value is null)
                {
                    continue;
                }

                prop.SetValue(response, Convert.ChangeType(value, prop.PropertyType));
            }

            return response;
        }
    }
}