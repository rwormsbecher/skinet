using System.Security.Claims;
using Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class UserManagerExtensions
{
    public static async Task<AppUser> FindUserByClaimsPrincipalWithAddresss(this UserManager<AppUser> usermanager, ClaimsPrincipal user)
    {
        var email = user.FindFirstValue(ClaimTypes.Email);

        return await usermanager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
    }


    public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> usermanager, ClaimsPrincipal user)
    {
        return await usermanager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
    }
}
