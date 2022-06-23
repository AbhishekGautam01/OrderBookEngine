namespace OrderBookEngineServer.Orders
{
    public class CancelOrder: IOrderCore
    {
        public CancelOrder(IOrderCore orderCore)
        {
            _orderCore = orderCore;
        }

        private readonly IOrderCore _orderCore;

        public long OrderId => _orderCore.OrderId;
        public string UserName => _orderCore.UserName;
        public int SecurityId => _orderCore.SecurityId;
    }
}
