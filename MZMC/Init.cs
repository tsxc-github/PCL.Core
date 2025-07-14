using PCL.Core.MZMC.Helper;
using PCL.Core.MZMC.API;
using System.Configuration;

namespace PCL.Core.MZMC
{
    public class Init
    {
        public static Configuration Config;
        public static SDK.User User = new SDK.User();
        public static void Main()
        {
            // 初始化
            Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if(Config.AppSettings.Settings["UserToken"]!=null)
                User.LoginByToken(Config.AppSettings.Settings["UserToken"].Value);
        }
    }
}