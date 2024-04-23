using Microsoft.EntityFrameworkCore;

namespace TodoApi.Entities
{
    public class ToDoContext: DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options): base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
               .HasOne(t => t.ParentToDo)
               .WithMany()
               .HasForeignKey(t => t.ParentToDoId)
               .OnDelete(DeleteBehavior.NoAction); // Cascade delete from parent to child

           // on delete category set todo category to null
            modelBuilder.Entity<ToDo>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.SetNull); // Set todo category to null when category is deleted

        }


    }
}
