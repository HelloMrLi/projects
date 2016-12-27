
using System.Collections.Generic;

namespace OestsCommon.Model
{
    /// <summary>
    /// 实训组
    /// </summary>
    public class TrainGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int TeacherId { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
