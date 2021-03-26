using System.IO;
using Microsoft.Extensions.Configuration;

namespace SS_API.Utils
{
    /// <summary>
    /// Instância singleton para acesso seguro e simples a um <see cref="IConfigurationRoot"/> já configurado.
    /// </summary>
    public class ConfigurationUtils
    {
        private readonly static IConfigurationBuilder _builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        private readonly static IConfigurationRoot _configuration = _builder.Build();

        /// <summary>
        /// Acesso já configurado somente-leitura do <see cref="IConfigurationRoot"/>.
        /// </summary>
        public IConfigurationRoot Configuration { get => _configuration; }
    }
}