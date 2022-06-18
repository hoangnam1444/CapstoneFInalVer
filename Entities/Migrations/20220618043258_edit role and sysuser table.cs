using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editroleandsysusertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Sys_User_Role");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "Sys_User_Role");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sys_User_Role");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "DeletedUser",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "Sys_User");

            migrationBuilder.AddColumn<int>(
                name: "AdminIdUpdate",
                table: "Sys_User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateAdminUserId",
                table: "Sys_User",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sys_User_Sys_User_UpdateAdminUserId",
                table: "Sys_User",
                column: "UpdateAdminUserId",
                principalTable: "Sys_User",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sys_User_Sys_User_UpdateAdminUserId",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "AdminIdUpdate",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "UpdateAdminUserId",
                table: "Sys_User");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Sys_User_Role",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "Sys_User_Role",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sys_User_Role",
                type: "bit",
                nullable: false,
                defaultValueSql: "('0')");

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Sys_User",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedUser",
                table: "Sys_User",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Sys_User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Sys_User",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUser",
                table: "Sys_User",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}
