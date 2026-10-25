using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Status",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "ChangesHistory",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastGetUserAt",
                table: "Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsFinished",
                value: true);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsFinished",
                value: false);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsFinished",
                value: true);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 10,
                column: "IsFinished",
                value: false);

            migrationBuilder.CreateIndex(
                name: "IX_ChangesHistory_ChangedBy",
                table: "ChangesHistory",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ChangesHistory_TaskId",
                table: "ChangesHistory",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangesHistory_Account_ChangedBy",
                table: "ChangesHistory",
                column: "ChangedBy",
                principalTable: "Account",
                principalColumn: "Login",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ChangesHistory_Task_TaskId",
                table: "ChangesHistory",
                column: "TaskId",
                principalTable: "Task",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangesHistory_Account_ChangedBy",
                table: "ChangesHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ChangesHistory_Task_TaskId",
                table: "ChangesHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChangesHistory_ChangedBy",
                table: "ChangesHistory");

            migrationBuilder.DropIndex(
                name: "IX_ChangesHistory_TaskId",
                table: "ChangesHistory");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "LastGetUserAt",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "ChangedBy",
                table: "ChangesHistory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
