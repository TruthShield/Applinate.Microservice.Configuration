// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Applinate.Configuration
{
    using System.Configuration;

    internal sealed class Config : Applinate.Configuration.IConfiguration
    {
        public T Get<T>() where T : new()
        {
            var response = new T();
            var responseProperties = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            var missingProperties = new List<string>();

            foreach (var prop in responseProperties)
            {
                string key = prop.Name;

                var value = GetValue(key);

                if (value is null)
                {
                    missingProperties.Add(key);
                    continue;
                }

                prop.SetValue(response, Convert.ChangeType(value, prop.PropertyType));
            }

            if (missingProperties.Count > 0)
            {
                ThrowMissingPropertyException(missingProperties);
            }

            return response;
        }

        private static void ThrowMissingPropertyException(IEnumerable<string> missing) =>
            throw new ConfigurationErrorsException(
                "Missing Properties: \n* " +
                string.Join("\n* ", missing.ToArray()) +
                "'.\n\nNot found in any of the following configuration stores: \n*" +
                string.Join("\n*", Roots.Value.SelectMany(x => x.Providers).Select(x => x.ToString()).ToArray()));

        private static string? GetValue(string name)
        {
            foreach (var configurationRoot in Roots.Value)
            {
                var result = configurationRoot.GetValue<string>(name);

                if (result is not null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}