using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class updateprojecttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectEndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 23, 10, 55, 45, 862, DateTimeKind.Local).AddTicks(9740),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectEndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 23, 10, 55, 45, 862, DateTimeKind.Local).AddTicks(9740));
        }
    }
}
