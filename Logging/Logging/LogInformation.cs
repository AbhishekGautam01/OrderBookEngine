using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBookEngineServer.Logging
{
    /// <summary>
    /// Immuateble record type acting as a buffer to store logs so we can pull in to our main method responsible to logs
    /// </summary>
    /// <param name="Loglevel"></param>
    /// <param name="module"></param>
    public record LogInformation(LogLevel loglevel, string module, string message, DateTime now, int threadId, string threadName);
}


/// <summary>
/// This has been added due to bug in visual studio 2022 and C# 9 compatibility 
/// </summary>
namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { };
}