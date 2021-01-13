using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addTvshowNameToSeason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Seasons",
                newName: "SeasonNumber");

            migrationBuilder.AddColumn<string>(
                name: "TvshowName",
                table: "Seasons",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TvshowName",
                table: "Seasons");

            migrationBuilder.RenameColumn(
                name: "SeasonNumber",
                table: "Seasons",
                newName: "Name");
        }
    }
}
