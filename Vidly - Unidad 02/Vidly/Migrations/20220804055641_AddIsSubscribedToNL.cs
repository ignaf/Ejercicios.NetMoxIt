using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Migrations
{
    public partial class AddIsSubscribedToNL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribedToNewsletter",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscribedToNewsletter",
                table: "Customers");
        }
    }
}
