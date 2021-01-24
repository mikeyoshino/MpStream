using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addMovieLikeToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieLike_MovieEntity_MovieEntityId",
                table: "MovieLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieLike",
                table: "MovieLike");

            migrationBuilder.RenameTable(
                name: "MovieLike",
                newName: "MovieLikes");

            migrationBuilder.RenameIndex(
                name: "IX_MovieLike_MovieEntityId",
                table: "MovieLikes",
                newName: "IX_MovieLikes_MovieEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieLikes",
                table: "MovieLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLikes_MovieEntity_MovieEntityId",
                table: "MovieLikes",
                column: "MovieEntityId",
                principalTable: "MovieEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieLikes_MovieEntity_MovieEntityId",
                table: "MovieLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieLikes",
                table: "MovieLikes");

            migrationBuilder.RenameTable(
                name: "MovieLikes",
                newName: "MovieLike");

            migrationBuilder.RenameIndex(
                name: "IX_MovieLikes_MovieEntityId",
                table: "MovieLike",
                newName: "IX_MovieLike_MovieEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieLike",
                table: "MovieLike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieLike_MovieEntity_MovieEntityId",
                table: "MovieLike",
                column: "MovieEntityId",
                principalTable: "MovieEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
