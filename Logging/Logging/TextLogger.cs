using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

using Microsoft.Extensions.Options;

using OrderBookEngineServer.Logging.LoggingConfiguration;


namespace OrderBookEngineServer.Logging
{
    public class TextLogger : AbstractLogger, ITextLogger
    {
        private readonly LoggerConfiguration _loggerConfiguration;
        public TextLogger(IOptions<LoggerConfiguration> loggingConfiguration): base()
        {
            _loggerConfiguration = loggingConfiguration.Value ?? throw new ArgumentNullException(nameof(loggingConfiguration));
            if (_loggerConfiguration.LoggerType != LoggerType.Text)
                throw new InvalidOperationException($"{nameof(TextLogger)} doesn't match LoggerType of {_loggerConfiguration.LoggerType}");
            var now = DateTime.Now;
            string logDirectory = Path.Combine(_loggerConfiguration.TextLoggerConfiguration.Directory, $"{now:yyyy-MM-dd}");
            string baseLogName = Path.ChangeExtension($"{_loggerConfiguration.TextLoggerConfiguration.FileName}--{now:HH_mm_ss}", _loggerConfiguration.TextLoggerConfiguration.FileExtension);
            Directory.CreateDirectory(logDirectory);
            string filePath = Path.Combine(logDirectory, baseLogName);  
            _ = Task.Run(() => LogAsync(filePath, _logQueue, _tokenSource.Token));
        }

        private static async Task LogAsync(string filepath, BufferBlock<LogInformation> logQueue, CancellationToken cancellationToken)
        {
            using var fileStream = new FileStream(filepath, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
            using var streamWriter = new StreamWriter(fileStream) { AutoFlush = true };
            try
            {
                while (true)
                {

                    var logItem = await logQueue.ReceiveAsync(cancellationToken).ConfigureAwait(false);
                    string formattedMessage = FormatLogItem(logItem);
                    await streamWriter.WriteLineAsync(formattedMessage).ConfigureAwait(false);  
                }
            } catch (OperationCanceledException)
            {

            }
        }

        private static string FormatLogItem(LogInformation logItem)
        {
            return $"[{logItem.now:HH-mm-ss.ffffff}] [{logItem.threadName}:{logItem.threadId:000}] [{logItem.loglevel}] [{logItem.message}]";
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock(_lock)
            if (_disposed)
                return;
            _disposed = true;
            if (disposing)
            {
                // Get rid of managed resources 
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }
        }

        ~TextLogger()
        {
            Dispose(false);
        }
        /// <summary>
        /// This will log on a different thread instead of the main thread on which it was called. 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="module"></param>
        /// <param name="message"></param>
        protected override void Log(LogLevel logLevel, string module, string message)
        {
            _logQueue.Post(new LogInformation(logLevel, module, message, DateTime.UtcNow, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name));
        }

        /// <summary>
        /// Thread Safe queue which have async APIs
        /// </summary>
        private readonly BufferBlock<LogInformation> _logQueue = new BufferBlock<LogInformation>();
        private readonly CancellationTokenSource _tokenSource =  new CancellationTokenSource();
        private readonly object _lock = new object();
        private bool _disposed = false;
    }
}
