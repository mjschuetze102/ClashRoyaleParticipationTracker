using ClashRoyaleApiQuery.Models;
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
            string url = $"clans/%23{config["clash_royale_clan_tag"]}/warlog";
            IEnumerable<WarLog> warlogs = api.GetRequestToAPI<WarLogs>(url).GetAwaiter().GetResult().Items;

            foreach (WarLog warlog in warlogs)
            {
                Console.WriteLine(warlog.CreatedDate);
            }
        }
    }
}
