using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_table_account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "LastLogin",
                table: "Users",
                newName: "LastLoginAt");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Users",
                newName: "LastGetUserAt");

            migrationBuilder.RenameColumn(
                name: "ChangePassword",
                table: "Users",
                newName: "ChangePasswordAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "LastLoginAt",
                table: "Users",
                newName: "LastLogin");

            migrationBuilder.RenameColumn(
                name: "LastGetUserAt",
                table: "Users",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "ChangePasswordAt",
                table: "Users",
                newName: "ChangePassword");
        }
    }
}
