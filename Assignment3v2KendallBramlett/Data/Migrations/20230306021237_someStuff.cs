using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3v2KendallBramlett.Data.Migrations
{
    public partial class someStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Headshot",
                table: "Actors",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IMBDLink",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Headshot",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "IMBDLink",
                table: "Actors");
        }
    }
}
