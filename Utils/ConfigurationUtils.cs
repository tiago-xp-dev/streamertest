using System.IO;
using Microsoft.Extensions.Configuration;

namespace SS_API.Utils
{
    public class ConfigurationUtils
    {
        private readonly static IConfigurationBuilder _builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        private readonly static IConfigurationRoot _configuration = _builder.Build();

        public IConfigurationRoot Configuration { get => _configuration; }
    }
}