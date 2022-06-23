using System;

namespace OrderBookEngineServer.Orders
{
    public class Order: IOrderCore
    {
        public Order(IOrderCore orderCore,long price, uint quanity, bool isBuySide)
        {
            Price = price;  
            IsBuySide = isBuySide;
            InitialQuanity = quanity;
            CurrentQuantity = quanity;

            _orderCore = orderCore;
        }

        public Order(ModifyOrder modifyOrder) : this(modifyOrder, modifyOrder.Price, modifyOrder.Quantity, modifyOrder.IsBuySide){ }

        public long Price { get; private set; }
        public uint InitialQuanity { get; private set; }
        public uint CurrentQuantity { get; private set; }   
        public bool IsBuySide { get; private set; }
        public long OrderId => _orderCore.OrderId;
        public string UserName => _orderCore.UserName;
        public int SecurityId => _orderCore.SecurityId;

        private readonly IOrderCore _orderCore;

        public void IncreateQuanity(uint quanitityDelta)
        {
            CurrentQuantity += quanitityDelta;
        }

        public void DecreaseQuantity(uint quanitityDelta)
        {
            if (quanitityDelta > CurrentQuantity)
                throw new InvalidOperationException($"Quanity delta > current quanitity for orderId={OrderId}");
            CurrentQuantity -= quanitityDelta;
        }
    }
}
