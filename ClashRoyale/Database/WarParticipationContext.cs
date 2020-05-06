using ClashRoyale.Models;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyale.Database
{
    public class WarParticipationContext : DbContext
    {
        public WarParticipationContext(DbContextOptions<WarParticipationContext> options) : base(options)
        {
        }

        public DbSet<ClanMember> ClanMembers { get; set; }
    }
}
