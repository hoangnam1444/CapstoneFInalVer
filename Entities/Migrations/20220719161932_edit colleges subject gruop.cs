using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editcollegessubjectgruop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Colleges_SubjectGroup",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FKColleges_SubjectGroup_Major",
                table: "Colleges_SubjectGroup",
                column: "MajorId",
                principalTable: "Majors",
                principalColumn: "MajorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKColleges_SubjectGroup_Major",
                table: "Colleges_SubjectGroup");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Colleges_SubjectGroup");
        }
    }
}
