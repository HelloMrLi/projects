using LitJson;
using OestsCommon;
using Photon.SocketServer;
using System.Collections.Generic;
using OestsDataBase.Manager;

namespace OestsServer.Handlers
{
    public class ServerHandler : HandlerBase
    {
        ServerManager manager;

        public ServerHandler()
        {
            manager = new ServerManager();
        }
        public override OperationCode OpCode { get { return OperationCode.LoadServer; } }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer)
        {
            List<string> lsit = manager.GetData();
            string json = JsonMapper.ToJson(lsit);
            response.Parameters.Add((byte)ParameterCode.ServerList, json);
            response.ReturnCode = (short)ReturnCode.Success;
        }
    }
}
