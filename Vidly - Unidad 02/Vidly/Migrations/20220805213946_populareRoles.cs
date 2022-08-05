using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class populareRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roles(Name) VALUES ('CanManageMovies')");
            migrationBuilder.Sql("INSERT INTO Roles(Name) VALUES ('ReadOnlyUser')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
