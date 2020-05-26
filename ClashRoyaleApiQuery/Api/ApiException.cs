using Newtonsoft.Json;
using System;

namespace ClashRoyaleApiQuery.Api
{
    class ApiException : Exception
    {
        /// <summary>
        /// Status code received from the API
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Reason the API call failed
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Message received from the API
        /// </summary>
        [JsonProperty("Message")]
        public string Content { get; set; }

        /// <summary>
        /// Type of failure from the API
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Detailed message received from the API
        /// </summary>
        public string Detail { get; set; }
    }
}
