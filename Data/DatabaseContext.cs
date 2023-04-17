using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieSystemAPI.Models;
using System.Text.Json;
using System.Transactions;

namespace MovieSystemAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasIndex(gt => gt.GenreTitle)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasIndex(e => e.Email)
                .IsUnique();
        }
    }
}
