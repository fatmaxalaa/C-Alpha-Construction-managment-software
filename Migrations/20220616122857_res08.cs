using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class res08 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "Resources",
                type: "float",
                nullable: false,
                computedColumnSql: "[CostPerDay] *[UnitsPerDay]* [Duration]",
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalCost",
                table: "Resources",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "[CostPerDay] *[UnitsPerDay]* [Duration]");
        }
    }
}
