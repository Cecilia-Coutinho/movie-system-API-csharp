using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystemAPI.Migrations
{
    public partial class TablesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GenreDescription = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieTmdbId = table.Column<int>(type: "int", nullable: false),
                    MovieTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieRating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PersonGenres",
                columns: table => new
                {
                    PersonGenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkPersonId = table.Column<int>(type: "int", nullable: false),
                    FkGenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGenres", x => x.PersonGenreId);
                    table.ForeignKey(
                        name: "FK_PersonGenres_Genres_FkGenreId",
                        column: x => x.FkGenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonGenres_People_FkPersonId",
                        column: x => x.FkPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMovies",
                columns: table => new
                {
                    PersonMovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkPersonId = table.Column<int>(type: "int", nullable: false),
                    FkMovieId = table.Column<int>(type: "int", nullable: false),
                    PersonRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMovies", x => x.PersonMovieId);
                    table.ForeignKey(
                        name: "FK_PersonMovies_Movies_FkMovieId",
                        column: x => x.FkMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonMovies_People_FkPersonId",
                        column: x => x.FkPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_GenreTitle",
                table: "Genres",
                column: "GenreTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieTitle",
                table: "Movies",
                column: "MovieTitle",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_MovieTmdbId",
                table: "Movies",
                column: "MovieTmdbId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Email",
                table: "People",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonGenres_FkGenreId",
                table: "PersonGenres",
                column: "FkGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGenres_FkPersonId",
                table: "PersonGenres",
                column: "FkPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMovies_FkMovieId",
                table: "PersonMovies",
                column: "FkMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMovies_FkPersonId",
                table: "PersonMovies",
                column: "FkPersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonGenres");

            migrationBuilder.DropTable(
                name: "PersonMovies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
