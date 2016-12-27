using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OestsDataBase.Manager
{
   public class ServerManager
    {
        public List<string> GetData()
        {
            List<string> list = new List<string>();
            list.Add("Jerry");
            list.Add("Jerry2");
            list.Add("Jerry3");
            return list;

        }
    }
}
