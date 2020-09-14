using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using ToyRoboSimulator.Core;
using ToyRoboSimulator.Core.Commands;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Client
{
    internal static class Program
    {
        private static ServiceProvider _serviceProvider;

        private static void Main()
        {
            SetupSeriLog();
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleRoboClient>().Run();
            DisposeServices();
            Console.WriteLine("Closing the client");
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<IValidator, Validator>();
            services.AddTransient<ISimulator, Simulator>();
            services.AddTransient<ConsoleRoboClient>();
            services.AddSingleton<ICommandFactory, CommandFactory>();

            services.AddLogging(cfg =>
            {
                cfg.AddConsole();
                cfg.AddSerilog();
                //cfg.AddEventLog(x => x.SourceName = "Shantanu")
            });

            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void SetupSeriLog()
        {
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.File("logs\\log.txt")
                            .CreateLogger();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}