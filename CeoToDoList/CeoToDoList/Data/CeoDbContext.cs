using CeoToDoList.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CeoToDoList.Data
{
    public class CeoDbContext : DbContext
    {
        public CeoDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<CeoList> Lists { get; set; }
        public DbSet<CeoTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship
            modelBuilder.Entity<CeoList>()
                .HasMany(cl => cl.Tasks)
                .WithOne(ct => ct.CeoList)
                .HasForeignKey(ct => ct.ListId);
      
        }
    }
}
