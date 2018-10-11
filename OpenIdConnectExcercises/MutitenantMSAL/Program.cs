using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MutitenantMSAL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
                    {
                        configurationBuilder
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{webHostBuilderContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddJsonFile("tenantsettings.json", optional: false, reloadOnChange: true);
                    })
                //.UseKestrel()
                //.UseConfiguration(new ConfigurationBuilder()
                //            .SetBasePath(Directory.GetCurrentDirectory())
                //            .AddJsonFile("hosting.json", optional: true)
                //            .Build()
                //        )
                //.UseContentRoot(Directory.GetCurrentDirectory())
                //.UseIISIntegration()
                .UseStartup<Startup>();
    }
}
