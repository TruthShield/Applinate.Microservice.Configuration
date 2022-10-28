// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Applinate
{
    using Applinate.Configuration;

    public static class ConfigurationProvider
    {
        public static TConfig GetConfiguration<TConfig>() where TConfig : new()
        {
            return ServiceProvider.Locate<IConfiguration>().Get<TConfig>();
        }
    }
}