using ClashRoyaleDataModel.DatabaseContexts;
using ClashRoyaleDataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery
{
    class DataStorage
    {
        DataCollection data;
        ClanParticipationContext context;
        HashSet<Player> players;
        HashSet<WarLog> previousWars;

        public DataStorage(DataCollection data, ClanParticipationContext context)
        {
            this.data = data;
            this.context = context;
            players = context.ClanMembers.ToHashSet();
            previousWars = context.WarHistory.ToHashSet();
        }

        public void StoreData()
        {
            IEnumerable<WarLog> warLogs = data.GetWarLogs();
            IEnumerable<DonationRecord> donationRecords = data.GetDonationRecords();

            HashSet<WarLog> newWars = new HashSet<WarLog>();

            foreach (WarLog warlog in warLogs)
            {
                if (previousWars.Contains(warlog))
                    continue;

                foreach (WarParticipation participation in warlog.Participants)
                {
                    Player player = GetPlayerFromDatabase(participation.Player);
                    player.WarParticipations.Add(participation);
                    participation.Player = player;
                }

                newWars.Add(warlog);
            }

            foreach (DonationRecord record in donationRecords)
            {
                Player player = GetPlayerFromDatabase(record.Player);
                player.DonationRecords.Add(record);
                record.Player = player;
            }

            foreach (Player player in players)
            {
                Console.WriteLine($"{player.Name,15}, {(player.DonationRecords.Count > 0 ? player.DonationRecords.First().Donations : 0),3}, {player.WarParticipations.Count,2}");
            }

            context.WarHistory.AddRange(newWars);
            context.DonationRecords.AddRange(donationRecords);
            context.SaveChanges();
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
