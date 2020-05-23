using Microsoft.Extensions.Configuration;

namespace ClashRoyaleApiQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            ApiConnection api = new ApiConnection(config["clash_royale_api_url"], config["clash_royale_api_key"]);
            DataCollection data = new DataCollection(api, config["clash_royale_clan_tag"]);
            DataStorage storage = new DataStorage(data);
            storage.StoreData();
        }
    }
}
