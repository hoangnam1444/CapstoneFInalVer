using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatelessionandlessiondetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKLearningPa175463",
                table: "LessionDetails");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "LessionDetails");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "LessionDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LessionDetailId",
                table: "Learning_Paths",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Learning_Paths_LessionDetails_LessionDetailId",
                table: "Learning_Paths",
                column: "LessionDetailId",
                principalTable: "LessionDetails",
                principalColumn: "LessionDetailID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Learning_Paths_LessionDetails_LessionDetailId",
                table: "Learning_Paths");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "LessionDetails");

            migrationBuilder.DropColumn(
                name: "LessionDetailId",
                table: "Learning_Paths");

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "LessionDetails",
                type: "int",
                nullable: false,
                defaultValueSql: "((1))");

            migrationBuilder.AddForeignKey(
                name: "FKLearningPa175463",
                table: "LessionDetails",
                column: "LessionID",
                principalTable: "Learning_Paths",
                principalColumn: "LessionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
