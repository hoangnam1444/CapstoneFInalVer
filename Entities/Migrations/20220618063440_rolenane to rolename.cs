using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class rolenanetorolename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleNane",
                table: "Sys_User_Role");

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "Sys_User_Role",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "Sys_User_Role");

            migrationBuilder.AddColumn<string>(
                name: "RoleNane",
                table: "Sys_User_Role",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
