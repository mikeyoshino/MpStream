using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addDbSetForMovieVideoAndPlayerAndLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePlayer_MovieVideo_MovieVideoId",
                table: "MoviePlayer");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieVideo_MovieEntity_MovieEntityId",
                table: "MovieVideo");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieVideo_MovieLanguage_MovieLanguageId",
                table: "MovieVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieVideo",
                table: "MovieVideo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePlayer",
                table: "MoviePlayer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieLanguage",
                table: "MovieLanguage");

            migrationBuilder.RenameTable(
                name: "MovieVideo",
                newName: "MovieVideos");

            migrationBuilder.RenameTable(
                name: "MoviePlayer",
                newName: "MoviePlayers");

            migrationBuilder.RenameTable(
                name: "MovieLanguage",
                newName: "MovieLanguages");

            migrationBuilder.RenameIndex(
                name: "IX_MovieVideo_MovieLanguageId",
                table: "MovieVideos",
                newName: "IX_MovieVideos_MovieLanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieVideo_MovieEntityId",
                table: "MovieVideos",
                newName: "IX_MovieVideos_MovieEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePlayer_MovieVideoId",
                table: "MoviePlayers",
                newName: "IX_MoviePlayers_MovieVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieVideos",
                table: "MovieVideos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePlayers",
                table: "MoviePlayers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieLanguages",
                table: "MovieLanguages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePlayers_MovieVideos_MovieVideoId",
                table: "MoviePlayers",
                column: "MovieVideoId",
                principalTable: "MovieVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieVideos_MovieEntity_MovieEntityId",
                table: "MovieVideos",
                column: "MovieEntityId",
                principalTable: "MovieEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieVideos_MovieLanguages_MovieLanguageId",
                table: "MovieVideos",
                column: "MovieLanguageId",
                principalTable: "MovieLanguages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePlayers_MovieVideos_MovieVideoId",
                table: "MoviePlayers");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieVideos_MovieEntity_MovieEntityId",
                table: "MovieVideos");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieVideos_MovieLanguages_MovieLanguageId",
                table: "MovieVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieVideos",
                table: "MovieVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePlayers",
                table: "MoviePlayers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieLanguages",
                table: "MovieLanguages");

            migrationBuilder.RenameTable(
                name: "MovieVideos",
                newName: "MovieVideo");

            migrationBuilder.RenameTable(
                name: "MoviePlayers",
                newName: "MoviePlayer");

            migrationBuilder.RenameTable(
                name: "MovieLanguages",
                newName: "MovieLanguage");

            migrationBuilder.RenameIndex(
                name: "IX_MovieVideos_MovieLanguageId",
                table: "MovieVideo",
                newName: "IX_MovieVideo_MovieLanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieVideos_MovieEntityId",
                table: "MovieVideo",
                newName: "IX_MovieVideo_MovieEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePlayers_MovieVideoId",
                table: "MoviePlayer",
                newName: "IX_MoviePlayer_MovieVideoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieVideo",
                table: "MovieVideo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePlayer",
                table: "MoviePlayer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieLanguage",
                table: "MovieLanguage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePlayer_MovieVideo_MovieVideoId",
                table: "MoviePlayer",
                column: "MovieVideoId",
                principalTable: "MovieVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieVideo_MovieEntity_MovieEntityId",
                table: "MovieVideo",
                column: "MovieEntityId",
                principalTable: "MovieEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieVideo_MovieLanguage_MovieLanguageId",
                table: "MovieVideo",
                column: "MovieLanguageId",
                principalTable: "MovieLanguage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
