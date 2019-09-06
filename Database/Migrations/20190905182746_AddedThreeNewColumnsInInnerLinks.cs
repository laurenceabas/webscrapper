using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddedThreeNewColumnsInInnerLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "XPath",
                table: "InnerLinks",
                newName: "Locator");

            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                table: "InnerLinks",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCssClass",
                table: "InnerLinks",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsXPath",
                table: "InnerLinks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttributeName",
                table: "InnerLinks");

            migrationBuilder.DropColumn(
                name: "IsCssClass",
                table: "InnerLinks");

            migrationBuilder.DropColumn(
                name: "IsXPath",
                table: "InnerLinks");

            migrationBuilder.RenameColumn(
                name: "Locator",
                table: "InnerLinks",
                newName: "XPath");
        }
    }
}
