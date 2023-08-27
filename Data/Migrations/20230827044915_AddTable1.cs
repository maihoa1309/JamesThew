using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project3.Data.Migrations
{
    public partial class AddTable1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_UserId1",
                table: "Recipes",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId1",
                table: "Recipes",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_UserId1",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_UserId1",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recipes");
        }
    }
}
