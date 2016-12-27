using OestsCommon;
using Photon.SocketServer;

namespace OestsServer.Handlers
{
    public abstract class HandlerBase
    {
        public abstract OperationCode OpCode { get; }
        public static readonly ExitGames.Logging.ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public HandlerBase()
        {
            //注册
            OestsApplication.Instance.handlers.Add((byte)OpCode, this);
            log.Debug("Hanlder:" + this.GetType().Name + "  is register.");
        }

        public abstract void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer);

    }
}
