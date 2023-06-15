using Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProfileService.Application;
using Mapster;
using MassTransit.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileApp profileApp;
        private readonly ILogger<ProfileController> logger;

        public ProfileController(ProfileApp profileApp, ILogger<ProfileController> logger)
        {
            this.profileApp = profileApp;
            this.logger = logger;
        }

        [HttpOptions]
        public async Task<List<Profile>> GetAll()
        {
            logger.LogInformation("GetAll method called.");

            var profile = profileApp.GetProfile(HttpContext.GetUserId());
            var all = await profileApp.GetAll();
            return all.Select(x => x.AsDto(profile.BlockedUsers.Contains(x.OwnerId))).ToList();
        }

        [HttpGet(Name = "GetProfile")]
        public Profile Get()
        {
            logger.LogInformation("Get method called.");

            var profile = profileApp.GetProfile(HttpContext.GetUserId());
            return profile.AsDto(false);
        }

        [HttpGet("{profileId}")]
        public Profile GetById(string profileId)
        {
            logger.LogInformation("GetById method called.");

            var profile = profileApp.GetProfile(Guid.Parse(profileId));
            var blocked = profileApp.GetProfile(HttpContext.GetUserId()).BlockedUsers.Any(x => x == Guid.Parse(profileId));
            return profile.AsDto(blocked);
        }

        [HttpPatch("{profileId}")]
        public async Task<IActionResult> UpdateProfile(string profileId, [FromBody] ProfileUpdate profile)
        {
            logger.LogInformation("UpdateProfile method called.");

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
