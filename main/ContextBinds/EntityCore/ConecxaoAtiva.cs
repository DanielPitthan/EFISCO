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
            IConfigurationSection conecxaoAtiva = configuration.GetSection("ProducaoNFeXML");

            return configuration.GetConnectionString("Server=192.168.1.252;Database=NFeXML;User Id=EFISCO;Password=Imperador; MultipleActiveResultSets=True;");
        }

        public static string StringConnectionBaseTotvs()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            IConfigurationSection conecxaoAtiva = configuration.GetSection("ProducaoProtheus");

            return configuration.GetConnectionString(conecxaoAtiva.Value);
        }
    }
}
