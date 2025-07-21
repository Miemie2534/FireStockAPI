using FireStockAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FireStockAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<FireExtinguisher> fireExtinguishers => Set<FireExtinguisher>();
        public DbSet<Claim> claims => Set<Claim>();
        public  DbSet<Incident> Incidents => Set<Incident>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Register> Registers => Set<Register>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FireExtinguisher>()
                .HasMany(f => f.RepairClaims)
                .WithOne(c => c.FireExtinguisher)
                .HasForeignKey(c => c.FireExtinguisherId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
