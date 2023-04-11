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
    public class UserFollowedConsumer : IConsumer<IUserFollowedEvent>
    {
        private readonly TweetContext tweetRepo;

        public UserFollowedConsumer(TweetContext tweetRepo)
        {
            this.tweetRepo = tweetRepo;
        }
        public async Task Consume(ConsumeContext<IUserFollowedEvent> context)
        {
            var userFollowedEvent = context.Message;

            var user = await tweetRepo.Users.FindAsync(Guid.Parse(userFollowedEvent.UserId));

            // Does the user exist? Add follower
            if (user != null)
            {
                user.Following.Add(Guid.Parse(userFollowedEvent.FollowedUserId));

            }
            else
            {
                // User does not exist, add it.
                await tweetRepo.Users.AddAsync(new Models.User()
                {
                    UserId = Guid.Parse(userFollowedEvent.UserId),
                    Following = new List<Guid>() { Guid.Parse(userFollowedEvent.FollowedUserId) }
                });
            }

            await tweetRepo.SaveChangesAsync();

        }
    }
}
