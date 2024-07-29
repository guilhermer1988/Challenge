using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace TaskManager.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public TypeUser Type { get; set; }

        public ICollection<Project> Projects {  get; set; }
        public ICollection<Task> Task { get; set; }
        public ICollection<TaskHistory> TaskHistory { get; set; } 
        public ICollection<Comment> Comment { get; set; }
    }

    public enum TypeUser
    {
        Standard,
        Manager,
    }
}
