using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleDataModel.Models
{
    /// <summary>
    /// Keeps track of information about each clan member such as name and tag.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Unique identifier for the player.
        /// </summary>
        /// <remarks>Primary key attribute.</remarks>
        [Key]
        public string Tag { get; set; }

        /// <summary>
        /// Name of the player.
        /// </summary>
        public string Name { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Collection of donation records for the player.
        /// </summary>
        public ICollection<DonationRecord> DonationRecords { get; set; }

        /// <summary>
        /// Collection of war participations for the player.
        /// </summary>
        public ICollection<WarParticipation> WarParticipations { get; set; }

        /// <summary>
        /// Creates a new player.
        /// Initializes empty lists for donation records and war participations.
        /// </summary>
        public Player()
        {
            DonationRecords = new List<DonationRecord>();
            WarParticipations = new List<WarParticipation>();
        }

        /// <summary>
        /// Compares an object to the player to see if they are equal.
        /// </summary>
        /// <param name="obj">Object being compared to the player.</param>
        /// <returns>Whether or not the two objects are equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Player))
            {
                return false;
            }

            return Tag.Equals(((Player)obj).Tag);
        }

        /// <summary>
        /// Generates a hashcode for the player.
        /// </summary>
        /// <returns>int hashcode that will be used to identify the player.</returns>
        public override int GetHashCode()
        {
            return Tag.GetHashCode();
        }
    }
}
