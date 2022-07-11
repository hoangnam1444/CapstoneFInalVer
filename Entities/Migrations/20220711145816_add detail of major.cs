using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class adddetailofmajor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MajorDetail",
                table: "Majors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MajorDetail",
                table: "Majors");
        }
    }
}
