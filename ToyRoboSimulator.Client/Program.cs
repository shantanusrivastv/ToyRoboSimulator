using Microsoft.Extensions.DependencyInjection;
using System;
using ToyRoboSimulator.Core;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Client
{
    internal static class Program
    {
        private static ServiceProvider _serviceProvider;

        private static void Main()
        {
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
            services.AddTransient<IRoboMover, RoboMover>();
            services.AddTransient<ConsoleRoboClient>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            _serviceProvider = services.BuildServiceProvider(true);
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