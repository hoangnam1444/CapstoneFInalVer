using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editsavepointforsysuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromGPA",
                table: "Colleges");

            migrationBuilder.DropColumn(
                name: "ToGPA",
                table: "Colleges");

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subject_User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false),
                    Point = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject_User", x => new { x.UserId, x.SubjectId });
                    table.ForeignKey(
                        name: "FKSubject_User_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKSubject_User_User",
                        column: x => x.UserId,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Major_SubjectGroup",
                columns: table => new
                {
                    MajorId = table.Column<int>(nullable: false),
                    SubjectGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major_SubjectGroup", x => new { x.MajorId, x.SubjectGroupId });
                    table.ForeignKey(
                        name: "FKMajor_SubjectGroup_Major",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKMajor_SubjectGroup_SubjectGroup",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subject_SubjectGroup",
                columns: table => new
                {
                    GroupSubjectId = table.Column<int>(nullable: false),
                    SubjectId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject_SubjectGroup", x => new { x.GroupSubjectId, x.SubjectId });
                    table.ForeignKey(
                        name: "FKSubject_SubjectGroup_SubjectGroup",
                        column: x => x.GroupSubjectId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKSubject_SubjectGroup_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroup_User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    SubjectGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroup_User", x => new { x.UserId, x.SubjectGroupId });
                    table.ForeignKey(
                        name: "FKSubjectGroup_User_Subject",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKSubjectGroup_User_User",
                        column: x => x.UserId,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Major_SubjectGroup");

            migrationBuilder.DropTable(
                name: "Subject_SubjectGroup");

            migrationBuilder.DropTable(
                name: "Subject_User");

            migrationBuilder.DropTable(
                name: "SubjectGroup_User");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "SubjectGroup");

            migrationBuilder.AddColumn<float>(
                name: "FromGPA",
                table: "Colleges",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "ToGPA",
                table: "Colleges",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
