using System;

namespace OestsCommon.Model
{

    public class Student : User
    {

        /// <summary>
        /// 姓名
        /// </summary>
        public string RelName
        {
            get;
            set;
        }
        public string ClassName
        {
            get;
            set;
        }

        /// <summary>
        /// 属于教师  -1时表示未配置任何教师
        /// </summary>
        public int TeacherId
        {
            get;
            set;
        }

        /// <summary>
        /// 是否在线  true 在线
        /// </summary>
        public bool OnLine { get; set; }

        public int MajorId { get; set; }

        public int  ExamLog { get; set; }

        #region 实时状态属性

        /// <summary>
        /// 当前培训模式
        /// </summary>
        public PatternType CurrenPattern;

        /// <summary>
        /// 当前进行的任务
        /// </summary>
        [NonSerialized] public TaskModel CurrentTask;

        /// <summary>
        /// 当前进行的岗位
        /// </summary>
        [NonSerialized] public Post CurrentPost;

        #endregion
      
        public Student()
        {
            Type = UserType.Student;
            ExamLog = 0;
        }     

        public Student(string name, string pwd)
            : base(name, pwd)
        {
            Type = UserType.Student;
            ExamLog = 0;
        }
      
    }
}
