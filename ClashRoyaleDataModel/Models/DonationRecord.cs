using System;
using System.Collections.Generic;

namespace ClashRoyaleDataModel.Models
{
    public class DonationRecords
    {
        /// <summary>
        /// Donation record object as received by the API
        /// </summary>
        public IEnumerable<DonationRecord> Items { get; set; }
    }

    public class DonationRecord
    {
        /// <summary>
        /// Date at which the donation record was collected
        /// </summary>
        /// <remarks>Composite key attribute</remarks>
        public DateTime StoredDate { get; set; }

        /// <summary>
        /// Tag for the player whom this record belongs to
        /// </summary>
        /// <remarks>Composite and foreign key attribute</remarks>
        public string PlayerTag { get; set; }

        /// <summary>
        /// Number of donations player made during the week
        /// </summary>
        public int Donations { get; set; }

        /// <summary>
        /// Number of donations player received during the week
        /// </summary>
        public int DonationsReceived { get; set; }

        // ///////////////////////////////////////////////
        //             Navigation Properties
        // ///////////////////////////////////////////////

        /// <summary>
        /// Reference to the player whom this record belongs to
        /// </summary>
        public Player Player { get; set; }
    }
}
