using System;
using System.Collections.Generic;
using System.Text;

namespace OestsCommon.Model
{
   public class Major
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 专业隐藏
        /// </summary>
        public bool Activate { get; set; }

    }
}
