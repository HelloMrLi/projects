using LitJson;
using OestsCommon;
using OestsServer.DB.Manager;
using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OestsServer.Handlers
{
    public class ServerHandler : HandlerBase
    {
        ServerManager manager;

        public ServerHandler()
        {
            manager = new ServerManager();
        }
        public override OperationResponse OnOperationMessage(OperationRequest request)
        {
            List<string> lsit = manager.GetData();
            string json = JsonMapper.ToJson(lsit);
            Dictionary<byte, object> parameters = new Dictionary<byte, object>(); 
            parameters.Add((byte)ParameterCode.ServerList, json);
            OperationResponse response = new OperationResponse();
            response.ReturnCode = (short)ReturnCode.Success;
            response.Parameters = parameters;
            response.OperationCode = request.OperationCode;
            return response;
        }
    }
}
