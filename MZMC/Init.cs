using Microsoft.Extensions.Logging;
using PCL.Core.MZMC.Helper;

namespace PCL.Core.MZMC
{
    public class Init
    {
        public static void Main()
        {
            // 初始化
            PCL.Core.MZMC.Helper.Log.Logger=new LoggerFactory().CreateLogger("MZMC");
            Log.Logger.LogInformation("初始化日志完成");

        }
    }
}