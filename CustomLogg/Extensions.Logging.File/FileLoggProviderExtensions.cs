using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.Logging.File
{
    public static class FileLoggProviderExtensions
    {
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.Services.AddSingleton<ILoggerProvider, FileLoggProvider>();
            return loggingBuilder;
        }
    }
}
