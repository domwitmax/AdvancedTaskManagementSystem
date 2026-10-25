using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Migrations
{
    public partial class AddLabelsAttachmentsAndCancelledStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskAttachment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UploadedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAttachment_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskLabel",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    LabelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLabel", x => new { x.TaskId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_TaskLabel_Label_LabelId",
                        column: x => x.LabelId,
                        principalTable: "Label",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskLabel_Task_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Task",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Label",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Błąd wymagający poprawy", "bug" },
                    { 2, "Nowa funkcjonalność", "feature" },
                    { 3, "Zadanie dotyczące dokumentacji", "dokumentacja" },
                    { 4, "Zadanie o wysokiej pilności", "pilne" },
                    { 5, "Zadanie opcjonalne", "opcjonalne" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "IsFinished", "Name", "TypeId" },
                values: new object[] { 11, true, "Anulowany", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Label_Name",
                table: "Label",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachment_TaskId",
                table: "TaskAttachment",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskLabel_LabelId",
                table: "TaskLabel",
                column: "LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskAttachment");

            migrationBuilder.DropTable(
                name: "TaskLabel");

            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
