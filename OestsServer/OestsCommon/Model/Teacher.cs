
using System;

namespace OestsCommon.Model
{
    /// <summary>
    /// 教员
    /// </summary>
    [Serializable]
    public class Teacher : User
    {
        public int MajorId { get; set; }

        public bool OnLine { get; set; }

        /// <summary>
        /// 模式设置类型
        /// </summary>
        public PatternType PatternType { get; set; }

        /// <summary>
        /// 实训配置 0 未配置
        /// </summary>
        public int TrainingId { get; set; }

        /// <summary>
        /// 考试配置 0 未配置
        /// </summary>
        public int ExamId { get; set; }


        public Teacher(string name, string pwd)
            : base(name, pwd)
        {
            Type = UserType.Teacher;
        }

        public Teacher()
        {
            Type = UserType.Teacher;
        }

    }
    public enum PatternType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        Undefine,
        /// <summary>
        /// 学员模式
        /// </summary>
        Student,
        /// <summary>
        /// 学习模式
        /// </summary>
        Learn,
        /// <summary>
        /// 实训模式
        /// </summary>
        Train,
        /// <summary>
        /// 考试模式
        /// </summary>
        Exam
    }

   
}
