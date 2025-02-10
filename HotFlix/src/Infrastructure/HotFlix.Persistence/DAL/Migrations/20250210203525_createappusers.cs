using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotFlix.Persistence.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createappusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CodeExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "AspNetUsers");
        }
    }
}
