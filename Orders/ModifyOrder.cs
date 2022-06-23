namespace OrderBookEngineServer.Orders
{
    public class ModifyOrder : IOrderCore
    {
        public ModifyOrder(IOrderCore orderCore, long modifyPrice, uint modifyQuantity, bool isBuySide )
        {
            Price = modifyPrice;
            Quantity = modifyQuantity;    
            IsBuySide = isBuySide;  

            _orderCore = orderCore;
        }

        public long Price { get; private set; }
        public uint Quantity { get; private set; }    
        public bool IsBuySide { get; private set;}
        public long OrderId => _orderCore.OrderId;
        public string UserName => _orderCore.UserName;
        public int SecurityId => _orderCore.SecurityId;

        private readonly IOrderCore _orderCore;
        
        /// <summary>
        /// Because cancel order take any type of IOrderCore hence we can pass this to do cancellation
        /// </summary>
        /// <returns></returns>
        public CancelOrder ToCancelOrder()
        {
            return new CancelOrder(this);
        }

        public Order ToNewOrder()
        {
            return new Order(this);
        }
    }
}
