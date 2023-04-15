using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieSystemAPI.Models;
using System.Transactions;

namespace LeaveManagementSystem.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
