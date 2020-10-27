using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PomoTimer.Migrations
{
    public partial class FixedlackofHasNoKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeModel");

            migrationBuilder.AddColumn<int>(
                name: "Minutes_Capacity",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Minutes_Capacity",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "TimeModel",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "text", nullable: true),
                    minutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeModel", x => x.DateTime);
                    table.ForeignKey(
                        name: "FK_TimeModel_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeModel_ApplicationUserId",
                table: "TimeModel",
                column: "ApplicationUserId");
        }
    }
}
