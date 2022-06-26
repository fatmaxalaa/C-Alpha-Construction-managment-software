using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class res05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "Resources",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "[CostPerDay] * [Duration]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "Resources",
                type: "float",
                nullable: false,
                computedColumnSql: "[CostPerDay] * [Duration]",
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
