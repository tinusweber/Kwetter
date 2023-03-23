using IdentityServer4.Models;

namespace authService.Application
{
    public interface IProfileRepository
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context);
        public Task IsActiveAsync(IsActiveContext context);
    }
}
