using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment3v2KendallBramlett.Data.Migrations
{
    public partial class changedAlotOfThings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Actors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_MovieId",
                table: "Actors",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Movies_MovieId",
                table: "Actors");

            migrationBuilder.DropIndex(
                name: "IX_Actors_MovieId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Actors");
        }
    }
}
