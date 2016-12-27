using System.Collections.Generic;

namespace OestsCommon.Model
{

    public class Paper
    {
        public int Id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        public int Time
        {
            get;
            set;
        }
        public int ProjectId { get; set; }

        /// <summary>
        /// 试卷中的任务
        /// </summary>
        public List<TaskModel> Tasks { get; set; }
    }
}
