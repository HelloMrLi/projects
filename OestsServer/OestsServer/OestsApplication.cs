﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net;
using log4net.Config;
using ExitGames.Logging;
using OestsServer.Handlers;
using OestsCommon;

namespace OestsServer
{
    public class OestsApplication:ApplicationBase
    {
        #region 单列
        private static OestsApplication _instance;
        public static OestsApplication Instance
        {
            get { return _instance; }
        }

        public OestsApplication() { _instance = this; }
        #endregion

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public  Dictionary<byte, HandlerBase> handlers = new Dictionary<byte, HandlerBase>();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ChatPeer(initRequest);
        }

      

        protected override void Setup()
        {
            InitLogging();//初始化日志功能

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
            handlers.Add((byte)OperationCode.Login, new LoginHandler());
            handlers.Add((byte)OperationCode.LoadServer, new ServerHandler());
        }

    }
}
