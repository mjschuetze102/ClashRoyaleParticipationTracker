using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleDataModel.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClanMembers",
                columns: table => new
                {
                    Tag = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanMembers", x => x.Tag);
                });

            migrationBuilder.CreateTable(
                name: "WarHistory",
                columns: table => new
                {
                    CreatedDate = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarHistory", x => x.CreatedDate);
                });

            migrationBuilder.CreateTable(
                name: "DonationRecords",
                columns: table => new
                {
                    StoredDate = table.Column<DateTime>(nullable: false),
                    PlayerTag = table.Column<string>(nullable: false),
                    Donations = table.Column<int>(nullable: false),
                    DonationsReceived = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationRecords", x => new { x.StoredDate, x.PlayerTag });
                    table.ForeignKey(
                        name: "FK_DonationRecords_ClanMembers_PlayerTag",
                        column: x => x.PlayerTag,
                        principalTable: "ClanMembers",
                        principalColumn: "Tag",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarResults",
                columns: table => new
                {
                    PlayerTag = table.Column<string>(nullable: false),
                    WarLogDate = table.Column<string>(nullable: false),
                    CardsEarned = table.Column<int>(nullable: false),
                    CollectionDayBattlesPlayed = table.Column<int>(nullable: false),
                    NumberOfBattles = table.Column<int>(nullable: false),
                    BattlesPlayed = table.Column<int>(nullable: false),
                    Wins = table.Column<int>(nullable: false),
                    WarlogCreatedDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarResults", x => new { x.PlayerTag, x.WarLogDate });
                    table.ForeignKey(
                        name: "FK_WarResults_ClanMembers_PlayerTag",
                        column: x => x.PlayerTag,
                        principalTable: "ClanMembers",
                        principalColumn: "Tag",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarResults_WarHistory_WarlogCreatedDate",
                        column: x => x.WarlogCreatedDate,
                        principalTable: "WarHistory",
                        principalColumn: "CreatedDate",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationRecords_PlayerTag",
                table: "DonationRecords",
                column: "PlayerTag");

            migrationBuilder.CreateIndex(
                name: "IX_WarResults_WarlogCreatedDate",
                table: "WarResults",
                column: "WarlogCreatedDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationRecords");

            migrationBuilder.DropTable(
                name: "WarResults");

            migrationBuilder.DropTable(
                name: "ClanMembers");

            migrationBuilder.DropTable(
                name: "WarHistory");
        }
    }
}
