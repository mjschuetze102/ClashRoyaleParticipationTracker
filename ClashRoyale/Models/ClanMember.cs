using System.ComponentModel.DataAnnotations;

namespace ClashRoyale.Models
{
    public class ClanMember
    {
        [Key]
        public string Tag { get; set; }

        public string Name { get; set; }

        public int Donations { get; set; }

        public int DonationsReceived { get; set; }
    }
}
