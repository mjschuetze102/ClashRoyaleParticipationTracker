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

        /// <summary>
        /// Reference to the players already stored in the database
        /// </summary>
        private readonly HashSet<Player> _players;

        public WarLogStore(ClanParticipationContext context)
        {
            _context = context;
            _players = _context.ClanMembers.ToHashSet();
        }

        public void StoreAll(HashSet<WarLog> warLogs)
        {
            // Filter out wars already tracked by the database
            warLogs.ExceptWith(_context.WarHistory);

            // Loop through each war log adding appropriate data to the associated player object
            foreach (var warlog in warLogs)
            {
                // Update the participation record for all participants of the war
                foreach (WarParticipation participation in warlog.Participants)
                {
                    // Add the war participation record to the player
                    Player player = GetPlayerFromDatabase(participation.Player);
                    player.WarParticipations.Add(participation);

                    // Set the player object to be equivalent to the one that is tracked
                    participation.Player = player;
                }
            }

            // Save information to the database
            _context.WarHistory.AddRange(warLogs);
            _context.SaveChanges();
        }

        /// <summary>
        /// Loads the data associated with the player from the database
        /// If player is not found, add the player to the collection
        /// </summary>
        /// <param name="player">Player object to search the database for</param>
        /// <returns>Player being tracked to later be added to the database</returns>
        private Player GetPlayerFromDatabase(Player player)
        {
            if (!_players.Contains(player))
                _players.Add(player);

            _players.TryGetValue(player, out player);
            return player;
        }
    }
}
