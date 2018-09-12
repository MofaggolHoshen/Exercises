using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LoggingAspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
            //.ConfigureLogging((webHostContext, loggingBuilder) =>
            //{
            //    var config = webHostContext.Configuration.GetSection("Logging");
            //    loggingBuilder.AddConsole();
            //    loggingBuilder.AddFile(config);
            //    loggingBuilder.AddAzureWebAppDiagnostics();
            //})
                .UseStartup<Startup>();
    }
}
