using Microsoft.EntityFrameworkCore.Migrations;

namespace ClashRoyale.Migrations
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
                    Name = table.Column<string>(nullable: true),
                    Donations = table.Column<int>(nullable: false),
                    DonationsReceived = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanMembers", x => x.Tag);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClanMembers");
        }
    }
}
