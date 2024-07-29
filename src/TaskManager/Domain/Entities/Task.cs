namespace TaskManager.Domain.Entities
{
    public class Task : BaseEntity
    {
        /*
    - Cada tarefa deve ter uma prioridade atribuída (baixa, média, alta).
    - Não é permitido alterar a prioridade de uma tarefa depois que ela foi criada.
         */
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public PriorityTask Priority { get; set; }
        public StatusTask Status { get; set; }
        public string Details { get; set; }
        public User User { get; set; }
        public string UserName { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<TaskHistory> TaskHistory { get; set; }
        public ICollection<Comment> Comment { get; set; }
    }

    public enum PriorityTask
    {
        Low,
        Medium, 
        High
    }

    public enum StatusTask
    {
        Pending,
        Doing,
        Done,
    }
}
