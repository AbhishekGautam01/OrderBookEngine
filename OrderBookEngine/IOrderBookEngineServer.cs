using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrderBookEngineServer.Core
{
    interface IOrderBookEngineServer
    {
        Task Run(CancellationToken token);
    }
}
