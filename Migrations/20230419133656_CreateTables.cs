using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystemAPI.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "PersonMovies",
                columns: table => new
                {
                    PersonMovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FkPersonId = table.Column<int>(type: "int", nullable: false),
                    FkMovieId = table.Column<int>(type: "int", nullable: false),
                    PersonRating = table.Column<int>(type: "int", nullable: false),
                    MoviesMovieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMovies", x => x.PersonMovieId);
                    table.ForeignKey(
                        name: "FK_PersonMovies_Movies_MoviesMovieId",
                        column: x => x.MoviesMovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId");
                    table.ForeignKey(
                        name: "FK_PersonMovies_People_FkPersonId",
                        column: x => x.FkPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_PersonMovies_FkPersonId",
                table: "PersonMovies",
                column: "FkPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMovies_MoviesMovieId",
                table: "PersonMovies",
                column: "MoviesMovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonMovies");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
