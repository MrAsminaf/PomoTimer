using Microsoft.EntityFrameworkCore.Migrations;

namespace PomoTimer.Migrations
{
    public partial class AddedTaskName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "TimeModels",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "TimeModels");
        }
    }
}
