using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Resources.Models
{
    public static class GanttInitializerExtension
    {
        public static IHost InitializeDatabase(this IHost webHost)
        {
            var serviceScopeFactory =
             (IServiceScopeFactory?)webHost.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = serviceScopeFactory!.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<AppDBContext>();
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                GanttSeeder.Seed(dbContext);
            }

            return webHost;
        }
    }
}
