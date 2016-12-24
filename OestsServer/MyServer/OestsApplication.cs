using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace OestsServer
{
    public class OestsApplication:ApplicationBase
    {
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ChatPeer(initRequest);
        }

        protected override void TearDown()
        {
            
        }

        protected override void Setup()
        {
            
        }
    }
}
