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

        public override OperationResponse OnOperationMessage(OperationRequest request)
        {
            List<Major> list = manager.GetData();
            string json = JsonMapper.ToJson(list);
            Dictionary<byte, object> parameters = new Dictionary<byte, object>(); 
            parameters.Add((byte)ParameterCode.MajorList, json);

            OperationResponse response = new OperationResponse();
            response.ReturnCode = (short)ReturnCode.Success;
            response.Parameters = parameters;
            response.OperationCode = request.OperationCode;
            return response;
        }
    }
}
