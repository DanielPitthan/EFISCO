using Microsoft.Extensions.Configuration;
using System.IO;

namespace ContextBinds.EntityCore
{
    public static class ConecxaoAtiva
    {
        //public static string StringConnectionBaseWebFrame()
        //{
        //    var builder = new ConfigurationBuilder()
        //             .SetBasePath(Directory.GetCurrentDirectory())
        //             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //             .AddEnvironmentVariables();

        //    IConfiguration configuration = builder.Build();
        //    var conecxaoAtiva = configuration.GetSection("ConecxaoAtiva");

        //    return configuration.GetConnectionString(conecxaoAtiva.Value);
        //}

        public static string StringConnectionBaseNfe()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            IConfigurationSection conecxaoAtiva = configuration.GetSection("ConecxaoAtivaBaseNfe");

            return configuration.GetConnectionString(conecxaoAtiva.Value);
        }

        public static string StringConnectionBaseTotvs()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            IConfigurationSection conecxaoAtiva = configuration.GetSection("ConecxaoAtivaProtheus");

            return configuration.GetConnectionString(conecxaoAtiva.Value);
        }
    }
}
