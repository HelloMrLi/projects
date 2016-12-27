using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using OestsServer.Handlers;
using ExitGames.Logging;
using OestsCommon.Model;

namespace OestsServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {

        //当前登录的用户
        public User LoginUser { get; set; }

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
            log.Debug("A client is connect." + ConnectionId);

        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            //if (OestsApplication.Instance.clientPeerListForTeam.Contains(this))
            //{
            //    OestsApplication.Instance.clientPeerListForTeam.Remove(this);
            //}
            log.Debug("A client is disconnect." + ConnectionId);
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler;
            OestsApplication.Instance.handlers.TryGetValue(operationRequest.OperationCode, out handler);
            if (handler != null)
            {
                OperationResponse response = new OperationResponse();
                response.Parameters = new System.Collections.Generic.Dictionary<byte, object>();
                response.OperationCode = operationRequest.OperationCode;
                handler.OnHandlerMessage(operationRequest, response, this);
                SendOperationResponse(response, sendParameters);
            }
            else
            {
                log.Debug("can not find handler from operation code:" + operationRequest.OperationCode);
            }

        }
    }
}
