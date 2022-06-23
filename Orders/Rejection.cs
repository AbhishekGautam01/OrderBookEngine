using OrderBookEngineServer.Orders;

namespace OrderBookEngineServer.Rejects
{
    /// <summary>
    /// To communicate order rejects in case order passes doesn't exist with us or some other issues with the order.
    /// </summary>
    public class Rejection: IOrderCore
    {
        public Rejection(IOrderCore rejectedOrder, RejectionReason rejectionReason)
        {
            _orderCore = rejectedOrder;
            RejectionReason = rejectionReason;
        }

        public RejectionReason RejectionReason { get; private set; }

        private readonly IOrderCore _orderCore;

        public long OrderId => _orderCore.OrderId;
        public string UserName => _orderCore.UserName;
        public int SecurityId => _orderCore.SecurityId;
    }
}
