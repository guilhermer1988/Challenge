using System.Text.Json.Serialization;

namespace TaskManager.Domain.Entities
{
    public class Project : BaseEntity
    {
        /*
         * 2. **Restrições de Remoção de Projetos:**
    - Um projeto não pode ser removido se ainda houver tarefas pendentes associadas a ele.
    - Caso o usuário tente remover um projeto com tarefas 
        >pendentes<, 
        a API deve retornar um erro e sugerir a conclusão ou remoção das tarefas primeiro.
         */
        public string Title { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public string UserName { get; set; }
        public ICollection<Task> Task { get; set; }
    }
}
