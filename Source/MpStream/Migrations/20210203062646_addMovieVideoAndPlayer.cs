using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addMovieVideoAndPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PlayerTwo",
                table: "MovieEntity");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MovieComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "MovieComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MovieLanguage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieLanguage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieVideo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieEntityId = table.Column<int>(type: "int", nullable: false),
                    MovieLanguageId = table.Column<int>(type: "int", nullable: false),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieVideo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieVideo_MovieEntity_MovieEntityId",
                        column: x => x.MovieEntityId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieVideo_MovieLanguage_MovieLanguageId",
                        column: x => x.MovieLanguageId,
                        principalTable: "MovieLanguage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoviePlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EembedLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MovieVideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoviePlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoviePlayer_MovieVideo_MovieVideoId",
                        column: x => x.MovieVideoId,
                        principalTable: "MovieVideo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePlayer_MovieVideoId",
                table: "MoviePlayer",
                column: "MovieVideoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieVideo_MovieEntityId",
                table: "MovieVideo",
                column: "MovieEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieVideo_MovieLanguageId",
                table: "MovieVideo",
                column: "MovieLanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePlayer");

            migrationBuilder.DropTable(
                name: "MovieVideo");

            migrationBuilder.DropTable(
                name: "MovieLanguage");

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

            migrationBuilder.AddColumn<string>(
                name: "PlayerTwo",
                table: "MovieEntity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MovieComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "MovieComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
