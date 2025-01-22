using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do_list_api.Migrations
{
    /// <inheritdoc />
    public partial class add_updated_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tasks_TasksId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "TasksId",
                table: "Tags",
                newName: "TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_TasksId",
                table: "Tags",
                newName: "IX_Tags_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tasks_TaskId",
                table: "Tags",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tasks_TaskId",
                table: "Tags");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "Tags",
                newName: "TasksId");

            migrationBuilder.RenameIndex(
                name: "IX_Tags_TaskId",
                table: "Tags",
                newName: "IX_Tags_TasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tasks_TasksId",
                table: "Tags",
                column: "TasksId",
                principalTable: "Tasks",
                principalColumn: "Id");
        }
    }
}
