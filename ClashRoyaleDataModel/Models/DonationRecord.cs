using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ClashRoyaleDataModel.Models
{
    /// <summary>
    /// Donation record object as received by the API.
    /// </summary>
    public class DonationRecords
    {
        /// <summary>
        /// Collection of donation records.
        /// </summary>
        public IEnumerable<DonationRecord> Items { get; set; }
    }

    /// <summary>
    /// Record keeping track of the clan member and the donations they made each week.
    /// </summary>
    public class DonationRecord
    {
        /// <summary>
        /// Date at which the donation record was collected.
        /// </summary>
        /// <remarks>Composite key attribute.</remarks>
        public DateTime StoredDate { get; set; }

        /// <summary>
        /// Tag for the player whom this record belongs to.
        /// </summary>
        /// <remarks>Composite and foreign key attribute.</remarks>
        public string PlayerTag { get; set; }

        /// <summary>
        /// Number of donations player made during the week.
        /// </summary>
        public int Donations { get; set; }

        /// <summary>
        /// Number of donations player received during the week.
        /// </summary>
        public int DonationsReceived { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Reference to the player whom this record belongs to.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Creates an empty record for clan member weekly donations.
        /// </summary>
        public DonationRecord()
        {
        }

        /// <summary>
        /// Creates a record for clan member weekly donations.
        /// Simultaneously generates a new player object to keep track of who made the donation.
        /// Computes the time for which the record was collected.
        /// </summary>
        /// <param name="tag">Unique identifier of the clan member.</param>
        /// <param name="name">Name of the clan member.</param>
        [JsonConstructor]
        public DonationRecord(string tag, string name)
        {
            var storedDate = DateTime.Now;
            StoredDate = storedDate.AddTicks(-(storedDate.Ticks % TimeSpan.TicksPerSecond));
            PlayerTag = tag;
            Player = new Player {
                Name = name,
                Tag = tag,
            };
        }
    }
}
