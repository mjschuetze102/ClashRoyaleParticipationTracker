using ClashRoyaleDataModel.DatabaseContexts;
using ClashRoyaleDataModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery.Database
{
    class WarLogStore
    {
        /// <summary>
        /// Reference to the database used to store the information
        /// </summary>
        private readonly ClanParticipationContext _context;

        public WarLogStore(ClanParticipationContext context)
        {
            _context = context;
        }

        public void StoreAll(IEnumerable<WarLog> warLogs)
        {
            // Filter out wars already tracked by the database
            warLogs = warLogs.ToHashSet().Except(_context.WarHistory);

            // Loop through each war log adding appropriate data to the associated player object
            foreach (var warlog in warLogs)
            {
                // Update the participation record for all participants of the war
                foreach (WarParticipation participation in warlog.Participants)
                {
                    // Add the war participation record to the player
                    Player player = _context.ClanMembers.Find(participation.Player.Tag) ?? participation.Player;
                    player.WarParticipations.Add(participation);

                    // Set the player object to be equivalent to the one that is tracked
                    participation.Player = player;
                }

                // Track the changes made to the database
                _context.WarHistory.Add(warlog);
            }

            // Save information to the database
            _context.SaveChanges();
        }
    }
}
