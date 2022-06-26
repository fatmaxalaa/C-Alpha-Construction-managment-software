using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class res : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityName",
                table: "Resources",
                newName: "TaskName");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_ResourceId",
                table: "Tasks",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Resources_ResourceId",
                table: "Tasks",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "ResourceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Resources_ResourceId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ResourceId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TaskName",
                table: "Resources",
                newName: "ActivityName");
        }
    }
}
