using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infra.Data.Mapping
{
    public class TaskMap : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("Task");

            builder.HasOne(t => t.Project)
                .WithMany(p => p.Task)
                .HasForeignKey(t => t.ProjectId)
            .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Task)
                .HasForeignKey(t => t.UserName)
            .IsRequired();

            builder.HasMany(t=>t.Comment)
                .WithOne(c=>c.Task)
                .HasForeignKey(c=>c.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
