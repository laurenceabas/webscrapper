using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddIsTagNameInInnerLinks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTagName",
                table: "InnerLinks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTagName",
                table: "InnerLinks");
        }
    }
}
