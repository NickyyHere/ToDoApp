using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeyToTodoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TodoItems_ProjectId",
                table: "TodoItems");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ProjectId_Name",
                table: "TodoItems",
                columns: new[] { "ProjectId", "Name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TodoItems_ProjectId_Name",
                table: "TodoItems");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ProjectId",
                table: "TodoItems",
                column: "ProjectId");
        }
    }
}
