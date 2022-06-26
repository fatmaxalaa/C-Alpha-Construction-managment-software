using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class editEndDate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectEndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 6, 22, 20, 14, 15, 39, DateTimeKind.Local).AddTicks(7803));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectEndDate",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 6, 22, 20, 14, 15, 39, DateTimeKind.Local).AddTicks(7803),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
