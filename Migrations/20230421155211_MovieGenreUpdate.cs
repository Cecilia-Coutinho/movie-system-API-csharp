using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystemAPI.Migrations
{
    public partial class MovieGenreUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Genres_FkGenreId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Movies_FkMovieId",
                table: "MovieGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.RenameTable(
                name: "MovieGenre",
                newName: "MovieGenres");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_FkMovieId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_FkMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_FkGenreId",
                table: "MovieGenres",
                newName: "IX_MovieGenres_FkGenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres",
                column: "MovieGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Genres_FkGenreId",
                table: "MovieGenres",
                column: "FkGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenres_Movies_FkMovieId",
                table: "MovieGenres",
                column: "FkMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Genres_FkGenreId",
                table: "MovieGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenres_Movies_FkMovieId",
                table: "MovieGenres");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenres",
                table: "MovieGenres");

            migrationBuilder.RenameTable(
                name: "MovieGenres",
                newName: "MovieGenre");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_FkMovieId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_FkMovieId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenres_FkGenreId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_FkGenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                column: "MovieGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Genres_FkGenreId",
                table: "MovieGenre",
                column: "FkGenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Movies_FkMovieId",
                table: "MovieGenre",
                column: "FkMovieId",
                principalTable: "Movies",
                principalColumn: "MovieId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
