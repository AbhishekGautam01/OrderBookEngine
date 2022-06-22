using System;
using System.Collections.Generic;
using System.Text;

namespace OrderBookEngineServer.Logging
{
    abstract class AbstractLogger : ILogger
    {

        protected AbstractLogger()
        { }
        
        /// <summary>
        /// This method would allow any specifc implementation of logger class to configure logging the way it should be
        /// </summary>
        /// <param name="logLevel">Level of Log: Info, Warn, Error, Debug</param>
        /// <param name="module">Module from which log has originated</param>
        /// <param name="message">Log Message</param>
        protected abstract void Log(LogLevel logLevel, string module, string message);

        public void Debug(string module, string message) => Log(LogLevel.Debug, module, message);
        public void Debug(string module, Exception exception) => Log(LogLevel.Debug, module, $"{exception}");
        public void Error(string module, string message) => Log(LogLevel.Error, module, message);
        public void Error(string module, Exception exception) => Log(LogLevel.Error, module, $"{exception}");
        public void Inforation(string module, Exception exception) => Log(LogLevel.Information, module, $"{exception}");
        public void Information(string module, string message) => Log(LogLevel.Information, module, message);
        public void Warning(string module, string message) => Log(LogLevel.Warning, module, message);
        public void Warning(string module, Exception exception) => Log(LogLevel.Warning, module, $"{exception}");
        
    }
}
