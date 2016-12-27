using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using OestsCommon.Model;

namespace OestsDataBase.Manager
{
    public static class TeacherManager
    {
        /// <summary>
        /// 教师
        /// </summary>
        public static void CreateTable()
        {
            string sqlCheck = "if object_id( 'T_Teacher') is not null select 1 else select 0";
            if ((int)SqlHelper.ExecuteScalar(sqlCheck) == 0)
            {
                string sqlCreacte = @"CREATE TABLE T_Teacher (id bigint identity(1,1) ,
                                    name NVARCHAR(20) primary key,password NVARCHAR(20),
major_id bigint not null,
login_time datetime,
logout_time datetime,
pattern NVARCHAR(20),
training_id int,exam_id int) ";
                SqlHelper.ExecuteNonQuery(sqlCreacte);
            }
        }
        public static Teacher Login(string name, string pwd)
        {
            string strSql = "select * from T_Teacher where name=@name and password=@pwd";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql, new SqlParameter("@name", name), new SqlParameter("@pwd", pwd));
            Teacher t = new Teacher();
            if (dt == null || dt.Rows.Count == 0)
            {
                t.DBid = -1;
            }
            else
            {
                t.DBid = Int32.Parse(dt.Rows[0]["id"].ToString());
                t.Name = name;
                t.PWD = pwd;
                t.OnLine = true;
                t.PatternType = dt.Rows[0]["pattern"].Equals(DBNull.Value) ? PatternType.Undefine : (PatternType)Enum.Parse(typeof(PatternType), dt.Rows[0]["pattern"].ToString());

                t.TrainingId = dt.Rows[0]["training_id"].Equals(DBNull.Value) ? 0 : Int32.Parse(dt.Rows[0]["training_id"].ToString());
                t.MajorId = dt.Rows[0]["major_id"].Equals(DBNull.Value) ? 0 : Int32.Parse(dt.Rows[0]["major_id"].ToString());
                t.ExamId = dt.Rows[0]["exam_id"].Equals(DBNull.Value) ? 0 : Int32.Parse(dt.Rows[0]["exam_id"].ToString());
            }
            return t;
        }

        public static void SaveLoginTime(int id)
        {
            DateTime date = DateTime.Now;
            string strSql = string.Format("update T_Teacher set login_time='{0}' where id = @id", date);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
        }

        public static void SaveLogoutTime(int id)
        {
            DateTime date = DateTime.Now;
            string strSql = string.Format("update T_Teacher set logout_time='{0}' where id = @id", date);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
        }

        public static int AddTeacher(Teacher t)
        {
            string strSql = string.Format("insert into T_Teacher (name, password,major_id) values('{0}','{1}',{2});SELECT @@Identity",
                t.Name, t.PWD, t.MajorId);
            object obj = SqlHelper.ExecuteScalar(strSql);
            return Int32.Parse(obj.ToString());
        }

        public static List<Teacher> GetData()
        {
            string strSql = "select * from T_Teacher";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql);
            if (dt == null) { return null; }
            List<Teacher> list = new List<Teacher>();
            foreach (DataRow dr in dt.Rows)
            {
                Teacher t = new Teacher();
                t.DBid = Int32.Parse(dr["id"].ToString());
                t.Name = dr["name"].ToString();
                t.PWD = dr["password"].ToString();
                t.MajorId = Int32.Parse(dr["major_id"].ToString());

                PatternType p = PatternType.Undefine;
                Enum.TryParse(dr["major_id"].ToString(), false, out p);
                t.PatternType = p;

                t.TrainingId = dr["training_id"].Equals(DBNull.Value) ? 0 : Int32.Parse(dr["training_id"].ToString());
                t.ExamId = dr["exam_id"].Equals(DBNull.Value) ? 0 : Int32.Parse(dr["exam_id"].ToString());

                list.Add(t);
            }
            return list;
        }
        /// <summary>
        /// 删除一条教师信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteTeacher(int id)
        {
            string strSql = string.Format("delete from T_Teacher where id = @id");
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
            if (n != 0)
            {
                return true;
            }
            return false;

        }

        public static bool ModifyTeacher(string name, string pwd)
        {
            string strSql = string.Format("update T_Teacher set password='{0}' where name = @name");
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@name", name), new SqlParameter("@type", UserType.Teacher.ToString()));
            if (n != 0)
            {
                return true;
            }
            return true;
        }

        public static bool ModifyTeacher(string name, string pwd, int id)
        {
            string strSql = string.Format("update T_Teacher set name='{0}', password='{1}'  where id = @id", name, pwd);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
            if (n != 0)
            {
                return true;
            }
            return true;
        }

        public static bool ModifyTeacher(string name, string pwd, int majorid, int id)
        {
            string strSql = string.Format("update T_Teacher set name='{0}', password='{1}',major_id ={2}  where id = @id", name, pwd, majorid);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
            if (n != 0)
            {
                return true;
            }
            return true;
        }

        public static bool ModifyTeacher(string name, string pwd, string onlineState, int id)
        {
            return true;
        }

        public static bool ModifyTeacherName(string name, int id)
        {
            string strSql = string.Format("update T_Teacher set name='{0}'  where id = @id", name);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
            if (n != 0)
            {
                return true;
            }
            return false;
        }

        public static bool ModifyTeacherPwd(string pwd, int id)
        {
            string strSql = string.Format("update T_Teacher set password='{0}'  where id = @id", pwd);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", id));
            if (n != 0)
            {
                return true;
            }
            return false;
        }

        public static bool SetPattern(int teacherId, string pattern, int trainingId, int examId)
        {
            string strSql = string.Format("update T_Teacher set pattern='{0}',training_id ={1},exam_id ={2}  where id = @id",
                pattern, trainingId, examId);
            int n = SqlHelper.ExecuteNonQuery(strSql, new SqlParameter("@id", teacherId));
            if (n != 0)
            {
                return true;
            }
            return false;
        }

        public static Pattern GetPattern(int teacherId)
        {
            string strSql = string.Format("select * from T_Teacher  where id = @id");
            DataTable dt = SqlHelper.ExecuteDataTable(strSql, new SqlParameter("@id", teacherId));
            if (dt == null) { return null; }
            if (dt.Rows.Count == 1)
            {
                Pattern p = new Pattern();
                DataRow dr = dt.Rows[0];
                if (DBNull.Value.Equals(dr["pattern"]))
                    return null;
                string pattern = dr["pattern"].ToString();
                string train = DBNull.Value.Equals(dr["training_id"]) ? "0" : dr["training_id"].ToString();
                string exam = DBNull.Value.Equals(dr["exam_id"]) ? "0" : dr["exam_id"].ToString();

                p.type = (PatternType)Enum.Parse(typeof(PatternType), pattern, false);

                if (p.type == PatternType.Train)
                {
                    p.data = Int32.Parse(train);
                }
                else if (p.type == PatternType.Exam)
                {
                    p.data = Int32.Parse(exam);
                }
                return p;
            }

            return null;
        }

    }
}
