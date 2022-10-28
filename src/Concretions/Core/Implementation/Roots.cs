// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Applinate.Configuration
{


    internal static class Roots
    {
        private static readonly string _MASTER_CONFIG    = "master-config.json";
        private static readonly string _SECRETS_CONFIG   = "secrets.json";
        private static readonly string _ADDITIONAL_FILES = "additional-files";

        private static readonly Lazy<IConfigurationRoot[]> _Roots = new Lazy<IConfigurationRoot[]>(GetRoots().ToArray());

        private static IEnumerable<IConfigurationRoot> GetRoots()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder()
                    .AddJsonFile(_MASTER_CONFIG, false)
                    .AddEnvironmentVariables()
                    .AddInMemoryCollection();

            var mainRoot = builder.Build();

            yield return mainRoot;

            yield return BuildFileConfigurationRoot(_SECRETS_CONFIG, true);

            var additionalFiles = mainRoot.GetValue(_ADDITIONAL_FILES, "");

            if(string.IsNullOrWhiteSpace(additionalFiles))
            {
                yield break;
            }


            var fileNames = additionalFiles.Split(",").Select(x => x.Trim()).ToArray();

            foreach (var path in fileNames)
            {
                yield return BuildFileConfigurationRoot(path);
            }
        }

        private static IConfigurationRoot BuildFileConfigurationRoot(string path, bool optional = false) => 
            new ConfigurationBuilder()
            .AddJsonFile(path, optional, reloadOnChange: false)
            .Build();

        internal static IConfigurationRoot[] Value => _Roots.Value;
    }
}