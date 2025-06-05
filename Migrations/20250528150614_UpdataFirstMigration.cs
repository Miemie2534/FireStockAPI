using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireStockAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdataFirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IssueDescription",
                table: "repairClaims",
                newName: "Claim");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Claim",
                table: "repairClaims",
                newName: "IssueDescription");
        }
    }
}
