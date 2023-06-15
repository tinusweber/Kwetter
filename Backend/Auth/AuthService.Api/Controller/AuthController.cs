using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> role, IPublishEndpoint publishEndpoint, ILogger<AuthController> logger)
        {
            UserManager = userManager;
            RoleManager = role;
            this.publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        public UserManager<ApplicationUser> UserManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        [HttpPatch]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
        {
            if (request.NewConfirm != request.New)
            {
                _logger.LogWarning("Passwords do not match");
                return BadRequest("Passwords do not match");
            }

            var user = await UserManager.GetUserAsync(ClaimsPrincipal.Current);
            await UserManager.ChangePasswordAsync(user, request.Current, request.New);

            _logger.LogInformation("Password updated successfully");
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

            _logger.LogError(errors);
            return BadRequest(errors);
        }
    }
}
