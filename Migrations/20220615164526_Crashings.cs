using Microsoft.EntityFrameworkCore.Migrations;

namespace Resources.Migrations
{
    public partial class Crashings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Criticality",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Crashings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptimisticDuration = table.Column<double>(type: "float", nullable: false),
                    MostLikelyDuration = table.Column<double>(type: "float", nullable: false),
                    PessimesticDuration = table.Column<double>(type: "float", nullable: false),
                    ExpectedTime = table.Column<double>(type: "float", nullable: false),
                    Te = table.Column<double>(type: "float", nullable: false),
                    Segma = table.Column<double>(type: "float", nullable: false),
                    TotalSegma = table.Column<double>(type: "float", nullable: false),
                    RequiredTime = table.Column<double>(type: "float", nullable: false),
                    Probability = table.Column<double>(type: "float", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crashings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crashings");

            migrationBuilder.DropColumn(
                name: "Criticality",
                table: "Tasks");
        }
    }
}
