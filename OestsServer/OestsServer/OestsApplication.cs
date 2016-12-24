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

        private static OestsApplication _instance;
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        private Dictionary<byte, HandlerBase> handlers = new Dictionary<byte, HandlerBase>();

        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ChatPeer(initRequest);
        }

        public static OestsApplication Instance
        {
            get { return _instance; }
        }

       public OestsApplication(){ _instance = this; }

        protected override void Setup()
        {
            InitLogging();//初始化日志功能

            log.Debug("application setup complete");
            log.Info("傻吊");

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
            log.Info("DDD");
        }

        public void RegisteHandler()
        {
            handlers.Add((byte)OperationCode.Login, new LoginHandler());
        }

    }
}
