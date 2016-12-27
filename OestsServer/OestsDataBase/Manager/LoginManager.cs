
using OestsCommon.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OestsDataBase.Manager
{
    public class LoginManager
    {
        public Student GetStudentByUserName(string username)
        {
            string strSql = "select * from T_Student where number=@number";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql, new SqlParameter("@number", username));
            Student s = new Student();
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            DataRow dr = dt.Rows[0];
            s.DBid = Int32.Parse(dr["id"].ToString());
            s.Name = dr["number"].ToString();
            s.PWD = dr["password"].ToString();
            s.ClassName = dr["class"].ToString();
            s.RelName = dr["name"].ToString();
            s.TeacherId = dr["teacher_id"].Equals(DBNull.Value) ? -1 : Int32.Parse(dr["teacher_id"].ToString());
            s.MajorId = Int32.Parse(dr["major_id"].ToString());
            return s;
        }

        public Teacher GetTeacherByUserName(string username)
        {
            string strSql = "select * from T_Teacher where name=@name";
            DataTable dt = SqlHelper.ExecuteDataTable(strSql, new SqlParameter("@name", username));
            Teacher t = new Teacher();
            if (dt == null || dt.Rows.Count == 0)
            {
                t.DBid = -1;
            }
            else
            {
                t.DBid = Int32.Parse(dt.Rows[0]["id"].ToString());
                t.Name = dt.Rows[0]["name"].ToString();
                t.PWD = dt.Rows[0]["password"].ToString();
                t.OnLine = false;
                t.PatternType = dt.Rows[0]["pattern"].Equals(DBNull.Value)
                    ? PatternType.Undefine
                    : (PatternType)Enum.Parse(typeof(PatternType), dt.Rows[0]["pattern"].ToString());

                t.TrainingId = dt.Rows[0]["training_id"].Equals(DBNull.Value)
                    ? 0
                    : Int32.Parse(dt.Rows[0]["training_id"].ToString());
                t.MajorId = dt.Rows[0]["major_id"].Equals(DBNull.Value)
                    ? 0
                    : Int32.Parse(dt.Rows[0]["major_id"].ToString());
                t.ExamId = dt.Rows[0]["exam_id"].Equals(DBNull.Value)
                    ? 0
                    : Int32.Parse(dt.Rows[0]["exam_id"].ToString());
            }
            return t;
        }

        public Admin GetAdminByUserName(string username)
        {
            return AdminManager.Login(username);
        }
    }
}
