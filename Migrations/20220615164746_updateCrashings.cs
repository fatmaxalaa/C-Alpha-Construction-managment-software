using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class updateCrashings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Segma",
                table: "Crashings",
                type: "float",
                nullable: false,
                computedColumnSql: "([PessimesticDuration]-[OptimisticDuration])/6",
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "ExpectedTime",
                table: "Crashings",
                type: "float",
                nullable: false,
                computedColumnSql: "([OptimisticDuration]+4*[MostLikelyDuration]+[PessimesticDuration])/6",
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Segma",
                table: "Crashings",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "([PessimesticDuration]-[OptimisticDuration])/6");

            migrationBuilder.AlterColumn<double>(
                name: "ExpectedTime",
                table: "Crashings",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldComputedColumnSql: "([OptimisticDuration]+4*[MostLikelyDuration]+[PessimesticDuration])/6");
        }
    }
}
