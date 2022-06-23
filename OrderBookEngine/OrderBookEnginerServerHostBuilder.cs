using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderBookEngineServer.Core.ServerConfiguration;
using OrderBookEngineServer.Logging;
using OrderBookEngineServer.Logging.LoggingConfiguration;

namespace OrderBookEngineServer.Core
{
    public sealed class OrderBookEnginerServerHostBuilder
    {
        public static IHost BuildOrderBookEngineServer() =>
            Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                // Adding configurations
                services.AddOptions();
                services.Configure<OrderBookEngineServerConfiguration>(context.Configuration.GetSection(nameof(OrderBookEngineServerConfiguration)));
                services.Configure<LoggerConfiguration>(context.Configuration.GetSection(nameof(LoggerConfiguration)));
                //Adding Singleton Objects
                services.AddSingleton<IOrderBookEngineServer, OrderBookEngineServer>();
                services.AddSingleton<ITextLogger, TextLogger>();

                // Adding Hosted Service
                services.AddHostedService<OrderBookEngineServer>();
            }).Build();
    }
}
