
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Shitou.Framework.Nlog
{
    public class NLogtProvider: ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger> _loggers =
            new ConcurrentDictionary<string, Microsoft.Extensions.Logging.ILogger>();

        public NLogtProvider()
        {
            
        }

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string name)
        {
            return _loggers.GetOrAdd(name, new NLogAdapter(name));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
