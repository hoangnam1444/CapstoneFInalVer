using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editcollegesmajor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Colleges_SubjectGroup",
                table: "Colleges_SubjectGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colleges_SubjectGroup",
                table: "Colleges_SubjectGroup",
                columns: new[] { "CollegesId", "SubjectGroupId", "MajorId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Colleges_SubjectGroup",
                table: "Colleges_SubjectGroup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colleges_SubjectGroup",
                table: "Colleges_SubjectGroup",
                columns: new[] { "CollegesId", "SubjectGroupId" });
        }
    }
}
