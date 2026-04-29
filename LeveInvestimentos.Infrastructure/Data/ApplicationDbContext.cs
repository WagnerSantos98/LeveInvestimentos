using LeveInvestimentos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeveInvestimentos.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<AppTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Email).IsUnique();

                entity.OwnsOne(e => e.Address, a => 
                {
                    a.Property(p => p.ZipCode).HasColumnName("ZipCode");
                    a.Property(p => p.Street).HasColumnName("Street");
                    a.Property(p => p.Number).HasColumnName("Number");
                    a.Property(p => p.Neighborhood).HasColumnName("Neighborhood");
                    a.Property(p => p.City).HasColumnName("City");
                    a.Property(p => p.State).HasColumnName("State");
                });
            });

            modelBuilder.Entity<AppTask>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);

                entity.HasOne(e => e.Creator)
                      .WithMany(u => u.CreatedTasks)
                      .HasForeignKey(e => e.CreatorId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Assignee)
                      .WithMany(u => u.AssignedTasks)
                      .HasForeignKey(e => e.AssigneeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
