using Microsoft.EntityFrameworkCore.Migrations;

namespace SD2API.Persistence.Migrations
{
    public partial class ContextUpdate_190424_1754 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Replays",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Replays");
        }
    }
}
