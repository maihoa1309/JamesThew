using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project3.Data.Migrations
{
    public partial class Updateregister : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Registers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Registers");
        }
    }
}
