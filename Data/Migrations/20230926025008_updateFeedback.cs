using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project3.Data.Migrations
{
    public partial class updateFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RecipeId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_RecipeId",
                table: "Feedbacks",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Recipes_RecipeId",
                table: "Feedbacks",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Recipes_RecipeId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_RecipeId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Feedbacks");
        }
    }
}
