using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebTargets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EventName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebTargets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InnerLinks",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WebTargetId = table.Column<int>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    XPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InnerLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InnerLinks_WebTargets_WebTargetId",
                        column: x => x.WebTargetId,
                        principalTable: "WebTargets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventName = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    SeatNumber = table.Column<string>(nullable: true),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    InnerLinkId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_InnerLinks_InnerLinkId",
                        column: x => x.InnerLinkId,
                        principalTable: "InnerLinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InnerLinks_WebTargetId",
                table: "InnerLinks",
                column: "WebTargetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_InnerLinkId",
                table: "Tickets",
                column: "InnerLinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "InnerLinks");

            migrationBuilder.DropTable(
                name: "WebTargets");
        }
    }
}
