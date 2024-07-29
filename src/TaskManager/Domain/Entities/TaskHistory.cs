namespace TaskManager.Domain.Entities
{
    public class TaskHistory : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public StatusTask Status { get; set; }
        public string Details { get; set; }
        public string Updated {  get; set; }
        public User UserUpdater { get; set; }
        public string UserNameUpdater { get; set; }
        public Task Task { get; set; }
        public int TaskId { get; set; }
        public Comment? Comment { get; set; }
        public int? CommentId { get; set; }
    }
}
