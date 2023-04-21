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
        public DbSet<PersonGenre> PersonGenres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<PersonMovie> PersonMovies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasIndex(gt => gt.GenreTitle)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasIndex(e => e.Email)
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasIndex(t => t.MovieTitle)
                .IsUnique();

            modelBuilder.Entity<PersonGenre>()
                .HasOne(p => p.People)
                .WithMany(pg => pg.PersonGenres)
                .HasForeignKey(fkp => fkp.FkPersonId);

            modelBuilder.Entity<PersonGenre>()
                .HasOne(g => g.Genres)
                .WithMany(pg => pg.PersonGenres)
                .HasForeignKey(fkg => fkg.FkGenreId);

            modelBuilder.Entity<PersonMovie>()
                .HasOne(p => p.People)
                .WithMany(pg => pg.PersonMovies)
                .HasForeignKey(fkp => fkp.FkPersonId);

            modelBuilder.Entity<PersonMovie>()
                .HasOne(p => p.Movies)
                .WithMany(pg => pg.PersonMovies)
                .HasForeignKey(fkp => fkp.FkMovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(m => m.Movies)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(fkm => fkm.FkMovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(g => g.Genres)
                .WithMany(mg => mg.MovieGenres)
                .HasForeignKey(fkg => fkg.FkGenreId);
        }
        public async Task PeopleDataSeed()
        {
            var file = File.ReadAllText("PeopleDataSample.json");
            var peopleSeed = JsonSerializer.Deserialize<List<Person>>(file);

            if (People.Count() == 0 && peopleSeed != null)
            {
                People.AddRange(peopleSeed);
                SaveChanges();
            }
            await Task.CompletedTask;
        }
    }
}
