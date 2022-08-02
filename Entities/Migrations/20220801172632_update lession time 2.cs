using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatelessiontime2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Learning_Paths");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Learning_Paths",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
