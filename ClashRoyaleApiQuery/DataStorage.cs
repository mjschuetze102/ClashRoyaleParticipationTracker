using ClashRoyaleDataModel.DatabaseContexts;
using ClashRoyaleDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery
{
    class DataStorage
    {
        /// <summary>
        /// Reference to the API connection
        /// </summary>
        private readonly DataCollection _data;

        /// <summary>
        /// Reference to the database to store the information to
        /// </summary>
        private readonly ClanParticipationContext _context;

        /// <summary>
        /// List of players to track before adding to the database
        /// </summary>
        private readonly HashSet<Player> _players;

        /// <summary>
        /// List of wars that are already tracked in the database
        /// </summary>
        private readonly HashSet<WarLog> _previousWars;

        public DataStorage(DataCollection data, ClanParticipationContext context)
        {
            this._data = data;
            this._context = context;
            _players = context.ClanMembers.ToHashSet();
            _previousWars = context.WarHistory.ToHashSet();
        }

        /// <summary>
        /// Store the data gathered from the API into the database
        /// </summary>
        public void StoreData()
        {
            SaveWarLogData();
            SaveDonationRecordData();

            foreach (var player in _players)
            {
                Console.WriteLine($"{player.Name,15}, {(player.DonationRecords.Count > 0 ? player.DonationRecords.First().Donations : 0),3}, {player.WarParticipations.Count,2}");
            }
        }

        /// <summary>
        /// Saves war log data to the database
        /// </summary>
        private void SaveWarLogData()
        {
            // Load the data from the API
            IEnumerable<WarLog> warLogs = _data.GetWarLogs();

            // Create a list to keep track of any wars not already tracked in the database
            var newWars = new HashSet<WarLog>();

            // Loop through each war log adding appropriate data to the associated player object
            foreach (var warlog in warLogs)
            {
                if (_previousWars.Contains(warlog))
                    continue;

                // Add the new war log to the list which will be added to the database
                newWars.Add(warlog);

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

            _context.WarHistory.AddRange(newWars);
            _context.SaveChanges();
        }

        /// <summary>
        /// Saves donation record data to the database
        /// </summary>
        private void SaveDonationRecordData()
        {
            // Load the data from the API
            IEnumerable<DonationRecord> donationRecords = _data.GetDonationRecords();

            // Update the donation record for all clan members
            foreach (var record in donationRecords)
            {
                // Add the donation record to the player
                Player player = GetPlayerFromDatabase(record.Player);
                player.DonationRecords.Add(record);

                // Set the player object to be equivalent to the one that is tracked
                record.Player = player;
            }

            // Save the information to the database
            _context.DonationRecords.AddRange(donationRecords);
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
