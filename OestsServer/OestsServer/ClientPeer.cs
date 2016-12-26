using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using OestsServer.Handlers;
using ExitGames.Logging; 

namespace OestsServer
{
   public class ClientPeer: Photon.SocketServer.ClientPeer
    {
        
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
            
            log.Debug("A client is connect." + initRequest.ConnectionId);

        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            //if (OestsApplication.Instance.clientPeerListForTeam.Contains(this))
            //{
            //    OestsApplication.Instance.clientPeerListForTeam.Remove(this);
            //}
            log.Debug("A client is disconnect.");
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler;
            OestsApplication.Instance.handlers.TryGetValue(operationRequest.OperationCode, out handler);
            if (handler != null)
            {
                OperationResponse response;
                response = handler.OnOperationMessage(operationRequest);
                SendOperationResponse(response, sendParameters);
            }
            else
            {
                log.Debug("can not find handler from operation code:" + operationRequest.OperationCode);
            }

        }
    }
}
