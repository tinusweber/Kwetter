using IdentityServer4.Models;
using IdentityServer4.Services;

namespace authService.Application
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
