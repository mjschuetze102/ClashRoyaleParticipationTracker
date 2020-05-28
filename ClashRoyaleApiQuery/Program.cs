using ClashRoyaleApiQuery.Api;
using ClashRoyaleApiQuery.Database;
using ClashRoyaleDataModel.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClashRoyaleApiQuery
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Load services needed by other parts of the application
                var config = services.GetRequiredService<IConfiguration>();
                var context = services.GetRequiredService<ClanParticipationContext>();

                // Parse configuration to complete app setup
                new ApiConnection(config["ClashRoyale:Api:Url"], config["ClashRoyale:Api:Key"]);
                string clanTag = config["ClashRoyale:ClanTag"];

                // Preload information from the database
                context.ClanMembers.Include(m => m.DonationRecords).Load();

                // Create the list of objects to gather information for
                List<IDataStore> _dataStores = new List<IDataStore> {
                    new DonationRecordStore(context, clanTag),
                    new WarLogStore(context, clanTag)
                };

                // Gather information from the API and store it in the database
                foreach (var dataStore in _dataStores)
                {
                    dataStore.StoreAll();
                }

                // Check to make sure information was stored correctly in the database
                foreach (var player in context.ClanMembers)
                {
                    Console.WriteLine($"{player.Name,15}, {(player.DonationRecords.Count > 0 ? player.DonationRecords.First().Donations : 0),3}, {player.WarParticipations.Count,2}");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(hostContext.Configuration);
                    services.AddDbContext<ClanParticipationContext>(options => options.UseSqlite(hostContext.Configuration.GetConnectionString("DefaultConnection")));
                });
    }
}
