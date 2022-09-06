using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // prod-env
            CreateHostBuilder(args).Build().Run();

            // ONLY for dev-env
            //var host = CreateHostBuilder(args).Build();

            //using var scope = host.Services.CreateScope();

            //var services = scope.ServiceProvider;

            //try
            //{
            //    var context = services.GetRequiredService<DataContext>();
            //    //await context.Database.MigrateAsync();
            //    await Seed.SeedData(context);
            //}
            //catch (Exception ex)
            //{
            //    var logger = services.GetRequiredService<ILogger<Program>>();
            //    logger.LogError(ex, "An error occured during migration");
            //}

            //host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
