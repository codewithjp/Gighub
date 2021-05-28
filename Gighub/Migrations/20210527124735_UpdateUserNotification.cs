using Microsoft.EntityFrameworkCore.Migrations;

namespace Gighub.Migrations
{
    public partial class UpdateUserNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotofications_AspNetUsers_AppUserId",
                table: "UserNotofications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotofications_AppUserId",
                table: "UserNotofications");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "UserNotofications");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotofications_UserId",
                table: "UserNotofications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotofications_AspNetUsers_UserId",
                table: "UserNotofications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserNotofications_AspNetUsers_UserId",
                table: "UserNotofications");

            migrationBuilder.DropIndex(
                name: "IX_UserNotofications_UserId",
                table: "UserNotofications");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "UserNotofications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserNotofications_AppUserId",
                table: "UserNotofications",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserNotofications_AspNetUsers_AppUserId",
                table: "UserNotofications",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
