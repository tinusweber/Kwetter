using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext context)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(context.Request.Headers["Authorization"].ToString().Replace("Bearer ", String.Empty));
            var claims = jwtSecurityToken.Claims.ToList();
            return Guid.Parse(claims.Single(x => x.Type == "id").Value);
        }
        public static Role GetUserRole(this HttpContext context)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(context.Request.Headers["Authorization"].ToString().Replace("Bearer ", String.Empty));
            var claims = jwtSecurityToken.Claims.ToList();
            return Enum.Parse<Role>(claims.Single(x => x.Type == "UserRole").Value);
        }
    }

    public enum Role
    {
        User,
        Admin
    }
}
