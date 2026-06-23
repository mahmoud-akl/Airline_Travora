using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineSimulation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAirportCongestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirportCongestions",
                columns: table => new
                {
                    AirportCongestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirportIataCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    HourOfDay = table.Column<int>(type: "int", nullable: false),
                    AverageCongestion = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportCongestions", x => x.AirportCongestionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirportCongestions_AirportIataCode_DayOfWeek_HourOfDay",
                table: "AirportCongestions",
                columns: new[] { "AirportIataCode", "DayOfWeek", "HourOfDay" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportCongestions");
        }
    }
}
