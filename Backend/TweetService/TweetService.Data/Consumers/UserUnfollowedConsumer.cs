using MassTransit;
using MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetService.Data.Context;

namespace TweetService.Data.Consumers
{
    public class UserUnfollowedConsumer : IConsumer<IUserUnfollowedEvent>
    {
        private readonly TweetContext tweetRepo;

        public UserUnfollowedConsumer(TweetContext tweetRepo)
        {
            this.tweetRepo = tweetRepo;
        }

        public async Task Consume(ConsumeContext<IUserUnfollowedEvent> context)
        {
            var userUnfollowedEvent = context.Message;

            var user = await tweetRepo.Users.FindAsync(Guid.Parse(userUnfollowedEvent.UserId));

            // Does the user exist? remove follower
            // If the user doesn't exist, ignore the event
            if (user != null
                && user.Following.Any(f => f == Guid.Parse(userUnfollowedEvent.UnfollowedUserId)))
            {
                user.Following.Remove(user.Following.Single(f => f == Guid.Parse(userUnfollowedEvent.UnfollowedUserId)));
                await tweetRepo.SaveChangesAsync();
            }
        }
    }
}
