using System.Collections;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;
using OestsServer.Message;

namespace OestsServer
{
   public class ChatPeer:ClientPeer
    {
        Hashtable userTabel;
        public ChatPeer(InitRequest initRequest) : base(initRequest)
        {
            userTabel = new Hashtable();
            userTabel.Add("123", "1234");
        }

        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
              //取得Client端传过来的要求加以处理
            switch (operationRequest.OperationCode)
            {
                case (byte)OpCodeEnum.Login:
                    string uname = (string)operationRequest.Parameters[(byte)OpKeyEnum.UserName];
                    string pwd = (string)operationRequest.Parameters[(byte)OpKeyEnum.PassWord];
                    
                    if (userTabel.ContainsKey(uname) && userTabel[uname].Equals(pwd))
                    {
                        SendOperationResponse(new OperationResponse((byte)OpCodeEnum.LoginSuccess, null), new SendParameters());
                    }
                    else
                    {
                        SendOperationResponse(new OperationResponse((byte)OpCodeEnum.LoginFailed, null), new SendParameters());
                    }
                    break;
            }
        }
    }
}
