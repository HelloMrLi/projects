
namespace OestsCommon.Model
{
    /// <summary>
    /// 岗位任务  关系类型
    /// </summary>
    public class PostTask
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int TaskCode { get; set; }
        public string SceneName { get; set; }
    }
}
