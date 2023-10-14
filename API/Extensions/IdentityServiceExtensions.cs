using Core;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
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


        services.AddIdentityCore<AppUser>(opt =>
        {
            // add identityoptions here
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddSignInManager<SignInManager<AppUser>>();

        services.AddAuthentication();
        services.AddAuthorization();

        return services;
    }
}
