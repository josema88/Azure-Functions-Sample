using System;
using Microsoft.Extensions.Configuration;

namespace Azure_Functions_Sample.Helpers
{
    public class ConfigurationCustomProvider
    {
        public ConfigurationCustomProvider()
        {
        }

        public IConfigurationRoot GetConfiguration(string basePath)
        {
            return new ConfigurationBuilder()
                            .SetBasePath(basePath)
                            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
        }
    }
}
