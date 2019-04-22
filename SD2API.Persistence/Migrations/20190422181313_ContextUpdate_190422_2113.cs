using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SD2API.Persistence.Migrations
{
    public partial class ContextUpdate_190422_2113 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BinaryUrl",
                table: "Replays",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Replays",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ReplayHashStub",
                table: "Replays",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReplayRawFooter",
                table: "Replays",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReplayRawHeader",
                table: "Replays",
                maxLength: 4096,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReplayFooter",
                columns: table => new
                {
                    ReplayFooterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReplayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayFooter", x => x.ReplayFooterId);
                    table.ForeignKey(
                        name: "FK_ReplayFooter_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "ReplayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplayHeader",
                columns: table => new
                {
                    ReplayHeaderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReplayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayHeader", x => x.ReplayHeaderId);
                    table.ForeignKey(
                        name: "FK_ReplayHeader_Replays_ReplayId",
                        column: x => x.ReplayId,
                        principalTable: "Replays",
                        principalColumn: "ReplayId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplayFooterResult",
                columns: table => new
                {
                    ReplayFooterResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReplayFooterId = table.Column<int>(nullable: false),
                    Duration = table.Column<string>(maxLength: 10, nullable: true),
                    Victory = table.Column<string>(maxLength: 10, nullable: true),
                    Score = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayFooterResult", x => x.ReplayFooterResultId);
                    table.ForeignKey(
                        name: "FK_ReplayFooterResult_ReplayFooter_ReplayFooterId",
                        column: x => x.ReplayFooterId,
                        principalTable: "ReplayFooter",
                        principalColumn: "ReplayFooterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplayHeaderGame",
                columns: table => new
                {
                    ReplayHeaderGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReplayHeaderId = table.Column<int>(nullable: false),
                    Version = table.Column<string>(nullable: true),
                    ModList = table.Column<string>(nullable: true),
                    GameMode = table.Column<string>(nullable: true),
                    Map = table.Column<string>(nullable: true),
                    NbMaxPlayer = table.Column<string>(nullable: true),
                    NbIA = table.Column<string>(nullable: true),
                    GameType = table.Column<string>(nullable: true),
                    Private = table.Column<string>(nullable: true),
                    InitMoney = table.Column<string>(nullable: true),
                    TimeLimit = table.Column<string>(nullable: true),
                    ScoreLimit = table.Column<string>(nullable: true),
                    ServerName = table.Column<string>(nullable: true),
                    VictoryCond = table.Column<string>(nullable: true),
                    IsNetworkMode = table.Column<string>(nullable: true),
                    IncomeRate = table.Column<string>(nullable: true),
                    AllowObservers = table.Column<string>(nullable: true),
                    MapSelection = table.Column<string>(nullable: true),
                    Seed = table.Column<string>(nullable: true),
                    UniqueSessionId = table.Column<string>(nullable: true),
                    InverseSpawnPoints = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayHeaderGame", x => x.ReplayHeaderGameId);
                    table.ForeignKey(
                        name: "FK_ReplayHeaderGame_ReplayHeader_ReplayHeaderId",
                        column: x => x.ReplayHeaderId,
                        principalTable: "ReplayHeader",
                        principalColumn: "ReplayHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplayHeaderPlayer",
                columns: table => new
                {
                    ReplayHeaderPlayerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReplayHeaderId = table.Column<int>(nullable: false),
                    PlayerUserId = table.Column<string>(nullable: true),
                    PlayerElo = table.Column<string>(nullable: true),
                    PlayerRank = table.Column<string>(nullable: true),
                    PlayerLevel = table.Column<string>(nullable: true),
                    PlayerName = table.Column<string>(nullable: true),
                    PlayerTeamName = table.Column<string>(nullable: true),
                    PlayerAvatar = table.Column<string>(nullable: true),
                    PlayerIALevel = table.Column<string>(nullable: true),
                    PlayerReady = table.Column<string>(nullable: true),
                    PlayerDeckContent = table.Column<string>(nullable: true),
                    PlayerModList = table.Column<string>(nullable: true),
                    PlayerAlliance = table.Column<string>(nullable: true),
                    PlayerIsEnteredInLobby = table.Column<string>(nullable: true),
                    PlayerSkinIndexUsed = table.Column<string>(nullable: true),
                    PlayerScoreLimit = table.Column<string>(nullable: true),
                    PlayerIncomeRate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplayHeaderPlayer", x => x.ReplayHeaderPlayerId);
                    table.ForeignKey(
                        name: "FK_ReplayHeaderPlayer_ReplayHeader_ReplayHeaderId",
                        column: x => x.ReplayHeaderId,
                        principalTable: "ReplayHeader",
                        principalColumn: "ReplayHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplayFooter_ReplayId",
                table: "ReplayFooter",
                column: "ReplayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplayFooterResult_ReplayFooterId",
                table: "ReplayFooterResult",
                column: "ReplayFooterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplayHeader_ReplayId",
                table: "ReplayHeader",
                column: "ReplayId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplayHeaderGame_ReplayHeaderId",
                table: "ReplayHeaderGame",
                column: "ReplayHeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplayHeaderPlayer_ReplayHeaderId",
                table: "ReplayHeaderPlayer",
                column: "ReplayHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplayFooterResult");

            migrationBuilder.DropTable(
                name: "ReplayHeaderGame");

            migrationBuilder.DropTable(
                name: "ReplayHeaderPlayer");

            migrationBuilder.DropTable(
                name: "ReplayFooter");

            migrationBuilder.DropTable(
                name: "ReplayHeader");

            migrationBuilder.DropColumn(
                name: "BinaryUrl",
                table: "Replays");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Replays");

            migrationBuilder.DropColumn(
                name: "ReplayHashStub",
                table: "Replays");

            migrationBuilder.DropColumn(
                name: "ReplayRawFooter",
                table: "Replays");

            migrationBuilder.DropColumn(
                name: "ReplayRawHeader",
                table: "Replays");
        }
    }
}
