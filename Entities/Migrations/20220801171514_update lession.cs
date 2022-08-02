using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatelession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learning_Paths_LessionDetails_LessionDetailId",
                table: "Learning_Paths");

            migrationBuilder.DropTable(
                name: "LessionDetails");

            migrationBuilder.DropColumn(
                name: "LessionDetailId",
                table: "Learning_Paths");

            migrationBuilder.AddColumn<string>(
                name: "LessionDetailContent",
                table: "Learning_Paths",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Learning_Paths",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessionDetailContent",
                table: "Learning_Paths");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Learning_Paths");

            migrationBuilder.AddColumn<int>(
                name: "LessionDetailId",
                table: "Learning_Paths",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LessionDetails",
                columns: table => new
                {
                    LessionDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "('0')"),
                    LessionDetailContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LessionID = table.Column<int>(type: "int", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__40D9D999F16AA74B", x => x.LessionDetailID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Learning_Paths_LessionDetails_LessionDetailId",
                table: "Learning_Paths",
                column: "LessionDetailId",
                principalTable: "LessionDetails",
                principalColumn: "LessionDetailID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
