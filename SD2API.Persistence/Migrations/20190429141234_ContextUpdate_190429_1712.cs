using Microsoft.EntityFrameworkCore.Migrations;

namespace SD2API.Persistence.Migrations
{
    public partial class ContextUpdate_190429_1712 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PlayerUserId",
                table: "ReplayHeaderPlayer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replays_ReplayHashStub",
                table: "Replays",
                column: "ReplayHashStub");

            migrationBuilder.CreateIndex(
                name: "IX_ReplayHeaderPlayer_PlayerUserId",
                table: "ReplayHeaderPlayer",
                column: "PlayerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Replays_ReplayHashStub",
                table: "Replays");

            migrationBuilder.DropIndex(
                name: "IX_ReplayHeaderPlayer_PlayerUserId",
                table: "ReplayHeaderPlayer");

            migrationBuilder.AlterColumn<string>(
                name: "PlayerUserId",
                table: "ReplayHeaderPlayer",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
