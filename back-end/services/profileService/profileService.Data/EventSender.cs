using MassTransit;
using MessagingModels;

namespace ProfileService.Data
{
    public class EventSender : IEventSender
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventSender(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task SendUserFollowedEvent(Guid user, Guid newFollowing)
        {
            await _publishEndpoint.Publish<IUserFollowedEvent>(new
            {
                UserId = user,
                NewFollowingId = newFollowing
            });
        }

        public Task SendUserUnfollowedEvent(Guid user, Guid unfollowing)
        {
            return _publishEndpoint.Publish<IUserUnfollowedEvent>(new
            {
                UserId = user,
                UnfollowingId = unfollowing
            });
        }
    }
}
