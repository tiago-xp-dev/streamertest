using System.IO;
using Microsoft.Extensions.Configuration;

namespace SS_API.Utils
{
    public static class ConfigurationUtils
    {
        private readonly static IConfigurationBuilder _builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        private readonly static IConfigurationRoot _configuration = _builder.Build();

        public static IConfigurationRoot Configuration { get => _configuration; }
    }
}