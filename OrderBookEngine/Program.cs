using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OrderBookEngineServer.Core;

// Building our host whcih would have DI, Hosted Service configured and built
using var engine = OrderBookEnginerServerHostBuilder.BuildOrderBookEngineServer();
OrderBookEngineServerServiceProvider.ServiceProvider = engine.Services;
{
    // Creating a manual scope before running in order to add scoped functionality
    using var scope = OrderBookEngineServerServiceProvider.ServiceProvider.CreateScope();
    await engine.RunAsync().ConfigureAwait(false); // Configure await to further optimize
}