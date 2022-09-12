using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DependencyInjectionConsole
{
    [TestClass]
    public class DependencyInjection
    {
        [TestMethod]
        public void DependencyInjectionTest()
        {
            /* Packages
             * ------------IConfiguration-------------
             *  -   Microsoft.Extensions.Configuration
             *  -   Microsoft.Extensions.Configuration.Json
             *  -   Microsoft.Extensions.Configuration.Binder
             *  -   other extension can be used, all extension start with Microsoft.Extensions.Configuration
             *  
             *  ------------DependencyInjection------------
             *  -   Microsoft.Extensions.DependencyInjection
             *  -   Microsoft.Extensions.Logging
             */
            IConfiguration Configuration = new ConfigurationBuilder()
                                                .AddJsonFile(@"appsettings.json", optional: true, reloadOnChange: true)
                                                .Build();

            var address = new Address();
            Configuration.GetSection("Address").Bind(address);

            var services = new ServiceCollection()
                               .AddLogging()
                               .AddDbContext<EnvironmentManagementSystemContext>()
                               .AddTransient<EnvironmentManagementSystemService>()
                               .AddSingleton<IConfiguration>(Configuration)
                               .BuildServiceProvider();
        }
    }
}
