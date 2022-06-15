using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class initdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    CollegeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeName = table.Column<string>(maxLength: 50, nullable: false),
                    ToGPA = table.Column<float>(nullable: false),
                    FromGPA = table.Column<float>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')"),
                    CollegeTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Colleges__29409519E55EE24B", x => x.CollegeID);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorName = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Majors__D5B8BFB1FFCDC09F", x => x.MajorID);
                });

            migrationBuilder.CreateTable(
                name: "Sys_User_Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNane = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')"),
                    DeletedDate = table.Column<DateTime>(type: "date", nullable: true),
                    DeletedUser = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_POSITION", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Test_Types",
                columns: table => new
                {
                    TestTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeName = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Typ__9BB876465EDCF709", x => x.TestTypeID);
                });

            migrationBuilder.CreateTable(
                name: "College_Ref_Major",
                columns: table => new
                {
                    MajorID = table.Column<int>(nullable: false),
                    CollegeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__College___572CB6E089C2CACB", x => new { x.MajorID, x.CollegeID });
                    table.ForeignKey(
                        name: "FKCollege_Re287083",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKCollege_Re494710",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Learning_Paths",
                columns: table => new
                {
                    LearningPathID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MajorID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Learning__20DCAEA1FA5B114D", x => x.LearningPathID);
                    table.ForeignKey(
                        name: "FKLearning_P92098",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sys_User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 200, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Gender = table.Column<bool>(nullable: false, defaultValueSql: "('0')"),
                    PhoneNumber = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    BirthDay = table.Column<DateTime>(type: "date", nullable: true),
                    UserName = table.Column<string>(maxLength: 60, nullable: true),
                    Email = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(unicode: false, maxLength: 400, nullable: true),
                    IsLocked = table.Column<bool>(nullable: false, defaultValueSql: "('0')"),
                    Grade = table.Column<int>(nullable: true),
                    GPA10 = table.Column<float>(nullable: false),
                    GPA11 = table.Column<float>(nullable: false),
                    GPA12 = table.Column<float>(nullable: false),
                    CreatedUser = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedUser = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')"),
                    DeletedDate = table.Column<DateTime>(type: "date", nullable: true),
                    DeletedUser = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_USER", x => x.UserID);
                    table.ForeignKey(
                        name: "FKSys_User142060",
                        column: x => x.RoleID,
                        principalTable: "Sys_User_Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test_Declarations",
                columns: table => new
                {
                    TestID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestDescrip = table.Column<string>(maxLength: 50, nullable: true),
                    TestTypeID = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedUser = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValueSql: "('0')"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Dec__8CC33100AFED4550", x => x.TestID);
                    table.ForeignKey(
                        name: "FKTest_Decla953794",
                        column: x => x.TestTypeID,
                        principalTable: "Test_Types",
                        principalColumn: "TestTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test_Personality_Groups",
                columns: table => new
                {
                    PersonalityGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalityGroupName = table.Column<string>(maxLength: 50, nullable: false),
                    TestTypeID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Per__EFD5EFA226ABA06D", x => x.PersonalityGroupID);
                    table.ForeignKey(
                        name: "FKTest_Perso404711",
                        column: x => x.TestTypeID,
                        principalTable: "Test_Types",
                        principalColumn: "TestTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LearningPathDetails",
                columns: table => new
                {
                    LearningPathDetailID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LearningPathID = table.Column<int>(nullable: false),
                    LearningPathDetailContent = table.Column<string>(maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
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
                    UserID = table.Column<int>(nullable: false),
                    LearningPathID = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "VC_Guidance",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    CollegeID = table.Column<int>(nullable: false),
                    MajorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__VC_Guida__25C97D42CD3C4906", x => new { x.UserID, x.CollegeID, x.MajorID });
                    table.ForeignKey(
                        name: "FKVC_Guidanc491547",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKVC_Guidanc283920",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKVC_Guidanc169535",
                        column: x => x.UserID,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test_Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestID = table.Column<int>(nullable: false),
                    QuestionContent = table.Column<string>(maxLength: 500, nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Que__0DC06F8C223898C0", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FKTest_Quest411060",
                        column: x => x.TestID,
                        principalTable: "Test_Declarations",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Major_Ref_Personality",
                columns: table => new
                {
                    PersonalityGroupID = table.Column<int>(nullable: false),
                    MajorID = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Major_Re__E28E6459D09867C3", x => new { x.PersonalityGroupID, x.MajorID });
                    table.ForeignKey(
                        name: "FKMajor_Ref_224122",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKMajor_Ref_93769",
                        column: x => x.PersonalityGroupID,
                        principalTable: "Test_Personality_Groups",
                        principalColumn: "PersonalityGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test_Answers",
                columns: table => new
                {
                    AnswerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerContent = table.Column<string>(maxLength: 200, nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    PersonalityGroupID = table.Column<int>(nullable: false),
                    Point = table.Column<int>(nullable: false),
                    OrderIndex = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValueSql: "('0')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Ans__D4825024C8BB5FCE", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FKTest_Answe19835",
                        column: x => x.PersonalityGroupID,
                        principalTable: "Test_Personality_Groups",
                        principalColumn: "PersonalityGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKTest_Answe601746",
                        column: x => x.QuestionID,
                        principalTable: "Test_Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Test_Results",
                columns: table => new
                {
                    TestResultID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    TestID = table.Column<int>(nullable: false),
                    QuestionID = table.Column<int>(nullable: false),
                    AnswerID = table.Column<int>(nullable: false),
                    IsLast = table.Column<bool>(nullable: false, defaultValueSql: "('1')"),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test_Res__E2463A674228CD84", x => x.TestResultID);
                    table.ForeignKey(
                        name: "FKTest_Resul169939",
                        column: x => x.AnswerID,
                        principalTable: "Test_Answers",
                        principalColumn: "AnswerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKTest_Resul345735",
                        column: x => x.QuestionID,
                        principalTable: "Test_Questions",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKTest_Resul403288",
                        column: x => x.TestID,
                        principalTable: "Test_Declarations",
                        principalColumn: "TestID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKTest_Resul11694",
                        column: x => x.UserID,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_College_Ref_Major_CollegeID",
                table: "College_Ref_Major",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_Learning_Paths_MajorID",
                table: "Learning_Paths",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathDetails_LearningPathID",
                table: "LearningPathDetails",
                column: "LearningPathID");

            migrationBuilder.CreateIndex(
                name: "IX_Major_Ref_Personality_MajorID",
                table: "Major_Ref_Personality",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_Sys_User_RoleID",
                table: "Sys_User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Answers_PersonalityGroupID",
                table: "Test_Answers",
                column: "PersonalityGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Answers_QuestionID",
                table: "Test_Answers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Declarations_TestTypeID",
                table: "Test_Declarations",
                column: "TestTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Personality_Groups_TestTypeID",
                table: "Test_Personality_Groups",
                column: "TestTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Questions_TestID",
                table: "Test_Questions",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Results_AnswerID",
                table: "Test_Results",
                column: "AnswerID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Results_QuestionID",
                table: "Test_Results",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Results_TestID",
                table: "Test_Results",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Test_Results_UserID",
                table: "Test_Results",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_LearningPath_LearningPathID",
                table: "User_LearningPath",
                column: "LearningPathID");

            migrationBuilder.CreateIndex(
                name: "IX_VC_Guidance_CollegeID",
                table: "VC_Guidance",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_VC_Guidance_MajorID",
                table: "VC_Guidance",
                column: "MajorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "College_Ref_Major");

            migrationBuilder.DropTable(
                name: "LearningPathDetails");

            migrationBuilder.DropTable(
                name: "Major_Ref_Personality");

            migrationBuilder.DropTable(
                name: "Test_Results");

            migrationBuilder.DropTable(
                name: "User_LearningPath");

            migrationBuilder.DropTable(
                name: "VC_Guidance");

            migrationBuilder.DropTable(
                name: "Test_Answers");

            migrationBuilder.DropTable(
                name: "Learning_Paths");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Sys_User");

            migrationBuilder.DropTable(
                name: "Test_Personality_Groups");

            migrationBuilder.DropTable(
                name: "Test_Questions");

            migrationBuilder.DropTable(
                name: "Majors");

            migrationBuilder.DropTable(
                name: "Sys_User_Role");

            migrationBuilder.DropTable(
                name: "Test_Declarations");

            migrationBuilder.DropTable(
                name: "Test_Types");
        }
    }
}
