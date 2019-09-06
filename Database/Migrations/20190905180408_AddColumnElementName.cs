using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddColumnElementName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElementName",
                table: "InnerLinks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElementName",
                table: "InnerLinks");
        }
    }
}
