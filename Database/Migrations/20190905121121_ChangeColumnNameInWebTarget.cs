using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class ChangeColumnNameInWebTarget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "WebTargets",
                newName: "ConstantName");
        }

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "ConstantName",
				table: "WebTargets",
				newName: "EventName");
		}
    }
}
