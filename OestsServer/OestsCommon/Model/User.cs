
using System;

namespace OestsCommon.Model
{
    [Serializable]
    public class User
    {
        /// <summary>
        /// 平台类型  管理系统和三维仿真平台
        /// </summary>
        public PlatformType Platform { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType Type { get; set; }

        public int DBid { get; set; }

        /// <summary>
        /// 学生时  为学号
        /// </summary>
        public string Name { get; set; }

        public string PWD { get; set; }

        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 该值会实时的刷新，保证异常退出时也会记录一个最近的值
        /// </summary>
        public DateTime LogoutTime { get; set; }

        public User()
        {
            Name = "";
            PWD = "";
            DBid = -1;
        }

        public User(string name, string pwd)
        {
            Name = name;
            PWD = pwd;
        }

        /// <summary>
        /// 计算在线时长
        /// </summary>
        /// <returns></returns>
        public string CalculationOnlineTime()
        {
            if (LoginTime == null || LogoutTime == null) return "0";
            TimeSpan ts = LogoutTime.Subtract(LoginTime);
            double hours = ts.Hours;//24.0
            double minutes = ts.Minutes;//60.0
            double seconds = ts.Seconds;//60.0
            return string.Format("{0}小时{1}分钟{2}秒", hours, minutes, seconds);
        }

    }
    public enum UserType
    {
        Admin,
        Teacher,
        Student,
        Visitor
    }
    public enum PlatformType
    {
        OSES,
        MS

    }
}
