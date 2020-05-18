using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleDataModel.Models
{
    public class Player
    {
        /// <summary>
        /// Unique identifier for the player
        /// </summary>
        /// <remarks>Primary key attribute</remarks>
        [Key]
        public string Tag { get; set; }

        /// <summary>
        /// Name of the player
        /// </summary>
        public string Name { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Collection of donation records for the player
        /// </summary>
        public ICollection<DonationRecord> DonationRecords;

        /// <summary>
        /// Collection of war participations for the player
        /// </summary>
        public ICollection<WarParticipation> WarParticipations;
    }
}
