
using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Shitou.Framework.Log4net
{
    public class Log4netAdapter: ILogger
    {
        private ILog _logger;

        public Log4netAdapter(string repositoryName, string loggerName)
        {
            _logger = LogManager.GetLogger(repositoryName, loggerName);
        }

        
        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return _logger.IsDebugEnabled;
                case LogLevel.Information:
                    return _logger.IsInfoEnabled;
                case LogLevel.Warning:
                    return _logger.IsWarnEnabled;
                case LogLevel.Error:
                    return _logger.IsErrorEnabled;
                case LogLevel.Critical:
                    return _logger.IsFatalEnabled;
                default:
                    throw new ArgumentException($"Unknown log level {logLevel}.", nameof(logLevel));
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
            //throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            string message = null;
            if (null != formatter)
            {
                message = formatter(state, exception);
            }
            if (!string.IsNullOrEmpty(message) || exception != null)
            {
                switch (logLevel)
                {
                    case LogLevel.Critical:
                        _logger.Fatal(message);
                        break;
                    case LogLevel.Debug:
                    case LogLevel.Trace:
                        _logger.Debug(message);
                        break;
                    case LogLevel.Error:
                        _logger.Error(message, exception);
                        break;
                    case LogLevel.Information:
                        _logger.Info(message);
                        break;
                    case LogLevel.Warning:
                        _logger.Warn(message);
                        break;
                    default:
                        throw new ArgumentException($"Unknown log level {logLevel}.", nameof(logLevel));
                }
            }
        }
    }
}
