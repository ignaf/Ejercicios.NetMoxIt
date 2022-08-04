using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class populateGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genre(Id, Name) VALUES (1,'Comedy')");
            migrationBuilder.Sql("INSERT INTO Genre(Id, Name) VALUES (2,'Action')");
            migrationBuilder.Sql("INSERT INTO Genre(Id, Name) VALUES (3,'Family')");
            migrationBuilder.Sql("INSERT INTO Genre(Id, Name) VALUES (4,'Romance')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
