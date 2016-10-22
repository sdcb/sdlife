using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace sdlife.web.unittest.Services
{
    public static class DbContextExtensions
    {
        public static void LogToDebug(this DbContext context)
        {
            var contextServices = ((IInfrastructure<IServiceProvider>)context).Instance;
            var loggerFactory = contextServices.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddDebug(LogLevel.Information);
        }
    }
}
