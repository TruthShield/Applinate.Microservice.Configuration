// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Applinate.Configuration
{
    using Microsoft.Extensions.DependencyInjection;

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

            Applinate.ServiceProvider.RegisterTransient<IConfiguration>(s => new ConfigurationEmulator());
        }
    }
}