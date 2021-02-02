using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addMovieCOmentToDbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieComment_MovieEntity_MovieEntityId",
                table: "MovieComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieComment",
                table: "MovieComment");

            migrationBuilder.RenameTable(
                name: "MovieComment",
                newName: "MovieComments");

            migrationBuilder.RenameIndex(
                name: "IX_MovieComment_MovieEntityId",
                table: "MovieComments",
                newName: "IX_MovieComments_MovieEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieComments",
                table: "MovieComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieComments_MovieEntity_MovieEntityId",
                table: "MovieComments",
                column: "MovieEntityId",
                principalTable: "MovieEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieComments_MovieEntity_MovieEntityId",
                table: "MovieComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieComments",
                table: "MovieComments");

            migrationBuilder.RenameTable(
                name: "MovieComments",
                newName: "MovieComment");

            migrationBuilder.RenameIndex(
                name: "IX_MovieComments_MovieEntityId",
                table: "MovieComment",
                newName: "IX_MovieComment_MovieEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieComment",
                table: "MovieComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieComment_MovieEntity_MovieEntityId",
                table: "MovieComment",
                column: "MovieEntityId",
                principalTable: "MovieEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
