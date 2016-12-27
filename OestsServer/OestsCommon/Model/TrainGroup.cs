
using System.Collections.Generic;

namespace OestsCommon.Model
{
    public class TrainGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProjectId { get; set; }
        public int TeacherId { get; set; }
        public List<TaskModel> Tasks { get; set; }
    }
}
