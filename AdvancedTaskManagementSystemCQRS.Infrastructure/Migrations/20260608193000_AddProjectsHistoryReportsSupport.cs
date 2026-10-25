using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Migrations
{
    public partial class AddProjectsHistoryReportsSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_ProjectStatus", x => x.Id));

            migrationBuilder.InsertData(
                table: "ProjectStatus",
                columns: new[] { "Id", "Name" },
                columnTypes: new[] { "int", "nvarchar(100)" },
                values: new object[,]
                {
                    { 1, "Nowy" },
                    { 2, "Aktywny" },
                    { 3, "Zakończony" },
                    { 4, "Archiwalny" }
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey("FK_Project_ProjectStatus_StatusId", x => x.StatusId, "ProjectStatus", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Project",
                columns: new[] { "Id", "Name", "Description", "StatusId", "StartDate", "EndDate" },
                columnTypes: new[] { "int", "nvarchar(100)", "nvarchar(500)", "int", "datetime2", "datetime2" },
                values: new object[,]
                {
                    { 1, "Projekt domyślny", "Projekt utworzony automatycznie dla istniejących zadań.", 2, DateTime.UtcNow, null }
                });

            migrationBuilder.CreateTable(
                name: "ChangesHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_ChangesHistory", x => x.Id));

            migrationBuilder.CreateTable(
                name: "ChangeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    ChangesHistoryId = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OldValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NewValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeDetails", x => x.Id);
                    table.ForeignKey("FK_ChangeDetails_ChangesHistory_ChangesHistoryId", x => x.ChangesHistoryId, "ChangesHistory", "Id", onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<int>(name: "ProjectId", table: "Task", type: "int", nullable: false, defaultValue: 1);
            migrationBuilder.CreateIndex(name: "IX_Project_StatusId", table: "Project", column: "StatusId");
            migrationBuilder.CreateIndex(name: "IX_Task_ProjectId", table: "Task", column: "ProjectId");
            migrationBuilder.CreateIndex(name: "IX_ChangeDetails_ChangesHistoryId", table: "ChangeDetails", column: "ChangesHistoryId");
            migrationBuilder.AddForeignKey("FK_Task_Project_ProjectId", "Task", "ProjectId", "Project", principalColumn: "Id", onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("FK_Task_Project_ProjectId", "Task");
            migrationBuilder.DropTable("ChangeDetails");
            migrationBuilder.DropTable("ChangesHistory");
            migrationBuilder.DropTable("Project");
            migrationBuilder.DropTable("ProjectStatus");
            migrationBuilder.DropColumn("ProjectId", "Task");
        }
    }
}
