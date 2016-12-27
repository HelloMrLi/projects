namespace OestsCommon.Model
{
    /// <summary>
    /// 任务
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// id 唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务编号 内建任务和新建任务可能编号相同
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 教师id 0 代表内建任务
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        public int ProjectId { get; set; }


        /// <summary>
        /// 详情
        /// </summary>
        public string Detail { get; set; }


        /// <summary>
        /// 默认时间
        /// </summary>
        public int DefaultTime { get; set; }

        /// <summary>
        /// 设置的时间
        /// </summary>
        public int SetTime { get; set; }

        /// <summary>
        /// 考试所考的岗位
        /// </summary>
        public int ExamPostId { get; set; }

        /// <summary>
        /// 默认分值
        /// </summary>
        public int DefaultScore { get; set; }

        /// <summary>
        /// 考试的分值
        /// </summary>
        public int Score { get; set; }


        /// <summary>
        /// 0 :仅单人  1：仅多人  2：可单人 也可多人 模式
        /// </summary>
        public int MultiPlayer { get; set; }


        public TaskModel() { }

    }


}
