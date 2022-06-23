namespace OrderBookEngineServer.Orders
{
    /// <summary>
    /// Immutable order core class
    /// </summary>
    public class OrderCore : IOrderCore
    {
        public OrderCore(long orderId, string userName, int securityId)
        {
            OrderId = orderId;
            UserName = userName;
            SecurityId = securityId;
        }
        public long OrderId { get; private set; }
        public string UserName { get; private set; }
        public int SecurityId { get; private set; }
    }
}
