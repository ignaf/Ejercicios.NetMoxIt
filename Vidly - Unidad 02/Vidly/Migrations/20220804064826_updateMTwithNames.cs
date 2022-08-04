using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class updateMTwithNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipType SET Name='Pay as you go' WHERE Id=1");
            migrationBuilder.Sql("UPDATE MembershipType SET Name='Monthly' WHERE Id=2");
            migrationBuilder.Sql("UPDATE MembershipType SET Name='Quarterly' WHERE Id=3");
            migrationBuilder.Sql("UPDATE MembershipType SET Name='Annual' WHERE Id=4");

        }       
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
