using ClashRoyale.Models;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyale.Database
{
    public class WarParticipationContext : DbContext
    {
        public DbSet<ClanMember> ClanMembers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlite("Data Source=clanmembers.db");
        }
    }
}
