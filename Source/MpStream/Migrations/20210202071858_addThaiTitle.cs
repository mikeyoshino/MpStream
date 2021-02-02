using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addThaiTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TitleTH",
                table: "MovieEntity",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TitleTH",
                table: "MovieEntity");
        }
    }
}
