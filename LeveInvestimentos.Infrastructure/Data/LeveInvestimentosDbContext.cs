using Microsoft.EntityFrameworkCore;
using LeveInvestimentos.Domain.Entities;

namespace LeveInvestimentos.Infrastructure.Data;

public class LeveInvestimentosDbContext : DbContext
{
    public LeveInvestimentosDbContext(DbContextOptions<LeveInvestimentosDbContext> options) : base(options){}

    public DbSet<AppUser> Users {get; set;}
    public DbSet<AppTask> Tasks {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração AppUser
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.MobilePhone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(250);
        });

        // Configuração AppTask
        modelBuilder.Entity<AppTask>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(500);
            
            // Relacionamentos
            entity.HasOne(e => e.CreatedBy)
                .WithMany(u => u.CreatedTasks)
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.AssignedTo)
                .WithMany(u => u.AssignedTasks)
                .HasForeignKey(e => e.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}