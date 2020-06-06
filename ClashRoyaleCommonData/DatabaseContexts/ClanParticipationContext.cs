using ClashRoyaleDataModel.Models;
using Microsoft.EntityFrameworkCore;

namespace ClashRoyaleDataModel.DatabaseContexts
{
    /// <summary>
    /// Database entries related to donations and war battles.
    /// </summary>
    public class ClanParticipationContext : DbContext
    {
        /// <summary>
        /// Initializes the DbContext with the specified options.
        /// Also ensures that the database is created.
        /// </summary>
        /// <param name="options">Options used to initialize the database.</param>
        public ClanParticipationContext(DbContextOptions<ClanParticipationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// The collection of clan members.
        /// </summary>
        public DbSet<Player> ClanMembers { get; set; }

        /// <summary>
        /// The collection of clan member donation history.
        /// </summary>
        public DbSet<DonationRecord> DonationRecords { get; set; }

        /// <summary>
        /// The collection of clan member war participation history.
        /// </summary>
        public DbSet<WarParticipation> WarParticipations { get; set; }

        /// <summary>
        /// The collection of wars the clan has been a part of.
        /// </summary>
        public DbSet<WarLog> WarHistory { get; set; }

        /// <summary>
        /// Assists with configuring database tables.
        /// Specifies composite key for entities.
        /// </summary>
        /// <param name="modelBuilder">The builder used to map entities and their relationships.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonationRecord>()
                .HasKey(d => new { d.StoredDate, d.PlayerTag });
            modelBuilder.Entity<WarParticipation>()
                .HasKey(w => new { w.PlayerTag, w.WarLogCreatedDate });
        }
    }
}
