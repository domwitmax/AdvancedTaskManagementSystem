using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addChangesHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangesHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChangesHistoryId = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChangeDetails_ChangesHistory_ChangesHistoryId",
                        column: x => x.ChangesHistoryId,
                        principalTable: "ChangesHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeDetails_ChangesHistoryId",
                table: "ChangeDetails",
                column: "ChangesHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeDetails");

            migrationBuilder.DropTable(
                name: "ChangesHistory");
        }
    }
}
