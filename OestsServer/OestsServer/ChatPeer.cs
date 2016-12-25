using System.Collections;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using OestsCommon;
using OestsServer.Handlers;
using ExitGames.Logging;

namespace OestsServer
{
   public class ChatPeer:ClientPeer
    {
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();
        public ChatPeer(InitRequest initRequest) : base(initRequest)
        {
           
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler;
            OestsApplication.Instance.handlers.TryGetValue(operationRequest.OperationCode, out handler);
            if(handler != null)
            {
                OperationResponse response;
                response = handler.OnOperationMessage(operationRequest);
                SendOperationResponse(response, sendParameters);
            }
            else
            {
                log.Debug("can not find handler from operation code:"+ operationRequest.OperationCode);
            }
            
        }
    }
}
