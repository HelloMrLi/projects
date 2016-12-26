using System;
using System.Collections.Generic;
using Photon.SocketServer;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net;
using log4net.Config;
using ExitGames.Logging;
using OestsServer.Handlers;
using System.Reflection;

namespace OestsServer
{
    public static class MyClass
    {
        public static string TryGetValueEx(this Dictionary<string, string> dic, string key)
        {

            if (dic == null)
                throw new Exception("Dictionary is NULL.");
            string value = string.Empty;
            if (!dic.TryGetValue(key, out value))
                throw new Exception("The given key:" + key + " was not present in the dictionary.");
            return value;
        }
    }

    public class OestsApplication:ApplicationBase
    {
        #region 单列
        private static OestsApplication _instance;
        public static OestsApplication Instance
        {
            get { return _instance; }
        }

        public OestsApplication() { _instance = this;  }
        #endregion

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public  Dictionary<byte, HandlerBase> handlers = new Dictionary<byte, HandlerBase>();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ClientPeer(initRequest);
        }

        protected override void Setup()
        {
            InitLogging();//初始化日志功能
            RegisteHandler();

            log.Debug("application setup complete");
        }

        protected virtual void InitLogging()
        {
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "OS" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));
        }
        protected override void TearDown()
        {
            log.Debug("application TearDown ");
        }

        public void RegisteHandler()
        {
            Type[] types = Assembly.GetAssembly(typeof(HandlerBase)).GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.EndsWith("Handler"))
                {
                    Activator.CreateInstance(type);
                }
            }
        }
    }
}
