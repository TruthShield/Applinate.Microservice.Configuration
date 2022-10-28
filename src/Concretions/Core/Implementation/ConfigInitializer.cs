// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Applinate.Configuration
{
    

    [InitializationPriority(1)]
    internal sealed class ConfigurationInitializer : IInitialize
    {
        public bool SkipDuringTesting => false;

        public void Initialize(IServiceCollection services, bool testing = false)
        {
            if (testing && SkipDuringTesting)
            {
                return;
            }

            ServiceProvider.RegisterTransient<IConfiguration>(s => new Config());
        }
    }
}