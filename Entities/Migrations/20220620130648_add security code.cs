using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addsecuritycode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "GPA12",
                table: "Sys_User",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "GPA11",
                table: "Sys_User",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<float>(
                name: "GPA10",
                table: "Sys_User",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Sys_User",
                nullable: true,
                defaultValueSql: "('0')",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "('0')");

            migrationBuilder.CreateTable(
                name: "SECURITY_CODE",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECURITY_CODE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SECURITY_CODE_Sys_User_UserId",
                        column: x => x.UserId,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SECURITY_CODE");

            migrationBuilder.AlterColumn<float>(
                name: "GPA12",
                table: "Sys_User",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "GPA11",
                table: "Sys_User",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "GPA10",
                table: "Sys_User",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Sys_User",
                type: "bit",
                nullable: false,
                defaultValueSql: "('0')",
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValueSql: "('0')");
        }
    }
}
