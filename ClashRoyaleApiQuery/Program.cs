using ClashRoyaleDataModel.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClashRoyaleApiQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<ClanParticipationContext>(options => {
                    options.UseSqlite(config.GetConnectionString("DefaultConnection"));
                    options.EnableSensitiveDataLogging(true);
                })
                .BuildServiceProvider();

            var context = serviceProvider.GetRequiredService<ClanParticipationContext>();

            ApiConnection api = new ApiConnection(config["clash_royale_api_url"], config["clash_royale_api_key"]);
            DataCollection data = new DataCollection(api, config["clash_royale_clan_tag"]);
            DataStorage storage = new DataStorage(data, context);
            storage.StoreData();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();

                    services.AddDbContext<ClanParticipationContext>(options => options.UseSqlite(config.GetConnectionString("DefaultConnection")));
                });
    }
}
