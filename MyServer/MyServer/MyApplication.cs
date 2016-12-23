using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;

namespace MyServer
{
    public class MyApplication:ApplicationBase
    {
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new MyPeer(initRequest);
        }

        protected override void TearDown()
        {
            
        }

        protected override void Setup()
        {
            
        }
    }
}
