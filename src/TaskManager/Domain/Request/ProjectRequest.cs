using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Request
{
    public class ProjectRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}
