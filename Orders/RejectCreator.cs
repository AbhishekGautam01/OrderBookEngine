using OrderBookEngineServer.Orders;

namespace OrderBookEngineServer.Rejects
{
    public sealed class RejectCreator
    {
        public static Rejection GenerateOrderRejection(IOrderCore rejectedOrder, RejectionReason rejectionReason)
        {
            return new Rejection(rejectedOrder, rejectionReason);
        }
    }
}
