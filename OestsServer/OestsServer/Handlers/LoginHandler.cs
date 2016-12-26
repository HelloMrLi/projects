
using OestsCommon;
using Photon.SocketServer;

namespace OestsServer.Handlers
{
    public class LoginHandler : HandlerBase
    {
        public override OperationCode OpCode { get {return OperationCode.Login;} }

        public override OperationResponse OnOperationMessage(OperationRequest request)
        {
            return null;
        }

    }
}
