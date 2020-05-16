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

            ApiConnection api = new ApiConnection(config["clash_royale_api_key"]);
            IEnumerable<WarLog> warlogs = api.ConnectToAPI(config["clash_royale_clan_tag"]).GetAwaiter().GetResult();

            foreach (WarLog warlog in warlogs)
            {
                Console.WriteLine(warlog.CreatedDate);
            }
        }
    }
}
