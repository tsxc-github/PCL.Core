using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace PCL.Core.MZMC.Helper
{
    public static class Log
    {
        public static ILogger Logger;
        static Log()
        {
            Logger = LoggerFactory.Create(builder =>
                {
                    builder.AddConsole();
                    #if DEBUG
                        builder.SetMinimumLevel(LogLevel.Trace);
                    #else
                        builder.SetMinimumLevel(LogLevel.Information);
                    #endif
                }
            ).CreateLogger("MZMC");
        }
    }
}