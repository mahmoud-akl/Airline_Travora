using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineSimulation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaggageAllowanceToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AllowedBaggageCount",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxAllowedWeight",
                table: "Tickets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedBaggageCount",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "MaxAllowedWeight",
                table: "Tickets");
        }
    }
}
