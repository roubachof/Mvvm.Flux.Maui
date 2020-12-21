using System;
using System.IO;
using System.Threading.Tasks;

using MetroLog;

namespace Symbiot.Mobile.Infrastructure.Logging
{
    public static class LoggerFactory
    {
        private static ILogManager logManager;

        public static void Initialize(LoggingConfiguration configuration)
        {
            logManager = LogManagerFactory.CreateLogManager(configuration);
        }

        public static ILogger GetLogger(string loggerName)
        {
            if (logManager == null)
            {
                throw new InvalidOperationException("LogFactory must be Initialized before creating any logger");
            }

            return logManager.GetLogger(loggerName);
        }

        public static ILogger TryGetLogger(string loggerName)
        {
            if (logManager == null)
            {
                return new ConsoleLogger();
            }

            return logManager.GetLogger(loggerName);
        }

        public static MemoryStream GetCompressedLogs()
        {
            Stream compressedLogs = null;
            Task.Run(async () => compressedLogs = await logManager.GetCompressedLogs()).Wait();

            return (MemoryStream)compressedLogs;
        }
    }
}
