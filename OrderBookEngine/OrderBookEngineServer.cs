using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading;
using System.Threading.Tasks;

using OrderBookEngineServer.Core.ServerConfiguration;

namespace OrderBookEngineServer.Core
{
    class OrderBookEngineServer : BackgroundService, IOrderBookEngineServer
    {
        private readonly ILogger<OrderBookEngineServer> _logger;
        private readonly OrderBookEngineServerConfiguration _config;
        public OrderBookEngineServer(ILogger<OrderBookEngineServer> logger, IOptions<OrderBookEngineServerConfiguration> config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _config = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// This method is there to make the execute async as by default we have done a protected override of this ExecuteAsync but we want to call it from other places.
        /// This will be used when we will not use microsoft hosting to call execute async for us. 
        /// </summary>
        /// <param name="token">Cancellation Token</param>
        /// <returns></returns>
        public Task Run(CancellationToken token) => ExecuteAsync(token);

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

            }
            return Task.CompletedTask;
        }
    }
}
