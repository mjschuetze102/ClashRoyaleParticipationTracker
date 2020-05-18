using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleDataModel.Models
{
    public class WarLogs
    {
        /// <summary>
        /// War log object as received by the API
        /// </summary>
        public IEnumerable<WarLog> Items { get; set; }
    }

    public class WarLog
    {
        /// <summary>
        /// Date at which the war was created
        /// </summary>
        /// <remarks>Primary key attribute</remarks>
        [Key]
        public string CreatedDate { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Collection of players who participated in the war
        /// </summary>
        public ICollection<WarParticipation> WarParticipants { get; set; }
    }
}
