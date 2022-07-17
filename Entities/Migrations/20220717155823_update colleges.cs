using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatecolleges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollegeTypeID",
                table: "Colleges");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Colleges",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Colleges",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceLink",
                table: "Colleges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Colleges");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Colleges");

            migrationBuilder.DropColumn(
                name: "ReferenceLink",
                table: "Colleges");

            migrationBuilder.AddColumn<int>(
                name: "CollegeTypeID",
                table: "Colleges",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
