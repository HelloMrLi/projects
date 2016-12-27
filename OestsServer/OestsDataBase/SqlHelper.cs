using System.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace OestsDataBase
{
    public class SqlHelper
    {
        //读取配置文件，获取连接数据库的字符串 注意：先要初始化Global._DbData类
        public static string connStr = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        /// <summary>
        /// 执行非查询语句，如delete、insert update等 创建表等
        /// <param name="sql"></param>
        /// <param name="sqlpars"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] sqlpars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlpars);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 执行只查询一行、一列的数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlpars"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] sqlpars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlpars);
                    return cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// 查询多个数据，返回的是一个DataTable,并且该结果集是保存在本地的
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlpars"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] sqlpars)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(sqlpars);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adp.Fill(dataset);
                    return dataset.Tables[0];
                }
            }
        }

        public static int CreateTable()
        {
            string sql = "CREATE TABLE myTable (myId INTEGER CONSTRAINT PKeyMyId PRIMARY KEY, myName CHAR(50), myAddress CHAR(255), myBalance FLOAT)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }


    }
}

