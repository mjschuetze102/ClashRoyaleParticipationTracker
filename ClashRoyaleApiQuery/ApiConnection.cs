using ClashRoyaleApiQuery.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        /// Initiliaze the HttpClient with the information needed to connect to the API
        /// </summary>
        /// <param name="baseUrl">The base url for the API</param>
        /// <param name="apiKey">Token used for authorization with the API client</param>
        public ApiConnection(string baseUrl, string apiKey)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        }

        /// <summary>
        /// Error received from the API
        /// </summary>
        public class ApiException : Exception
        {
            /// <summary>
            /// Status code received from the API
            /// </summary>
            public int StatusCode { get; set; }

            /// <summary>
            /// Message received from the API
            /// </summary>
            public string Content { get; set; }
        }

        /// <summary>
        /// Perform a GET request to the specified url
        /// </summary>
        /// <typeparam name="T">Type of object being received from the API</typeparam>
        /// <param name="url">Url to connect to to retreive data</param>
        /// <returns>Object of type T containing information from the API</returns>
        internal async Task<T> GetRequestToAPI<T>(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await client.SendAsync(request))
            {
                Stream stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return GetObjectFromStream<T>(stream);

                string content = await GetErrorFromStream(stream);
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
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

            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonTextReader textReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                return serializer.Deserialize<T>(textReader);
            }
        }

        /// <summary>
        /// Retrieve the error message from the API
        /// </summary>
        /// <param name="stream">Stream response from the API</param>
        /// <returns>String containing the error message from the API</returns>
        private async Task<string> GetErrorFromStream(Stream stream)
        {
            if (stream != null)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string message = await streamReader.ReadToEndAsync();
                    return message;
                }
            }

            return null;
        }
    }
}
