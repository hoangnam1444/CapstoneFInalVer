using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editchatroom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_ChatRoom");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ChatRooms");

            migrationBuilder.AddColumn<bool>(
                name: "IsConnector",
                table: "User_College",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CollegeId",
                table: "ChatRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConnectorId",
                table: "ChatRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "ChatRooms",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FKUser_Chatroom_Connector",
                table: "ChatRooms",
                column: "ConnectorId",
                principalTable: "Sys_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FKUser_Chatroom_Student",
                table: "ChatRooms",
                column: "StudentId",
                principalTable: "Sys_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FKUser_Chatroom_Connector",
                table: "ChatRooms");

            migrationBuilder.DropForeignKey(
                name: "FKUser_Chatroom_Student",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "IsConnector",
                table: "User_College");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "ConnectorId",
                table: "ChatRooms");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "ChatRooms");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ChatRooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User_ChatRoom",
                columns: table => new
                {
                    ChatRoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_User_ChatRoom_UserId",
                table: "User_ChatRoom",
                column: "UserId");
        }
    }
}
