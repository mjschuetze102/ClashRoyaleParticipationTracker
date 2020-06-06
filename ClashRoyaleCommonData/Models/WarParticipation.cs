using Newtonsoft.Json;
using System;

namespace ClashRoyaleDataModel.Models
{
    /// <summary>
    /// Record keeping track of war statistics for a clan member.
    /// </summary>
    public class WarParticipation
    {
        /// <summary>
        /// Tag for the player whom these results belong to.
        /// </summary>
        /// <remarks>Composite and foreign key attribute.</remarks>
        [JsonIgnore]
        public string PlayerTag { get; set; }

        /// <summary>
        /// Date at which the war these results are from took place.
        /// </summary>
        /// <remarks>Composite and foreign key attribute.</remarks>
        public DateTime WarLogCreatedDate { get; set; }

        /// <summary>
        /// Number of cards earned.
        /// </summary>
        public int CardsEarned { get; set; }

        /// <summary>
        /// Number of collection day battles played.
        /// </summary>
        public int CollectionDayBattlesPlayed { get; set; }

        /// <summary>
        /// Number of war day battles.
        /// </summary>
        public int NumberOfBattles { get; set; }

        /// <summary>
        /// Number of war day battles played.
        /// </summary>
        public int BattlesPlayed { get; set; }

        /// <summary>
        /// Number of wins during way day battles.
        /// </summary>
        public int Wins { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Reference to the player whom these results belong to.
        /// </summary>
        [JsonIgnore]
        public Player Player { get; set; }

        /// <summary>
        /// Reference to the war these results are from.
        /// </summary>
        [JsonIgnore]
        public WarLog Warlog { get; set; }

        /// <summary>
        /// Creates an empty record for clan war participation.
        /// </summary>
        public WarParticipation()
        {
        }

        /// <summary>
        /// Creates a record for clan war participation.
        /// Simultaneously generates a new player object to keep track of who made the donation.
        /// </summary>
        /// <param name="tag">Unique identifier of the clan member.</param>
        /// <param name="name">Name of the clan member.</param>
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
