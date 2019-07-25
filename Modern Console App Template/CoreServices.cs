using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModernConsoleAppTemplate.SampleService;
using System;
using System.IO;

namespace ModernConsoleAppTemplate
{
    public class CoreServices
    {
        public static ServiceProvider AddDotCoreCoreServices()
        {
            var configurationRoot = InitConfig();

            var serviceProvider = InitDependencyInjection(configurationRoot);

            return serviceProvider;
        }

        private static IConfigurationRoot InitConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    //.AddJsonFile($"appsettings.{env}.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.local.json", optional: false, reloadOnChange: true);
                    //.AddEnvironmentVariables();

            return builder.Build();
        }

        private static ServiceProvider InitDependencyInjection(IConfigurationRoot configurationRoot)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configurationRoot);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot config)
        {
            services.AddLogging(configure =>
            {
                //wire up logging to configuration
                //pulled from C:\Users\mdepouw\source\repos\GitHub\spottedmahn\AspNetCore\src\DefaultBuilder\src\WebHost.cs
                //I can't find the current location on GitHub
                //todo should verify this the current way to do this
                configure.AddConfiguration(config.GetSection("Logging"));

                //configure.SetMinimumLevel(LogLevel.Trace);

                configure.AddConsole();
                //add VS output window
                configure.AddDebug();
            });

            //add custom services here
            services.AddTransient<IMyService, MyService>();

            //add custom configuration / options
            services.Configure<MyOptions>(config.GetSection(nameof(MyOptions)));
        }
    }

}
