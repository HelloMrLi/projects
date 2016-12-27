using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using OestsCommon.Model;

namespace OestsDataBase.Manager
{
    public static class AdminManager
    {
        //管理
        public static void CreateTable()
        {
            string sqlCheck = "if object_id( 'T_Admin') is not null select 1 else select 0";
            if ((int)SqlHelper.ExecuteScalar(sqlCheck) == 0)
            {
                string sqlCreacte = @"CREATE TABLE T_Admin (id bigint identity(1,1) ,
name NVARCHAR(20) primary key,password NVARCHAR(20),login_time datetime,logout_time datetime) ";
                SqlHelper.ExecuteNonQuery(sqlCreacte);

                string strSql = string.Format("insert into T_Admin (name, password) values('{0}','{1}');SELECT @@Identity", "Admin", "");
                SqlHelper.ExecuteScalar(strSql);
            }
        }

        public static Admin Login(string name)
        {
            string strSql = "select id from T_Admin where name=@name";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql, new SqlParameter("@name", name));
            Admin a = new Admin();
            if (dt == null || dt.Rows.Count == 0)
            {
                a.DBid = -1;
            }
            else
            {
                a.DBid = Int32.Parse(dt.Rows[0]["id"].ToString());
                a.Type = UserType.Admin;
                a.Name = dt.Rows[0]["name"].ToString();
                a.PWD = dt.Rows[0]["password"].ToString();
            }
            return a;
        }

        public static void SaveLoginTime(int id)
        {
            DateTime date = DateTime.Now;
            string strSql = string.Format("update T_Admin set login_time='{0}' where id = @id", date);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
        }

        public static void SaveLogoutTime(int id)
        {
            DateTime date = DateTime.Now;
            string strSql = string.Format("update T_Admin set logout_time='{0}' where id = @id", date);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
        }

        /// <summary>
        /// 只有一个管理员 不需要id
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static bool ModifyPwd(string pwd)
        {
            string strSql = string.Format("update T_Admin set password='{0}'", pwd);
            int n = SqlHelper.ExecuteNonQuery(strSql);
            if (n != 0)
            {
                return true;
            }
            return false;
        }

    }
}
