using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Request
{
    public class TaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusTask Status { get; set; }
        public PriorityTask Priority { get; internal set; }
        public string Details { get; set; }
        public string UserName { get; set; }
        public int ProjectId { get; set; }
    }
}
