using ClashRoyaleDataModel.Models;
using System.Collections.Generic;

namespace ClashRoyaleApiQuery
{
    class DataCollection
    {
        ApiConnection api;
        private readonly string WarLogUrl;
        private readonly string DonationRecordUrl;

        public DataCollection(ApiConnection api, string clanTag)
        {
            this.api = api;
            WarLogUrl = $"clans/%23{clanTag}/warlog";
            DonationRecordUrl = $"clans/%23{clanTag}/members";
        }

        public IEnumerable<WarLog> GetWarLogs()
        {
            return api.GetRequestToAPI<WarLogs>(WarLogUrl).GetAwaiter().GetResult().Items;
        }

        public IEnumerable<DonationRecord> GetDonationRecords()
        {
            return api.GetRequestToAPI<DonationRecords>(DonationRecordUrl).GetAwaiter().GetResult().Items;
        }
    }
}
