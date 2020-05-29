namespace ClashRoyaleDataModel.Configuration
{
    /// <summary>
    /// Configuration settings related to the Clash Royale API.
    /// </summary>
    public class ApiConfiguration
    {
        /// <summary>
        /// Base URL to the API.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Authorization token required by the API.
        /// </summary>
        public string Key { get; set; }
    }
}
