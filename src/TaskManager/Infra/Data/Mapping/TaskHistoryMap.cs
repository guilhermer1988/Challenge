using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infra.Data.Mapping
{
    public class TaskHistoryMap : IEntityTypeConfiguration<TaskHistory>
    {
        public void Configure(EntityTypeBuilder<TaskHistory> builder)
        {
            builder.ToTable("TaskHistory");

            builder.HasOne(th => th.UserUpdater)
                .WithMany(u => u.TaskHistory)
                .HasForeignKey(th => th.UserNameUpdater)
            .IsRequired();
        }
    }
}
