using ClashRoyaleApiQuery.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClashRoyaleApiQuery
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<IEnumerable<WarLog>> ConnectToAPI(string apiKey, string clanTag)
        {
            client.BaseAddress = new Uri("https://api.clashroyale.com/v1/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);

            HttpResponseMessage response = await client.GetAsync($"clans/%23{clanTag}/warlog");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                WarLogs warlogs = JsonConvert.DeserializeObject<WarLogs>(content);
                return warlogs.Items;
            }

            return null;
        }

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            IEnumerable<WarLog> warlogs = ConnectToAPI(config["clash_royale_api_key"], config["clash_royale_clan_tag"]).GetAwaiter().GetResult();

            foreach (WarLog warlog in warlogs)
            {
                Console.WriteLine(warlog.CreatedDate);
            }
        }
    }
}
