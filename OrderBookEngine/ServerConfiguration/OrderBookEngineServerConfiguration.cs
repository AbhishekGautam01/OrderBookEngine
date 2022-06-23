namespace OrderBookEngineServer.Core.ServerConfiguration
{
    class OrderBookEngineServerConfiguration
    {
        public OrderBookEngineServerSettings OrderBookEngineServerSettings { get; set; }
    }

    class OrderBookEngineServerSettings
    {
        public int Port { get; set; }
    }
}
