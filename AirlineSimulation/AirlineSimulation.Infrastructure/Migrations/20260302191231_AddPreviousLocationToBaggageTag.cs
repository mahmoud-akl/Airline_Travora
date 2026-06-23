using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineSimulation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPreviousLocationToBaggageTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreviousLocation",
                table: "BaggageTags",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousLocation",
                table: "BaggageTags");
        }
    }
}
