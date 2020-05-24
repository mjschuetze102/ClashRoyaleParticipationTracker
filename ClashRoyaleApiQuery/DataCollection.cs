using ClashRoyaleDataModel.Models;
using System.Collections.Generic;

namespace ClashRoyaleApiQuery
{
    /// <summary>
    /// Collects the information from the API
    /// </summary>
    class DataCollection
    {
        /// <summary>
        /// Reference to the API connection which will be making the requests
        /// </summary>
        ApiConnection api;

        /// <summary>
        /// Endpoint to hit to load war log information
        /// </summary>
        private readonly string WarLogUrl;

        /// <summary>
        /// Endpoint to hit to load donation record information
        /// </summary>
        private readonly string DonationRecordUrl;

        public DataCollection(ApiConnection api, string clanTag)
        {
            this.api = api;
            WarLogUrl = $"clans/%23{clanTag}/warlog";
            DonationRecordUrl = $"clans/%23{clanTag}/members";
        }

        /// <summary>
        /// Retrieve the war log information from the API
        /// </summary>
        /// <returns>List of WarLogs retrieved from the API</returns>
        public IEnumerable<WarLog> GetWarLogs()
        {
            try
            {
                return api.GetRequestToAPI<WarLogs>(WarLogUrl).GetAwaiter().GetResult().Items;
            }
            catch (ApiException)
            {
                return new List<WarLog>();
            }
        }
        
        /// <summary>
        /// Retreive the donation record information from the API
        /// </summary>
        /// <returns>List of DonationRecords retrieved from the API</returns>
        public IEnumerable<DonationRecord> GetDonationRecords()
        {
            try
            { 
                return api.GetRequestToAPI<DonationRecords>(DonationRecordUrl).GetAwaiter().GetResult().Items;
            }
            catch (ApiException)
            {
                return new List<DonationRecord>();
            }
        }
    }
}
