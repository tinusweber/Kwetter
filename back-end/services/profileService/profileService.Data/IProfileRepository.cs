using ProfileService.Data.Models;

namespace ProfileService.Data
{
    public interface IProfileRepository
    {
        public Task FollowUser(Guid userId, Guid userToFollow);
        public Task UnfollowUser(Guid userId, Guid userToUnfollow);
        public Task<List<Profile>> GetAll();
        public Profile GetProfile(Guid userId);

        public Task<Profile> UpdateProfile(Guid userId, Profile profileInfo);

    }
}
