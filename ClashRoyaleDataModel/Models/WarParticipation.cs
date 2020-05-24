using Newtonsoft.Json;

namespace ClashRoyaleDataModel.Models
{
    public class WarParticipation
    {
        /// <summary>
        /// Tag for the player whom these results belong to
        /// </summary>
        /// <remarks>Composite and foreign key attribute</remarks>
        public string PlayerTag { get; set; }

        /// <summary>
        /// Date at which the war these results are from took place
        /// </summary>
        /// <remarks>Composite and foreign key attribute</remarks>
        public string WarLogCreatedDate { get; set; }

        /// <summary>
        /// Number of cards earned
        /// </summary>
        public int CardsEarned { get; set; }

        /// <summary>
        /// Number of collection day battles played
        /// </summary>
        public int CollectionDayBattlesPlayed { get; set; }

        /// <summary>
        /// Number of war day battles
        /// </summary>
        public int NumberOfBattles { get; set; }

        /// <summary>
        /// Number of war day battles played
        /// </summary>
        public int BattlesPlayed { get; set; }

        /// <summary>
        /// Number of wins during way day battles
        /// </summary>
        public int Wins { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Reference to the player whom these results belong to
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Reference to the war these results are from
        /// </summary>
        public WarLog Warlog { get; set; }

        public WarParticipation()
        {
        }

        [JsonConstructor]
        public WarParticipation(string tag, string name)
        {
            PlayerTag = tag;
            Player = new Player
            {
                Name = name,
                Tag = tag,
            };
        }
    }
}
