using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleDataModel.Migrations
{
    public partial class InformationCorrectlyReadFromAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarResults_ClanMembers_PlayerTag",
                table: "WarResults");

            migrationBuilder.DropForeignKey(
                name: "FK_WarResults_WarHistory_WarlogCreatedDate",
                table: "WarResults");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarResults",
                table: "WarResults");

            migrationBuilder.RenameTable(
                name: "WarResults",
                newName: "WarParticipations");

            migrationBuilder.RenameIndex(
                name: "IX_WarResults_WarlogCreatedDate",
                table: "WarParticipations",
                newName: "IX_WarParticipations_WarlogCreatedDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarParticipations",
                table: "WarParticipations",
                columns: new[] { "PlayerTag", "WarLogDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarParticipations_ClanMembers_PlayerTag",
                table: "WarParticipations",
                column: "PlayerTag",
                principalTable: "ClanMembers",
                principalColumn: "Tag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarParticipations_WarHistory_WarlogCreatedDate",
                table: "WarParticipations",
                column: "WarlogCreatedDate",
                principalTable: "WarHistory",
                principalColumn: "CreatedDate",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarParticipations_ClanMembers_PlayerTag",
                table: "WarParticipations");

            migrationBuilder.DropForeignKey(
                name: "FK_WarParticipations_WarHistory_WarlogCreatedDate",
                table: "WarParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarParticipations",
                table: "WarParticipations");

            migrationBuilder.RenameTable(
                name: "WarParticipations",
                newName: "WarResults");

            migrationBuilder.RenameIndex(
                name: "IX_WarParticipations_WarlogCreatedDate",
                table: "WarResults",
                newName: "IX_WarResults_WarlogCreatedDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarResults",
                table: "WarResults",
                columns: new[] { "PlayerTag", "WarLogDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarResults_ClanMembers_PlayerTag",
                table: "WarResults",
                column: "PlayerTag",
                principalTable: "ClanMembers",
                principalColumn: "Tag",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarResults_WarHistory_WarlogCreatedDate",
                table: "WarResults",
                column: "WarlogCreatedDate",
                principalTable: "WarHistory",
                principalColumn: "CreatedDate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
