using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

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
        [JsonProperty("participants")]
        public ICollection<WarParticipation> WarParticipants { get; set; }

        /// <summary>
        /// Adds a reference to this object to each of the war participant records
        /// </summary>
        /// <param name="context">Stream with which the object was deserialized</param>
        [OnDeserialized]
        public void AddReferenceToEachParticipant(StreamingContext context)
        {
            foreach (WarParticipation participant in WarParticipants)
            {
                participant.Warlog = this;
                participant.WarLogDate = CreatedDate;
            }
        }
    }
}
