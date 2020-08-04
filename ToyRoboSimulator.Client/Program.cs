using Microsoft.Extensions.DependencyInjection;
using System;
using ToyRoboSimulator.Core;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Client
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run();
            DisposeServices();
            Console.WriteLine("Click key to close the console");
            Console.ReadKey();
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<IValidator, Validator>();
            services.AddTransient<ISimulator, Simulator>();
            services.AddTransient<ConsoleApplication>();
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}