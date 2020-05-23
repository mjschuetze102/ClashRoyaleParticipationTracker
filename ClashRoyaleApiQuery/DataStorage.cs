using ClashRoyaleDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery
{
    class DataStorage
    {
        DataCollection data;
        HashSet<Player> players = new HashSet<Player>();

        public DataStorage(DataCollection data)
        {
            this.data = data;
        }

        public void StoreData()
        {
            IEnumerable<WarLog> warLogs = data.GetWarLogs();
            IEnumerable<DonationRecord> donationRecords = data.GetDonationRecords();

            foreach (WarLog warlog in warLogs)
            {
                foreach (WarParticipation participation in warlog.Participants)
                {
                    Player player = GetPlayerFromDatabase(participation.Player);
                    player.WarParticipations.Add(participation);
                }
            }

            foreach (DonationRecord record in donationRecords)
            {
                Player player = GetPlayerFromDatabase(record.Player);
                player.DonationRecords.Add(record);
            }

            foreach (Player player in players)
            {
                Console.WriteLine($"{player.Name,15}, {(player.DonationRecords.Count > 0 ? player.DonationRecords.First().Donations : 0),3}, {player.WarParticipations.Count,2}");
            }
        }

        private Player GetPlayerFromDatabase(Player player)
        {
            if (!players.Contains(player))
                players.Add(player);

            players.TryGetValue(player, out player);
            return player;
        }
    }
}
