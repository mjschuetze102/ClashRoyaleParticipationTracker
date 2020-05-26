using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClashRoyaleApiQuery
{
    class ApiConnection
    {
        /// <summary>
        /// Used to make HTTP requests and receive responses
        /// </summary>
        HttpClient client;

        /// <summary>
        /// Initialize the HttpClient with the information needed to connect to the API
        /// </summary>
        /// <param name="baseUrl">The base url for the API</param>
        /// <param name="apiKey">Token used for authorization with the API client</param>
        public ApiConnection(string baseUrl, string apiKey)
        {
            // Set the URL the API is located at
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            // Make sure only json data will be accepted
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // Add the API key to the request
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        }

        /// <summary>
        /// Perform a GET request to the specified url
        /// </summary>
        /// <typeparam name="T">Type of object being received from the API</typeparam>
        /// <param name="url">Url to connect to to retreive data</param>
        /// <returns>Object of type T containing information from the API</returns>
        internal async Task<T> GetRequestToAPI<T>(string url)
        {
            // Make a GET request to the specified URL
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await client.SendAsync(request))
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
        private T GetObjectFromStream<T>(Stream stream)
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
