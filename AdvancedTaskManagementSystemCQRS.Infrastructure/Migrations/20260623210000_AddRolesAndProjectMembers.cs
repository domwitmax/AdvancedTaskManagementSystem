using System;
using AdvancedTaskManagementSystemCQRS.Infrastructure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Migrations
{
    [DbContext(typeof(TaskManagerDbContext))]
    [Migration("20260623210000_AddRolesAndProjectMembers")]
    public partial class AddRolesAndProjectMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                columnTypes: new[] { "int", "nvarchar(50)" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "ProjectManager" },
                    { 3, "TeamMember" }
                });

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.Sql(
                """
                DECLARE @AdministratorLogin nvarchar(50);
                SELECT TOP (1) @AdministratorLogin = [Login]
                FROM [Account]
                ORDER BY [CreatedAt], [Login];

                UPDATE [Account]
                SET [RoleId] = CASE WHEN [Login] = @AdministratorLogin THEN 1 ELSE 3 END;
                """);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectMember",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AccountLogin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMember", x => new { x.ProjectId, x.AccountLogin });
                    table.ForeignKey(
                        name: "FK_ProjectMember_Account_AccountLogin",
                        column: x => x.AccountLogin,
                        principalTable: "Account",
                        principalColumn: "Login",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMember_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_RoleId",
                table: "Account",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMember_AccountLogin",
                table: "ProjectMember",
                column: "AccountLogin");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Account_Role_RoleId",
                table: "Account",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.Sql(
                """
                INSERT INTO [ProjectMember] ([ProjectId], [AccountLogin], [AddedAt], [AddedBy])
                SELECT source.[ProjectId], source.[AccountLogin], SYSUTCDATETIME(), source.[AccountLogin]
                FROM
                (
                    SELECT [ProjectId], [CreatedBy] AS [AccountLogin]
                    FROM [Task]
                    WHERE [CreatedBy] IS NOT NULL
                    UNION
                    SELECT taskItem.[ProjectId], taskItem.[AssignedTo] AS [AccountLogin]
                    FROM [Task] taskItem
                    INNER JOIN [Account] accountItem ON accountItem.[Login] = taskItem.[AssignedTo]
                ) source
                INNER JOIN [Account] accountItem ON accountItem.[Login] = source.[AccountLogin]
                WHERE NOT EXISTS
                (
                    SELECT 1
                    FROM [ProjectMember] memberItem
                    WHERE memberItem.[ProjectId] = source.[ProjectId]
                      AND memberItem.[AccountLogin] = source.[AccountLogin]
                );

                DECLARE @DefaultAdministrator nvarchar(50);
                SELECT TOP (1) @DefaultAdministrator = [Login]
                FROM [Account]
                ORDER BY CASE WHEN [RoleId] = 1 THEN 0 ELSE 1 END, [CreatedAt], [Login];

                IF @DefaultAdministrator IS NOT NULL
                BEGIN
                    INSERT INTO [ProjectMember] ([ProjectId], [AccountLogin], [AddedAt], [AddedBy])
                    SELECT projectItem.[Id], @DefaultAdministrator, SYSUTCDATETIME(), @DefaultAdministrator
                    FROM [Project] projectItem
                    WHERE NOT EXISTS
                    (
                        SELECT 1
                        FROM [ProjectMember] memberItem
                        WHERE memberItem.[ProjectId] = projectItem.[Id]
                    );
                END
                """);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ProjectMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Account_Role_RoleId",
                table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_RoleId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Account");

            migrationBuilder.DropTable(name: "Role");
        }
    }
}
