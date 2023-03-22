using ProfileService.Data.Models;

namespace ProfileService.Data
{
    public interface IProfileRepository
    {
        public Task FollowUser(Guid userId, Guid userToFollow);
        public Task UnfollowUser(Guid userId, Guid userToUnfollow);
        public Task<List<ProfileData>> GetAll();
        public ProfileData GetProfile(Guid userId);

        public Task<ProfileData> UpdateProfile(Guid userId, ProfileData profileInfo);

    }
}
