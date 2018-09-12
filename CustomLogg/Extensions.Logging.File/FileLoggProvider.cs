using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Extensions.Logging.File
{
    public class FileLoggProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, FileLogger> _fileLoggers = new ConcurrentDictionary<string, FileLogger>();

        public ILogger CreateLogger(string categoryName)
        {
            return _fileLoggers.GetOrAdd(categoryName, name => new FileLogger(name));
        }

        public void Dispose()
        {
            _fileLoggers.Clear();
        }
    }
}
