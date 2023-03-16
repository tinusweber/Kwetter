using Helpers;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application;
using MassTransit.Initializers;

namespace ProfileService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileApp profileApp;

        public ProfileController(ProfileApp profileApp)
        {
            this.profileApp = profileApp;
        }

        [HttpOptions]
        public async Task<List<Profile>> GetAll()
        {
            var profile = profileApp.GetProfile(HttpContext.GetUserId());
            var all = await profileApp.GetAll();
            return all.Select(x => x.AsDto(profile.BlockedUsers.Contains(x.OwnerId))).ToList();
        }
        [HttpGet(Name = "GetProfile")]
        public Profile Get()
        {
            var profile = profileApp.GetProfile(HttpContext.GetUserId());
            return profile.AsDto(false);
        }

        [HttpGet("{profileId}")]
        public Profile GetById(string profileId)
        {
            var profile = profileApp.GetProfile(Guid.Parse(profileId));
            var blocked = profileApp.GetProfile(HttpContext.GetUserId()).BlockedUsers.Any(x => x == Guid.Parse(profileId));
            return profile.AsDto(blocked);
        }

        [HttpPatch("{profileId}")]
        public async Task<IActionResult> UpdateProfile(string profileId, [FromBody] ProfileUpdate profile)
        {
            var profileDto = await profileApp.UpdateProfile(Guid.Parse(profileId), new Data.Models.Profile()
            {
                Biography = profile.Biography,
                ProfilePictureBase64 = profile.ProfilePictureBase64,
                Username = profile.DisplayName,
            });

            return Ok();
        }
    }
}