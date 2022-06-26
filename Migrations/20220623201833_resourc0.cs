using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class resourc0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "projectsrelatedID",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_projectsrelatedID",
                table: "Tasks",
                column: "projectsrelatedID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Projects_projectsrelatedID",
                table: "Tasks",
                column: "projectsrelatedID",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Projects_projectsrelatedID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_projectsrelatedID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "projectsrelatedID",
                table: "Tasks");
        }
    }
}
