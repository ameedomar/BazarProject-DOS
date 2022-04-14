using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogServer.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BookName = table.Column<string>(type: "TEXT", nullable: false),
                    BookTopic = table.Column<string>(type: "TEXT", nullable: false),
                    BookCost = table.Column<double>(type: "REAL", nullable: false),
                    CountInStock = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Catalogs");
        }
    }
}
