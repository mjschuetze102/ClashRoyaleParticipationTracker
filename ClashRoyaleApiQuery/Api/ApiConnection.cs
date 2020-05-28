using ClashRoyaleDataModel.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClashRoyaleApiQuery.Api
{
    class ApiConnection
    {
        /// <summary>
        /// Used to make HTTP requests and receive responses
        /// </summary>
        private static HttpClient _client;

        /// <summary>
        /// Initialize the HttpClient with the information needed to connect to the API
        /// </summary>
        /// <param name="config">Configuration containing API base path and key</param>
        public ApiConnection(ApiConfiguration config)
        {
            // Set the URL the API is located at
            _client = new HttpClient();
            _client.BaseAddress = new Uri(config.Url);

            // Make sure only json data will be accepted
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Add the API key to the request
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + config.Key);
        }

        /// <summary>
        /// Perform a GET request to the specified url
        /// </summary>
        /// <typeparam name="T">Type of object being received from the API</typeparam>
        /// <param name="url">Url to connect to to retreive data</param>
        /// <returns>Object of type T containing information from the API</returns>
        internal static async Task<T> GetRequestToAPI<T>(string url)
        {
            // Make a GET request to the specified URL
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request))
            {
                Stream stream = await response.Content.ReadAsStreamAsync();

                // Get the specified object from the API
                if (response.IsSuccessStatusCode)
                    return GetObjectFromStream<T>(stream);

                // Get the error information if the API fails to load
                ApiException ex = GetObjectFromStream<ApiException>(stream);
                ex.StatusCode = (int)response.StatusCode;
                throw ex;
            }
        }

        /// <summary>
        /// Deserialize an object from the API response
        /// </summary>
        /// <typeparam name="T">Type of the object to deserialize</typeparam>
        /// <param name="stream">Stream which contains information relating to an object</param>
        /// <returns>An object deserialized from information within the stream</returns>
        private static T GetObjectFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default;

            // Deserialize the object from the JSON response
            using (var streamReader = new StreamReader(stream))
            using (var textReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<T>(textReader);
            }
        }
    }
}
