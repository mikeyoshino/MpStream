using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addMoreMoviePlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePlayers");

            migrationBuilder.RenameColumn(
                name: "Player",
                table: "MovieEntity",
                newName: "PlayerTwo");

            migrationBuilder.AddColumn<string>(
                name: "PlayerFour",
                table: "MovieEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerOne",
                table: "MovieEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerThree",
                table: "MovieEntity",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerFour",
                table: "MovieEntity");

            migrationBuilder.DropColumn(
                name: "PlayerOne",
                table: "MovieEntity");

            migrationBuilder.DropColumn(
                name: "PlayerThree",
                table: "MovieEntity");

            migrationBuilder.RenameColumn(
                name: "PlayerTwo",
                table: "MovieEntity",
                newName: "Player");

            migrationBuilder.CreateTable(
                name: "MoviePlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmbedCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePlayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviePlayers_MovieEntity_MovieEntityId",
                        column: x => x.MovieEntityId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePlayers_MovieEntityId",
                table: "MoviePlayers",
                column: "MovieEntityId");
        }
    }
}
