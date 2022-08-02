using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addtblusercolleges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_College",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    CollegeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_College", x => new { x.UserId, x.CollegeId });
                    table.ForeignKey(
                        name: "FKUser_College_College",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKUser_College_User",
                        column: x => x.UserId,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_College");
        }
    }
}
