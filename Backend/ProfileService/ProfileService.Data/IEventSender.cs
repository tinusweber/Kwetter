namespace ProfileService.Data
{
    public interface IEventSender
    {
        Task SendUserFollowedEvent(Guid user, Guid newFollowing);
        Task SendUserUnfollowedEvent(Guid user, Guid unfollowing);

    }
}
