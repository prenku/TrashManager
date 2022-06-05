using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrashManager.Data;

namespace TrashManager
{
    public static class WebHostExtensions
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<ApplicationDbContext>();

                // now we have the DbContext. Run migrations
                //context.Database.sSeedData().Migrate();

                // now that the database is up to date. Let's seed

#if DEBUG
                // if we are debugging, then let's run the test data seeder
                // alternatively, check against the environment to run this seeder
                new SeedData(context).Initialize(context);
#endif
            }

            return host;
        }
    }
}
