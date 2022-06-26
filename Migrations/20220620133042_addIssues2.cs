using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class addIssues2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueEndDate",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "IssueStartDate",
                table: "Issues",
                newName: "IssueDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssueDate",
                table: "Issues",
                newName: "IssueStartDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "IssueEndDate",
                table: "Issues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
