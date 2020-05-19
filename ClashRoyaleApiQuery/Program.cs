using ClashRoyaleDataModel.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ClashRoyaleApiQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            ApiConnection api = new ApiConnection("https://api.clashroyale.com/v1/", config["clash_royale_api_key"]);
            DataCollection data = new DataCollection(api, config["clash_royale_clan_tag"]);

            IEnumerable<WarLog> warLogs = data.GetWarLogs();
            IEnumerable<DonationRecord> donationRecords = data.GetDonationRecords();

            foreach (WarLog warlog in warLogs)
            {
                Console.WriteLine(warlog.CreatedDate);
            }

            foreach (DonationRecord record in donationRecords)
            {
                Console.WriteLine(record.StoredDate);
            }
        }
    }
}
