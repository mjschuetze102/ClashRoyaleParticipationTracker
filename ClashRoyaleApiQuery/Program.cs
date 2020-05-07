using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClashRoyaleApiQuery
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<string> ConnectToAPI(string apiKey, string clanTag)
        {
            client.BaseAddress = new Uri("https://api.clashroyale.com/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

            HttpResponseMessage response = await client.GetAsync($"clans/%23{clanTag}/warlog");

            string message = await response.Content.ReadAsStringAsync();
            return message;
        }

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            Console.WriteLine(ConnectToAPI(config["clash_royale_api_key"], config["clash_royale_clan_tag"]).GetAwaiter().GetResult());
        }
    }
}
