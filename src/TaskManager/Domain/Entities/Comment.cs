namespace TaskManager.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }
        public User User { get; set; }
        public string UserName { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public ICollection<TaskHistory> TaskHistories { get; set; }
    }
}
