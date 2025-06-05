using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireStockAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "fireExtinguishers",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "fireExtinguishers",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "fireExtinguishers",
                newName: "size");

            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "fireExtinguishers",
                newName: "serialNumber");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "fireExtinguishers",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "fireExtinguishers",
                newName: "createdDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type",
                table: "fireExtinguishers",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "fireExtinguishers",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "size",
                table: "fireExtinguishers",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "serialNumber",
                table: "fireExtinguishers",
                newName: "SerialNumber");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "fireExtinguishers",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "createdDate",
                table: "fireExtinguishers",
                newName: "CreatedDate");
        }
    }
}
