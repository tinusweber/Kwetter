using ProfileService.Data;
using ProfileService.Data.Models;

namespace ProfileService.Application
{
    public class ProfileApp
    {
        private readonly IProfileRepository profileRepository;

        public ProfileApp(IProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }

        public Task FollowUser(Guid userId, Guid userToFollow)
        {
            return profileRepository.FollowUser(userId, userToFollow);
        }

        public ProfileData GetProfile(Guid userId)
        {
            return profileRepository.GetProfile(userId);
        }

        public Task UnfollowUser(Guid userId, Guid userToUnfollow)
        {
            return profileRepository.UnfollowUser(userId, userToUnfollow);
        }

        public Task<ProfileData> UpdateProfile(Guid userId, ProfileData profileInfo)
        {
            return profileRepository.UpdateProfile(userId, profileInfo);
        }

        public Task<List<ProfileData>> GetAll()
        {
            return profileRepository.GetAll();
        }
    }
}