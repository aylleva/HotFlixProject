using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotFlix.Persistence.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createcommentstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisLikeCount",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Comments",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisLikeCount",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Comments");
        }
    }
}
