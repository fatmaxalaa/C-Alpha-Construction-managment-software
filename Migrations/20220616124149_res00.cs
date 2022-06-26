using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class res00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ResourceTask",
                columns: table => new
                {
                    TasksId = table.Column<int>(type: "int", nullable: false),
                    resourcesResourceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTask", x => new { x.TasksId, x.resourcesResourceId });
                    table.ForeignKey(
                        name: "FK_ResourceTask_Resources_resourcesResourceId",
                        column: x => x.resourcesResourceId,
                        principalTable: "Resources",
                        principalColumn: "ResourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceTask_Tasks_TasksId",
                        column: x => x.TasksId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTask_resourcesResourceId",
                table: "ResourceTask",
                column: "resourcesResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceTask");

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
    }
}
