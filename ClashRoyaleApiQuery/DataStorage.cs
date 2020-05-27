using ClashRoyaleApiQuery.Database;
using ClashRoyaleDataModel.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

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
            context.ClanMembers.Include(m => m.DonationRecords).Load();
        }

        /// <summary>
        /// Store the data gathered from the API into the database
        /// </summary>
        public void StoreData()
        {
            var donationsStore = new DonationRecordStore(_context);
            donationsStore.StoreAll(_data.GetDonationRecords());

            var warLogStore = new WarLogStore(_context);
            warLogStore.StoreAll(_data.GetWarLogs());
        }
    }
}
