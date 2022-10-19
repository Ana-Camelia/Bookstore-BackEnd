using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bookstore.DataAccess.Persistence
{
    public static class AutoMigration
    {
        public static IServiceProvider AddAutoMigration(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                if (dataContext.Database.IsRelational())
                {
                    dataContext.Database.Migrate();
                }
            }

            return serviceProvider;
        }
    }
}
