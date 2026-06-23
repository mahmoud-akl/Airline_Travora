using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirlineSimulation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationToBaggageTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentLocation",
                table: "BaggageTags",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLocationUpdatedAt",
                table: "BaggageTags",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentLocation",
                table: "BaggageTags");

            migrationBuilder.DropColumn(
                name: "LastLocationUpdatedAt",
                table: "BaggageTags");
        }
    }
}
