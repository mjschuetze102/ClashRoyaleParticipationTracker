using System.Collections.Generic;

namespace ClashRoyaleApiQuery.Models
{
    class WarLogs
    {
        public IEnumerable<WarLog> Items { get; set; }
    }

    class WarLog
    {
        public string CreatedDate { get; set; }

        public IEnumerable<WarParticipant> WarParticipants { get; set; }
    }
}
