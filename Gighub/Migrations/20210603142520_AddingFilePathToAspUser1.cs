using Microsoft.EntityFrameworkCore.Migrations;

namespace Gighub.Migrations
{
    public partial class AddingFilePathToAspUser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AspNetUsers");
        }
    }
}
