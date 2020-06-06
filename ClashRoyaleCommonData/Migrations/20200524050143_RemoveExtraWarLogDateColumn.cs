using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyaleDataModel.Migrations
{
    public partial class RemoveExtraWarLogDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarParticipations_WarHistory_WarlogCreatedDate",
                table: "WarParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarParticipations",
                table: "WarParticipations");

            migrationBuilder.DropColumn(
                name: "WarLogDate",
                table: "WarParticipations");

            migrationBuilder.RenameColumn(
                name: "WarlogCreatedDate",
                table: "WarParticipations",
                newName: "WarLogCreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_WarParticipations_WarlogCreatedDate",
                table: "WarParticipations",
                newName: "IX_WarParticipations_WarLogCreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "WarLogCreatedDate",
                table: "WarParticipations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarParticipations",
                table: "WarParticipations",
                columns: new[] { "PlayerTag", "WarLogCreatedDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarParticipations_WarHistory_WarLogCreatedDate",
                table: "WarParticipations",
                column: "WarLogCreatedDate",
                principalTable: "WarHistory",
                principalColumn: "CreatedDate",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarParticipations_WarHistory_WarLogCreatedDate",
                table: "WarParticipations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarParticipations",
                table: "WarParticipations");

            migrationBuilder.RenameColumn(
                name: "WarLogCreatedDate",
                table: "WarParticipations",
                newName: "WarlogCreatedDate");

            migrationBuilder.RenameIndex(
                name: "IX_WarParticipations_WarLogCreatedDate",
                table: "WarParticipations",
                newName: "IX_WarParticipations_WarlogCreatedDate");

            migrationBuilder.AlterColumn<string>(
                name: "WarlogCreatedDate",
                table: "WarParticipations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "WarLogDate",
                table: "WarParticipations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarParticipations",
                table: "WarParticipations",
                columns: new[] { "PlayerTag", "WarLogDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarParticipations_WarHistory_WarlogCreatedDate",
                table: "WarParticipations",
                column: "WarlogCreatedDate",
                principalTable: "WarHistory",
                principalColumn: "CreatedDate",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
