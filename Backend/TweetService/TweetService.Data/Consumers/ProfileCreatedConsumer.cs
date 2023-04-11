using MassTransit;
using MessagingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetService.Data.Context;
using TweetService.Data.Models;

namespace TweetService.Data.Consumers
{
    public class ProfileCreatedConsumer : IConsumer<IProfileCreatedEvent>
    {
        private readonly TweetContext tweetRepo;

        public ProfileCreatedConsumer(TweetContext tweetRepo)
        {
            this.tweetRepo = tweetRepo;
        }
        public async Task Consume(ConsumeContext<IProfileCreatedEvent> context)
        {

            await tweetRepo.Users.AddAsync(new User()
            {
                Following = new List<Guid>(),
                UserId = Guid.Parse(context.Message.Id)
            });

            await tweetRepo.SaveChangesAsync();
        }
    }
}
