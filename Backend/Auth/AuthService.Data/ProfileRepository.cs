using AuthService.Application;
using AuthService.Model;
using IdentityModel;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthService.Data
{
    public class ProfileRepository : IProfileRepository
    {
        protected UserManager<ApplicationUser> _userManager;

        public ProfileRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var roles = await _userManager.GetRolesAsync(user);
            var topRole = "";
            if (roles.Any(x => x == "Admin")) topRole = "Admin";
            else if (roles.Any(x => x == "User")) topRole = "User";

            var claims = new List<Claim>
             {
                 new Claim("UserRole", topRole),
                 new Claim(JwtClaimTypes.Id, user.Id),
             };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            context.IsActive = (user != null) && user.LockoutEnabled;
        }
    }
}
