using ClashRoyaleApiQuery.Database;
using ClashRoyaleDataModel.DatabaseContexts;
using System;
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
            _data = data;
            _context = context;
        }

        /// <summary>
        /// Store the data gathered from the API into the database
        /// </summary>
        public void StoreData()
        {
            var donationsStore = new DonationRecordStore(_context);
            donationsStore.StoreAll(_data.GetDonationRecords().ToHashSet());

            var warLogStore = new WarLogStore(_context);
            warLogStore.StoreAll(_data.GetWarLogs().ToHashSet());

            var players = _context.ClanMembers.ToHashSet();

            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name,15}, {(player.DonationRecords.Count > 0 ? player.DonationRecords.First().Donations : 0),3}, {player.WarParticipations.Count,2}");
            }
        }
    }
}
