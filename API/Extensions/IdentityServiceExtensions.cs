using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIDentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppIdentityDbContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("IdentityConnection"));
        });

        return services;
    }
}
