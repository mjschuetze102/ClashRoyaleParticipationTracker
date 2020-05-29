namespace ClashRoyaleDataModel.Configuration
{
    /// <summary>
    /// Configuration settings related to Clash Royale.
    /// </summary>
    public class ClashRoyaleConfiguration
    {
        /// <summary>
        /// Configuration settings for the API.
        /// </summary>
        public ApiConfiguration Api { get; set; }

        /// <summary>
        /// Clan tag for the clan being monitored.
        /// </summary>
        public string ClanTag { get; set; }
    }
}
