using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ContextBinds.EntityCore
{
    public static class ConecxaoAtiva
    {
        public static string StringConnectionBaseWebFrame()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            var conecxaoAtiva = configuration.GetSection("ConecxaoAtiva");

            return configuration.GetConnectionString(conecxaoAtiva.Value);
        }

        public static string StringConnectionBaseNfe()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            var conecxaoAtiva = configuration.GetSection("ConecxaoAtivaBaseNfe");

            return configuration.GetConnectionString(conecxaoAtiva.Value);
        }
        
        public static string StringConnectionBaseTotvs()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();
            var conecxaoAtiva = configuration.GetSection("ConecxaoAtivaProtheus");

            return configuration.GetConnectionString(conecxaoAtiva.Value);
        }
    }
}
