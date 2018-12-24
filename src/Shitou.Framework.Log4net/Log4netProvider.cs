
using log4net;
using log4net.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Shitou.Framework.Log4net
{
    public class Log4netProvider: ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();

        private ILoggerRepository _loggerRepository;

        public Log4netProvider(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }

        public ILogger CreateLogger(string name)
        {
            return _loggers.GetOrAdd(name, new Log4netAdapter(_loggerRepository.Name, name));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
