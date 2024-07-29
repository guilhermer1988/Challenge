using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;

namespace TaskManager.Infra.Data.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");

            builder.HasOne(c => c.User)
                .WithMany(u => u.Comment)
                .HasForeignKey(c => c.UserName)
            .IsRequired();

            builder.HasMany(c => c.TaskHistories)
                .WithOne(u => u.Comment)
                .HasForeignKey(c => c.CommentId);
        }
    }
}
