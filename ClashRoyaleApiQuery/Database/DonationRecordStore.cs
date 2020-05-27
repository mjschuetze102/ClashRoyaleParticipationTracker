using ClashRoyaleDataModel.DatabaseContexts;
using ClashRoyaleDataModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery.Database
{
    class DonationRecordStore
    {
        /// <summary>
        /// Reference to the database used to store the information
        /// </summary>
        private readonly ClanParticipationContext _context;

        /// <summary>
        /// Reference to the players already stored in the database
        /// </summary>
        private readonly HashSet<Player> _players;

        public DonationRecordStore(ClanParticipationContext context)
        {
            _context = context;
            _players = _context.ClanMembers.Include(p => p.DonationRecords).ToHashSet();
        }

        public void StoreAll(HashSet<DonationRecord> newRecords)
        {
            // Update the donation record for all clan members
            foreach (var record in newRecords)
            {
                // Update the donation record for the player
                Player player = GetPlayerFromDatabase(record.Player);

                // Remove the old record from the database
                var oldRecord = player.DonationRecords.Where(r => AreFallingInSameWeek(r.StoredDate, record.StoredDate, DayOfWeek.Monday)).FirstOrDefault();
                if (player.DonationRecords.Contains(oldRecord))
                    player.DonationRecords.Remove(oldRecord);

                // Update the database with the new record
                player.DonationRecords.Add(record);

                // Set the player object to be equivalent to the one that is tracked
                record.Player = player;
            }

            // Save information to the database
            _context.DonationRecords.UpdateRange(newRecords.Intersect(_context.DonationRecords));
            _context.DonationRecords.AddRange(newRecords.Except(_context.DonationRecords));
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

        /// <summary>
        /// Checks to see if two dates fall within the same week
        /// </summary>
        /// <param name="date1">First date to compare</param>
        /// <param name="date2">Second date to compare</param>
        /// <param name="weekStartsOn">Day of the week that weeks start on</param>
        /// <returns>Whether or not the two dates fall within the same week</returns>
        private bool AreFallingInSameWeek(DateTime date1, DateTime date2, DayOfWeek weekStartsOn)
        {
            return date1.AddDays(-((int)date1.DayOfWeek - (int)weekStartsOn + 7) % 7).Date == date2.AddDays(-((int)date2.DayOfWeek - (int)weekStartsOn + 7) % 7).Date;
        }
    }
}
