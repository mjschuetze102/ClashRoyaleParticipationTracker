using System.ComponentModel.DataAnnotations;

namespace ClashRoyaleApiQuery.Models
{
    class WarParticipant
    {
        [Key]
        public string Tag { get; set; }

        public string Name { get; set; }

        public int CardsEarned { get; set; }

        public int BattlesPlayed { get; set; }

        public int Wins { get; set; }

        public int CollectionDayBattlesPlayed { get; set; }

        public int NumberOfBattles { get; set; }
    }
}
