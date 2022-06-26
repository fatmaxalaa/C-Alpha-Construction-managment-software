using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class updateType2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "type1",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type1",
                table: "Tasks");
        }
    }
}
