using ClashRoyaleDataModel.Converters;
using Newtonsoft.Json;
using System;
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
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreatedDate { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Collection of players who participated in the war
        /// </summary>
        public ICollection<WarParticipation> Participants { get; set; }

        /// <summary>
        /// Adds a reference to this object to each of the war participant records
        /// </summary>
        /// <param name="context">Stream with which the object was deserialized</param>
        [OnDeserialized]
        public void AddReferenceToEachParticipant(StreamingContext context)
        {
            foreach (var participant in Participants)
            {
                participant.Warlog = this;
                participant.WarLogCreatedDate = CreatedDate;
            }
        }

        /// <summary>
        /// Compares an object to the war log to see if they are equal
        /// </summary>
        /// <param name="obj">Object being compared to the war log</param>
        /// <returns>Whether or not the two objects are equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is WarLog))
            {
                return false;
            }

            return CreatedDate.Equals(((WarLog)obj).CreatedDate);
        }

        /// <summary>
        /// Generates a hashcode for the war log
        /// </summary>
        /// <returns>int hashcode that will be used to identify the war log</returns>
        public override int GetHashCode()
        {
            return CreatedDate.GetHashCode();
        }
    }
}
