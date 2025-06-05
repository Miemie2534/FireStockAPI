using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FireStockAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "repairClaims");

            migrationBuilder.CreateTable(
                name: "claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FireExtinguisherId = table.Column<int>(type: "int", nullable: false),
                    Claims = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_claims_fireExtinguishers_FireExtinguisherId",
                        column: x => x.FireExtinguisherId,
                        principalTable: "fireExtinguishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_claims_FireExtinguisherId",
                table: "claims",
                column: "FireExtinguisherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "claims");

            migrationBuilder.CreateTable(
                name: "repairClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FireExtinguisherId = table.Column<int>(type: "int", nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Claim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_repairClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_repairClaims_fireExtinguishers_FireExtinguisherId",
                        column: x => x.FireExtinguisherId,
                        principalTable: "fireExtinguishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_repairClaims_FireExtinguisherId",
                table: "repairClaims",
                column: "FireExtinguisherId");
        }
    }
}
