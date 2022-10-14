using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourRecipe.Data.Migrations
{
    public partial class CreateTablesTest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryType",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Categories");
        }
    }
}
