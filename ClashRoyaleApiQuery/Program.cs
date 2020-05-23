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

            HashSet<Player> players = new HashSet<Player>();

            foreach (WarLog warlog in warLogs)
            {
                foreach (WarParticipation participation in warlog.WarParticipants)
                {
                    Player player = participation.Player;

                    if (!players.Contains(player))
                        players.Add(player);

                    players.TryGetValue(player, out player);
                    player.WarParticipations.Add(participation);
                }
            }

            foreach (DonationRecord record in donationRecords)
            {
                Player player = record.Player;

                if (!players.Contains(player))
                    players.Add(player);

                players.TryGetValue(player, out player);
                player.DonationRecords.Add(record);
            }

            foreach(Player player in players)
            {
                Console.WriteLine($"{player.Tag, 10}, {player.DonationRecords.Count, 3}, {player.WarParticipations.Count, 2}");
            }
        }
    }
}
