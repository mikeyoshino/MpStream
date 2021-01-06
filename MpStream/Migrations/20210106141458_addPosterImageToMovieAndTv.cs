using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addPosterImageToMovieAndTv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poster",
                table: "MovieEntity");

            migrationBuilder.AddColumn<byte[]>(
                name: "PosterImage",
                table: "TvShowEntities",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PosterImage",
                table: "MovieEntity",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterImage",
                table: "TvShowEntities");

            migrationBuilder.DropColumn(
                name: "PosterImage",
                table: "MovieEntity");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "MovieEntity",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
