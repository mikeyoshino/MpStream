using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MpStream.Migrations
{
    public partial class addMoreFieldToTvShowEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TvShowEntities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Backdrop_Path",
                table: "TvShowEntities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeRunTime",
                table: "TvShowEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstAirDate",
                table: "TvShowEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEpisode",
                table: "TvShowEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Score",
                table: "TvShowEntities",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "TvShowEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Backdrop_Path",
                table: "TvShowEntities");

            migrationBuilder.DropColumn(
                name: "EpisodeRunTime",
                table: "TvShowEntities");

            migrationBuilder.DropColumn(
                name: "FirstAirDate",
                table: "TvShowEntities");

            migrationBuilder.DropColumn(
                name: "NumberOfEpisode",
                table: "TvShowEntities");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "TvShowEntities");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "TvShowEntities");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TvShowEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
