using System;
using System.Collections.Generic;

using Prism.Logging;

namespace Symbiot.Mobile.Infrastructure.Logging
{
    public class PrismLoggerWrapper : ILogger
    {
        private readonly MetroLog.ILogger _logger;

        public PrismLoggerWrapper(MetroLog.ILogger logger)
        {
            _logger = logger;
        }

        public void TrackEvent(string name, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public void Report(Exception ex, IDictionary<string, string> properties)
        {
            throw new NotImplementedException();
        }

        public void Log(string message, IDictionary<string, string> properties)
        {
            _logger.Info(message);
        }
    }
}