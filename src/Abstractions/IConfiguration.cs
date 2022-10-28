// Copyright (c) TruthShield, LLC. All rights reserved.
namespace Applinate.Configuration
{
    public interface IConfiguration
    {
        public T Get<T>() where T : new();
    }
}