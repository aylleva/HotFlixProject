using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotFlix.Persistence.DAL.Migrations
{
    /// <inheritdoc />
    public partial class createmoviewatchestable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WatchedFilms",
                table: "AspNetUsers",
                type: "int",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "MovieWatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    WatchedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieWatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieWatches_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieWatches_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieWatches_MovieId",
                table: "MovieWatches",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieWatches_UserId",
                table: "MovieWatches",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieWatches");

            migrationBuilder.DropColumn(
                name: "WatchedFilms",
                table: "AspNetUsers");
        }
    }
}
