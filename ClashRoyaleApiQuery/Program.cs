using ClashRoyaleApiQuery.Api;
using ClashRoyaleApiQuery.Database;
using ClashRoyaleDataModel.Configuration;
using ClashRoyaleDataModel.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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
                var config = services.GetRequiredService<IOptions<ClashRoyaleConfiguration>>().Value;
                var context = services.GetRequiredService<ClanParticipationContext>();

                // Parse configuration to complete app setup
                new ApiConnection(config.Api);
                string clanTag = config.ClanTag;

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
                    services.AddOptions();

                    var section = hostContext.Configuration.GetSection("ClashRoyale");
                    services.Configure<ClashRoyaleConfiguration>(section);

                    services.AddDbContext<ClanParticipationContext>(options => options.UseSqlite(hostContext.Configuration.GetConnectionString("DefaultConnection")));
                });
    }
}
