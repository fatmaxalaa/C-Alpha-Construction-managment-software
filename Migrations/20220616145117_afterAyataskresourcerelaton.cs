using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class afterAyataskresourcerelaton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
