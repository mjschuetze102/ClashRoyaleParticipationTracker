using ClashRoyaleApiQuery.Api;
using ClashRoyaleDataModel.DatabaseContexts;
using ClashRoyaleDataModel.Models;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery.Database
{
    class WarLogStore : DataStore<WarLog>
    {
        public WarLogStore(ClanParticipationContext context, string clanTag) : base(context, $"clans/%23{clanTag}/warlog")
        {
        }

        protected override IEnumerable<WarLog> GetDataFromApi()
        {
            try
            {
                return ApiConnection.GetRequestToAPI<WarLogs>(_endpointUrl).GetAwaiter().GetResult().Items;
            }
            catch (ApiException)
            {
                return new List<WarLog>();
            }
        }

        protected override void SaveAll(IEnumerable<WarLog> warLogs)
        {
            // Filter out wars already tracked by the database
            warLogs = warLogs.ToHashSet().Except(_context.WarHistory);

            // Loop through each war log adding appropriate data to the associated player object
            foreach (var warlog in warLogs)
            {
                // Update the participation record for all participants of the war
                foreach (WarParticipation participation in warlog.Participants)
                {
                    // Add the war participation record to the player
                    Player player = _context.ClanMembers.Find(participation.Player.Tag) ?? participation.Player;
                    player.WarParticipations.Add(participation);

                    // Set the player object to be equivalent to the one that is tracked
                    participation.Player = player;
                }

                // Track the changes made
                _context.WarHistory.Add(warlog);
            }

            // Save information to the database
            _context.SaveChanges();
        }
    }
}
