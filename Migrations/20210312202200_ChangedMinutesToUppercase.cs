using Microsoft.EntityFrameworkCore.Migrations;

namespace PomoTimer.Migrations
{
    public partial class ChangedMinutesToUppercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "minutes",
                table: "TimeModels",
                newName: "Minutes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Minutes",
                table: "TimeModels",
                newName: "minutes");
        }
    }
}
