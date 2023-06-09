﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieSystemAPI.Data;

#nullable disable

namespace MovieSystemAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230420123324_PropertyTypeUpdate2")]
    partial class PropertyTypeUpdate2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MovieSystemAPI.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<string>("GenreDescription")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("GenreTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("GenreId");

                    b.HasIndex("GenreTitle")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.Movie", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"), 1L, 1);

                    b.Property<decimal>("MovieRating")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MovieTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MovieTmdbId")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.HasIndex("MovieTitle")
                        .IsUnique();

                    b.HasIndex("MovieTmdbId")
                        .IsUnique();

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PersonId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("People");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.PersonGenre", b =>
                {
                    b.Property<int>("PersonGenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonGenreId"), 1L, 1);

                    b.Property<int>("FkGenreId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.HasKey("PersonGenreId");

                    b.HasIndex("FkGenreId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("PersonGenres");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.PersonMovie", b =>
                {
                    b.Property<int>("PersonMovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonMovieId"), 1L, 1);

                    b.Property<int>("FkMovieId")
                        .HasColumnType("int");

                    b.Property<int>("FkPersonId")
                        .HasColumnType("int");

                    b.Property<decimal>("PersonRating")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PersonMovieId");

                    b.HasIndex("FkMovieId");

                    b.HasIndex("FkPersonId");

                    b.ToTable("PersonMovies");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.PersonGenre", b =>
                {
                    b.HasOne("MovieSystemAPI.Models.Genre", "Genres")
                        .WithMany("PersonGenres")
                        .HasForeignKey("FkGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieSystemAPI.Models.Person", "People")
                        .WithMany("PersonGenres")
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genres");

                    b.Navigation("People");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.PersonMovie", b =>
                {
                    b.HasOne("MovieSystemAPI.Models.Movie", "Movies")
                        .WithMany("PersonMovies")
                        .HasForeignKey("FkMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieSystemAPI.Models.Person", "People")
                        .WithMany("PersonMovies")
                        .HasForeignKey("FkPersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movies");

                    b.Navigation("People");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.Genre", b =>
                {
                    b.Navigation("PersonGenres");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.Movie", b =>
                {
                    b.Navigation("PersonMovies");
                });

            modelBuilder.Entity("MovieSystemAPI.Models.Person", b =>
                {
                    b.Navigation("PersonGenres");

                    b.Navigation("PersonMovies");
                });
#pragma warning restore 612, 618
        }
    }
}
