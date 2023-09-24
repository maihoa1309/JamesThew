using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project3.Data.Migrations
{
    public partial class updateTips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Tips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Tips");
        }
    }
}
