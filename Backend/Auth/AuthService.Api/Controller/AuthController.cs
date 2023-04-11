using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using AuthService.Api.Constants;
using AuthService.Model;
using AuthService.Model.Requests;
using MassTransit;
using MessagingModels;

namespace AuthService.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role, IPublishEndpoint publishEndpoint)
        {
            UserManager = userManager;
            RoleManager = role;
            this.publishEndpoint = publishEndpoint;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        [HttpPatch]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if (request.NewConfirm != request.New)
                return BadRequest("Passwords does not match");
            
            var user = await UserManager.GetUserAsync(ClaimsPrincipal.Current);
            await UserManager.ChangePasswordAsync(user, request.Current, request.New);

            return Ok("Password updated");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SignupRequest request)
        {
            var user = new ApplicationUser(request);
            var res = await UserManager.CreateAsync(user, request.Password);


            if (res.Succeeded)
            {
                await UserManager.AddToRoleAsync(user, Roles.User);
                await publishEndpoint.Publish<IUserRegisteredEvent>(new
                {
                    Id = user.Id,
                    Username = user.UserName
                });

                return Ok();
            }

            string errors = "";
            foreach (var error in res.Errors)
            {
                errors += error.Description + "\n";
            }

            return BadRequest(errors);
        }
        
        
    }
}
