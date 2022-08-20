using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addchatroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_ChatRoom",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    ChatRoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_ChatRoom", x => new { x.ChatRoomId, x.UserId });
                    table.ForeignKey(
                        name: "FKUser_ChatRoom_ChatRoom",
                        column: x => x.ChatRoomId,
                        principalTable: "ChatRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FKUser_ChatRoom_User",
                        column: x => x.UserId,
                        principalTable: "Sys_User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_ChatRoom");

            migrationBuilder.DropTable(
                name: "ChatRooms");
        }
    }
}
