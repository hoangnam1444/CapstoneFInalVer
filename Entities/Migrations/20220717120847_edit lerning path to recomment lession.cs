using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editlerningpathtorecommentlession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningPathDetails");

            migrationBuilder.DropTable(
                name: "User_LearningPath");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Learning__20DCAEA1FA5B114D",
                table: "Learning_Paths");

            migrationBuilder.DropColumn(
                name: "GPA10",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "GPA11",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "GPA12",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "LearningPathID",
                table: "Learning_Paths");

            migrationBuilder.AddColumn<int>(
                name: "LessionID",
                table: "Learning_Paths",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Learning__20DCAEA1FA5B114D",
                table: "Learning_Paths",
                column: "LessionID");

            migrationBuilder.CreateTable(
                name: "LessionDetails",
                columns: table => new
                {
                    LessionDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessionID = table.Column<int>(nullable: false),
                    LessionDetailContent = table.Column<string>(maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__40D9D999F16AA74B", x => x.LessionDetailID);
                    table.ForeignKey(
                        name: "FKLearningPa175463",
                        column: x => x.LessionID,
                        principalTable: "Learning_Paths",
                        principalColumn: "LessionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Lession",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    LessionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Lea__15850646891BC882", x => new { x.UserID, x.LessionID });
                    table.ForeignKey(
                        name: "FKUser_Learn182352",
                        column: x => x.LessionID,
                        principalTable: "Learning_Paths",
                        principalColumn: "LessionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKUser_Learn707353",
                        column: x => x.UserID,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessionDetails");

            migrationBuilder.DropTable(
                name: "User_Lession");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Learning__20DCAEA1FA5B114D",
                table: "Learning_Paths");

            migrationBuilder.DropColumn(
                name: "LessionID",
                table: "Learning_Paths");

            migrationBuilder.AddColumn<float>(
                name: "GPA10",
                table: "Sys_User",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "GPA11",
                table: "Sys_User",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "GPA12",
                table: "Sys_User",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LearningPathID",
                table: "Learning_Paths",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK__Learning__20DCAEA1FA5B114D",
                table: "Learning_Paths",
                column: "LearningPathID");

            migrationBuilder.CreateTable(
                name: "LearningPathDetails",
                columns: table => new
                {
                    LearningPathDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "('0')"),
                    LearningPathDetailContent = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LearningPathID = table.Column<int>(type: "int", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__40D9D999F16AA74B", x => x.LearningPathDetailID);
                    table.ForeignKey(
                        name: "FKLearningPa175463",
                        column: x => x.LearningPathID,
                        principalTable: "Learning_Paths",
                        principalColumn: "LearningPathID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_LearningPath",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    LearningPathID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Lea__15850646891BC882", x => new { x.UserID, x.LearningPathID });
                    table.ForeignKey(
                        name: "FKUser_Learn182352",
                        column: x => x.LearningPathID,
                        principalTable: "Learning_Paths",
                        principalColumn: "LearningPathID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKUser_Learn707353",
                        column: x => x.UserID,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
        }
    }
}
