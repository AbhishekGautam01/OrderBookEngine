namespace OrderBookEngineServer.Orders
{
    /// <summary>
    /// An immutable contract holding user order metadata
    /// </summary>
    public interface IOrderCore
    {
        public long OrderId { get; }
        public string UserName { get; }
        public int SecurityId { get; }
    }
}
