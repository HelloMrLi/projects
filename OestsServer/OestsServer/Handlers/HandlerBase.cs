using Photon.SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OestsServer.Handlers
{
    public abstract class HandlerBase
    {
        public abstract OperationResponse OnOperationMessage(OperationRequest request);

    }
}
