using ClashRoyaleDataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyaleDataModel.DatabaseContexts
{
    public class ClanParticipationContext : DbContext
    {
        public ClanParticipationContext(DbContextOptions<ClanParticipationContext> options) : base(options)
        {
        }

        public DbSet<Player> ClanMembers { get; set; }
        public DbSet<DonationRecord> DonationRecords { get; set; }
        public DbSet<WarParticipation> WarResults { get; set; }
        public DbSet<WarLog> WarHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonationRecord>()
                .HasKey(d => new { d.StoredDate, d.PlayerTag });
            modelBuilder.Entity<WarParticipation>()
                .HasKey(w => new { w.PlayerTag, w.WarLogDate });
        }
    }
}
