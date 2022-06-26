using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class updateProjectt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectEndDate",
                table: "projects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComputedColumnSql: "DATEADD(day,[ProjectDuration], [ProjectStartDate])");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProjectEndDate",
                table: "projects",
                type: "datetime2",
                nullable: false,
                computedColumnSql: "DATEADD(day,[ProjectDuration], [ProjectStartDate])",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
