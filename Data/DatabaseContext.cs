using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieSystemAPI.Models;
using System.Transactions;

namespace MovieSystemAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Genre> Genres { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("appsettings.json")
        //    .Build()
        //    .GetConnectionString("DefaultConnection"));
        //}
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasIndex(gt => gt.GenreTitle)
                .IsUnique();
        }
    }
}
