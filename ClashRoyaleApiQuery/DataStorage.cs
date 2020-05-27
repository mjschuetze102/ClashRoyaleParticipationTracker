using ClashRoyaleApiQuery.Database;
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

        public DataStorage(DataCollection data, ClanParticipationContext context)
        {
            this._data = data;
            this._context = context;
        }

        /// <summary>
        /// Store the data gathered from the API into the database
        /// </summary>
        public void StoreData()
        {
            var donationsStore = new DonationRecordStore(_context);
            IEnumerable<DonationRecord> donations = _data.GetDonationRecords();
            donationsStore.StoreAll(donations.ToHashSet());

            var warLogStore = new WarLogStore(_context);
            IEnumerable<WarLog> warLogs = _data.GetWarLogs();
            warLogStore.StoreAll(warLogs.ToHashSet());

            var players = _context.ClanMembers.ToHashSet();

            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name,15}, {(player.DonationRecords.Count > 0 ? player.DonationRecords.First().Donations : 0),3}, {player.WarParticipations.Count,2}");
            }
        }
    }
}
