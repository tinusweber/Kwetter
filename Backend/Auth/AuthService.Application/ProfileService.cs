using AuthService.Model;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application
{
    public class ProfileService : IProfileService
    {
        protected IProfileRepository _repository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _repository = profileRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await _repository.GetProfileDataAsync(context);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            await _repository.IsActiveAsync(context);
        }
    }
}
