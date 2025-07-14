using System;
using PCL.Core.MZMC.Helper;
using PCL.Core.MZMC.API;
using System.Configuration;
using PCL.Core.App;


namespace PCL.Core.MZMC
{
    [LifecycleService(LifecycleState.BeforeLoading, Priority = 0)]
    public sealed class MZMCService : ILifecycleService
    {
        #region ILifecycleService 实现

        public string Identifier => "MZMCService";
        public string Name => "MZMC 相关服务";
        public bool SupportAsyncStart => false;

        private static LifecycleContext? _context;
        private MZMCService() { _context = Lifecycle.GetContext(this); }
        public static LifecycleContext Context => _context ?? throw new InvalidOperationException("MZMCService 未初始化");

        public void Start()
        {
            // 初始化
            Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if(Config.AppSettings.Settings["UserToken"]!=null)
                User.LoginByToken(Config.AppSettings.Settings["UserToken"].Value);
        }
        public void Stop() { _context = null; }

        #endregion

        public static Configuration Config;
        public static SDK.User User = new SDK.User();
    }
}