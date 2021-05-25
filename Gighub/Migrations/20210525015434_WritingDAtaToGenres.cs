using Microsoft.EntityFrameworkCore.Migrations;

namespace Gighub.Migrations
{
    public partial class WritingDAtaToGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Genres (Name) values ('Country')");
            migrationBuilder.Sql("Insert into Genres (Name) values ('Blues')");
            migrationBuilder.Sql("Insert into Genres (Name) values ('Rock')");
            migrationBuilder.Sql("Insert into Genres (Name) values ('Jazz')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from  Genres where name in  ('Jazz','Rock','Blues','Country')");
        }
    }
}
