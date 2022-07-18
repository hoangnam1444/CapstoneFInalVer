using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addcollegessubjectgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colleges_SubjectGroup",
                columns: table => new
                {
                    CollegesId = table.Column<int>(nullable: false),
                    SubjectGroupId = table.Column<int>(nullable: false),
                    Sum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges_SubjectGroup", x => new { x.CollegesId, x.SubjectGroupId });
                    table.ForeignKey(
                        name: "FKColleges_SubjectGroup_Colleges",
                        column: x => x.CollegesId,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKColleges_SubjectGroup_SubjectGroup",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colleges_SubjectGroup");
        }
    }
}
