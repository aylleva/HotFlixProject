using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotFlix.Persistence.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createjobcontactsandpartnershipstables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerShips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    PartnerShipId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobContacts_PartnerShips_PartnerShipId",
                        column: x => x.PartnerShipId,
                        principalTable: "PartnerShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobContacts_PartnerShipId",
                table: "JobContacts",
                column: "PartnerShipId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerShips_Name",
                table: "PartnerShips",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobContacts");

            migrationBuilder.DropTable(
                name: "PartnerShips");

        }
    }
}
