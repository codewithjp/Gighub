using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gighub.Migrations
{
    public partial class AddingGigsAndGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtistId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gig_AspNetUsers_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gig_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gig_ArtistId",
                table: "Gig",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Gig_GenreId",
                table: "Gig",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gig");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
