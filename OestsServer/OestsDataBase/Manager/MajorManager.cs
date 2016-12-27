using System;
using System.Collections.Generic;
using System.Data;
using OestsCommon.Model;

namespace OestsDataBase.Manager
{
   public class MajorManager
    {
        public  List<Major> GetData()
        {
            List<Major> list = new List<Major>();
            string strSql = "select * from T_Major";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt == null) { return null; }
            foreach (DataRow dr in dt.Rows)
            {
                Major s = new Major();
                s.Id = Int32.Parse(dr["id"].ToString());
                s.Name = dr["name"].ToString();
                bool b = false;
                Boolean.TryParse(dr["activate"].ToString(), out b);
                s.Activate = b;
                list.Add(s);
            }
            return list;

        }
    }
}
