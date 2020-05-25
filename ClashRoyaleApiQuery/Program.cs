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
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // Load services needed by other parts of the application
                var config = services.GetRequiredService<IConfiguration>();
                var context = services.GetRequiredService<ClanParticipationContext>();

                // Collect information from the API and store it within the database
                var data = new DataCollection(config);
                new DataStorage(data, context).StoreData();
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
