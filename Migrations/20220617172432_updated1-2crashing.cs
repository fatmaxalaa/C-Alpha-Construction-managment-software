using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class updated12crashing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrashingId",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CrashingId",
                table: "Tasks",
                column: "CrashingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Crashings_CrashingId",
                table: "Tasks",
                column: "CrashingId",
                principalTable: "Crashings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Crashings_CrashingId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CrashingId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CrashingId",
                table: "Tasks");
        }
    }
}
