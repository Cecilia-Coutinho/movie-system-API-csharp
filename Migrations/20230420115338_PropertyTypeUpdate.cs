using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieSystemAPI.Migrations
{
    public partial class PropertyTypeUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PersonRating",
                table: "PersonMovies",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PersonRating",
                table: "PersonMovies",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
