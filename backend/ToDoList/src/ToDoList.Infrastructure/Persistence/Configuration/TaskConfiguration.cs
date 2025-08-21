using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Domain.Tasks;

namespace ToDoList.Infrastructure.Persistence.Configuration
{
    public class TaskConfiguration : IEntityTypeConfiguration<ToDoTask>
    {
        public void Configure(EntityTypeBuilder<ToDoTask> builder)
        {
            // Configuración de la entidad ToDoTask
            builder.ToTable("Tasks");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(500);
            builder.Property(t => t.IsCompleted).IsRequired();
            builder.Property(t => t.CreatedAt).IsRequired();
            builder.Property(t => t.DueDate);
            builder.Property(t => t.UserId).IsRequired();

            builder.Property(e => e.ModifiedAt);
            builder.Property(e => e.DeletedAt);
            builder.Property(e => e.IsDeleted).IsRequired();
            builder.Property(e => e.Color).HasMaxLength(7); // Assuming color is stored as a hex string

            // Query Filter: Filtra automáticamente las tareas eliminadas
            builder.HasQueryFilter(t => !t.IsDeleted);
        }
    }
}