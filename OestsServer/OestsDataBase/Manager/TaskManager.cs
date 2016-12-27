
using OestsCommon.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace OestsDataBase.Manager
{
    public class TaskManager
    {
        /// <summary>
        /// 创建任务表
        /// teacher_id:0代表内建任务，其他代表教师建的任务
        /// </summary>
        public static void CreateTable()
        {
            string sqlCheck = "if object_id( 'T_Task') is not null select 1 else select 0";
            if ((int)SqlHelper.ExecuteScalar(sqlCheck) == 0)
            {
                string sqlCreacte = @"CREATE TABLE T_Task (id bigint identity(1,1) primary key ,code int not null,name NVARCHAR(MAX) not null,
project_id bigint not null,defaulttime int not null,defaultscore int,teacher_id int not null,multiplayer int not null,detail NVARCHAR(MAX))";

                SqlHelper.ExecuteNonQuery(sqlCreacte);
            }
        }


        public static List<TaskModel> GetDataByTeacher(int teacherId)
        {
            string strSql = "select * from T_Task where teacher_id = 0 or teacher_id = " + teacherId;
            return GetData(strSql);
        }


        public static List<TaskModel> GetData()
        {
            string strSql = "select * from T_Task";
            return GetData(strSql);
        }

        public static TaskModel GetTaskById(int taskId)
        {
            string strSql = "select * from T_Task where id = " + taskId;
            List<TaskModel> list = GetData(strSql);
            if (list == null) return null;
            if (list.Count > 0)
                return GetData(strSql)[0];
            return null;
        }

        private static List<TaskModel> GetData(string sql)
        {
            List<TaskModel> list = new List<TaskModel>();
            DataTable dt = SqlHelper.ExecuteDataTable(sql);
            if (dt == null) { return null; }
            foreach (DataRow dr in dt.Rows)
            {
                TaskModel s = new TaskModel();
                s.Id = Int32.Parse(dr["id"].ToString());
                s.Code = Int32.Parse(dr["code"].ToString());
                s.TeacherId = Int32.Parse(dr["teacher_id"].ToString());
                s.Name = dr["name"].ToString();
                s.ProjectId = Int32.Parse(dr["project_id"].ToString());
                s.Detail = dr["detail"].ToString();

                s.DefaultTime = Int32.Parse(dr["defaulttime"].ToString());
                s.DefaultScore = dr["defaultscore"].Equals(DBNull.Value) ? 0 : Int32.Parse(dr["defaultscore"].ToString());
                s.MultiPlayer = Int32.Parse(dr["multiplayer"].ToString());
                //s.ExamPostId = dr["exam_post_id"].Equals(DBNull.Value) ? 0 : Int32.Parse(dr["exam_post_id"].ToString());
                //s.Score = dr["score"].Equals(DBNull.Value) ? 0 : Int32.Parse(dr["score"].ToString());
                list.Add(s);
            }
            return list;
        }

        public static int Add(TaskModel task)
        {
            string strSql = string.Format(@"insert into T_Task (code, name,   project_id,     detail,     defaulttime,    defaultscore, teacher_id)
                                                          values({0},'{1}',     {2},            '{3}',          {4},           {5},                {6});SELECT @@Identity",
                                                            task.Code, task.Name, task.ProjectId, task.Detail, task.DefaultTime, task.DefaultScore, task.TeacherId);
            object obj = SqlHelper.ExecuteScalar(strSql);
            return Int32.Parse(obj.ToString());
        }

        public static int Delete(int id)
        {
            string strSql = string.Format("delete from T_Task where id = @id");
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
            return n;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public static bool Modify(TaskModel model)
        //{
        //    string strSql = string.Format("update T_Task set code={0}, isbuiltin={1}, name ='{2}', project_id={3}, area_name='{4}',defaulttime={5},time={6},activate={7}  where id = @id",
        //        model.Code, Convert.ToInt32(model.IsBuiltIn), model.Name, model.ProjectId, model.AreaName, model.DefaultTime, model.SetTime, Convert.ToInt32(model.TrainingActive));
        //    int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", model.Id));
        //    if (n != 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
