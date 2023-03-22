using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Data.Context;
using ProfileService.Data.Models;

namespace ProfileService.Data
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ProfileContext _repo;
        private readonly IEventSender publishEndpoint;

        public ProfileRepository(ProfileContext repo, IEventSender publishEndpoint)
        {
            this._repo = repo;
            this.publishEndpoint = publishEndpoint;
        }

        public async Task FollowUser(Guid userId, Guid userToFollow)
        {
            var profile = _repo.Profiles.Single(x => x.OwnerId == userId);
            profile.FollowingUsers.Add(userToFollow);
            await _repo.SaveChangesAsync();

            await publishEndpoint.SendUserFollowedEvent(userId, userToFollow);
        }

        public Task<List<ProfileData>> GetAll()
        {
            return _repo.Profiles.ToListAsync();
        }

        public ProfileData GetProfile(Guid userId)
        {
            return _repo.Profiles.Single(x => x.OwnerId == userId);
        }

        public async Task UnfollowUser(Guid userId, Guid userToUnfollow)
        {
            var profile = _repo.Profiles.Single(x => x.OwnerId == userId);
            profile.FollowingUsers.Remove(userToUnfollow);
            await _repo.SaveChangesAsync();

            await publishEndpoint.SendUserUnfollowedEvent(userId, userToUnfollow);
        }

        public async Task UnBlockUser(Guid userId, Guid userToUnblock)
        {
            var profile = _repo.Profiles.Single(x => x.OwnerId == userId);
            profile.BlockedUsers.Remove(userToUnblock);
            await _repo.SaveChangesAsync();
        }

        public async Task BlockUser(Guid userId, Guid userToBlock)
        {
            var profile = _repo.Profiles.Single(x => x.OwnerId == userId);
            profile.BlockedUsers.Add(userToBlock);
            await _repo.SaveChangesAsync();
        }

        public async Task<ProfileData> UpdateProfile(Guid userId, ProfileData profileInfo)
        {
            profileInfo.OwnerId = userId;

            var profile = _repo.Profiles.Update(profileInfo);
            await _repo.SaveChangesAsync();

            return profile.Entity;
        }
    }
}