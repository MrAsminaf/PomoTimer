using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PomoTimer.Migrations
{
    public partial class AddetTimeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeSpan",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "TimeModel",
                columns: table => new
                {
                    DateTime = table.Column<DateTime>(nullable: false),
                    minutes = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeModel");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeSpan",
                table: "AspNetUsers",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
