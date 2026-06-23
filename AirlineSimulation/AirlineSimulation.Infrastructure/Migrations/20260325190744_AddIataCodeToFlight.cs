using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineSimulation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIataCodeToFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArrivalIataCode",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureIataCode",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalIataCode",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureIataCode",
                table: "Flights");
        }
    }
}
