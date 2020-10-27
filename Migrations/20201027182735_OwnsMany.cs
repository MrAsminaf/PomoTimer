using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PomoTimer.Migrations
{
    public partial class OwnsMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes_Capacity",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "TimeModel",
                columns: table => new
                {
                    ApplicationUserId = table.Column<string>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    minutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeModel", x => new { x.ApplicationUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_TimeModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeModel");

            migrationBuilder.AddColumn<int>(
                name: "Minutes_Capacity",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
        }
    }
}
