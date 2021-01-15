using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class AddGenreNameToNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "MoviePlayers");

            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "MovieWithGenres",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "MovieWithGenres");

            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "MoviePlayers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
