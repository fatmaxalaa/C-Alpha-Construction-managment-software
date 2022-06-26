using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class updated12Task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskId",
                table: "Resources",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_TaskId",
                table: "Resources",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Tasks_TaskId",
                table: "Resources",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Tasks_TaskId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_TaskId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "TaskId",
                table: "Resources");
        }
    }
}
