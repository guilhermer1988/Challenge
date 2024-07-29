using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Request
{
    public class CommentRequest
    {
        public string Description { get; set; }
        public string UserName { get; set; }
        public int TaskId { get; set; }
    }
}
