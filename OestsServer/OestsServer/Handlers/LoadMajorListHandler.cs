using LitJson;
using OestsCommon;
using Photon.SocketServer;
using System.Collections.Generic;
using OestsCommon.Model;
using OestsDataBase.Manager;

namespace OestsServer.Handlers
{
    public class LoadMajorListHandler : HandlerBase
    {
        MajorManager manager;

        public LoadMajorListHandler()
        {
            manager = new MajorManager();
        }
        public override OperationCode OpCode { get { return OperationCode.LoadMajorList; } }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer)
        {
            List<Major> list = manager.GetData();
            string json = JsonMapper.ToJson(list);
            response.ReturnCode = (short)ReturnCode.Success;
            response.Parameters.Add((byte)ParameterCode.MajorList, json);

        }
    }
}
