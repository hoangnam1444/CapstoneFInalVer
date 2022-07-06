using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class edittableansweraddtablepgroupanswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKTest_Answe19835",
                table: "Test_Answers");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "Test_Answers");

            migrationBuilder.RenameColumn(
                name: "PersonalityGroupID",
                table: "Test_Answers",
                newName: "PersonalityGroupId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Test_Personality_Groups",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnswersPerGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PGroupId = table.Column<int>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false),
                    Point = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersPerGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FKAnswer_PGroup",
                        column: x => x.AnswerId,
                        principalTable: "Test_Answers",
                        principalColumn: "AnswerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FKPGroup_Answer",
                        column: x => x.PGroupId,
                        principalTable: "Test_Personality_Groups",
                        principalColumn: "PersonalityGroupID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "AnswersPerGroups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Test_Personality_Groups");

            migrationBuilder.RenameColumn(
                name: "PersonalityGroupId",
                table: "Test_Answers",
                newName: "PersonalityGroupID");

            migrationBuilder.AlterColumn<int>(
                name: "PersonalityGroupID",
                table: "Test_Answers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Test_Answers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FKTest_Answe19835",
                table: "Test_Answers",
                column: "PersonalityGroupID",
                principalTable: "Test_Personality_Groups",
                principalColumn: "PersonalityGroupID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
