using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class initialProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Player = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sound = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrailerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieGenreEntities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieGenreEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoviePlayers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieEntityId = table.Column<int>(type: "int", nullable: false),
                    EmbedCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "MovieWithGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieEntityId = table.Column<int>(type: "int", nullable: false),
                    MovieGenreEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWithGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieWithGenres_MovieEntity_MovieEntityId",
                        column: x => x.MovieEntityId,
                        principalTable: "MovieEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieWithGenres_MovieGenreEntities_MovieGenreEntityId",
                        column: x => x.MovieGenreEntityId,
                        principalTable: "MovieGenreEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoviePlayers_MovieEntityId",
                table: "MoviePlayers",
                column: "MovieEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieWithGenres_MovieEntityId",
                table: "MovieWithGenres",
                column: "MovieEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieWithGenres_MovieGenreEntityId",
                table: "MovieWithGenres",
                column: "MovieGenreEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoviePlayers");

            migrationBuilder.DropTable(
                name: "MovieWithGenres");

            migrationBuilder.DropTable(
                name: "MovieEntity");

            migrationBuilder.DropTable(
                name: "MovieGenreEntities");
        }
    }
}
