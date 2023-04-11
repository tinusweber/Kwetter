

using Mapster;
using TweetService.Data;
using TweetService.DomainModel;

namespace TweetService.Application
{
    public class TweetApplication
    {
        private readonly ITweetRepository tweetRepository;

        public TweetApplication(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
        }

        public List<Tweet> GetFeedByUser(Guid userId)
        {
            var tweets = tweetRepository.GetFeedByUser(userId).ProjectToType<Tweet>().ToList();
            return tweets;
        }
        public async Task<Data.Models.Tweet> AddTweet(Tweet tweet)
        {
            _ = tweet.Body ?? throw new ArgumentNullException(nameof(tweet.Body));
            if (tweet.TweeterId == default)
                throw new ArgumentNullException(nameof(tweet.TweeterId));

            var obj = new Data.Models.Tweet()
            {
                Body = tweet.Body,
                TweeterId = tweet.TweeterId,
                ImageBase64 = tweet.ImageBase64

            };

            await this.tweetRepository.AddTweetAsync(obj);

            return obj;
        }

        public Task DeleteTweet(Guid guid)
        {
            return this.tweetRepository.DeleteTweetAsync(guid);
        }

        public List<Tweet> GetTweetByUser(Guid userId)
        {
            return this.tweetRepository.GetTweetsByUser(userId).ProjectToType<Tweet>().ToList();
        }

        public List<Tweet> GetTweets()
        {
            return this.tweetRepository.GetTweets().ProjectToType<Tweet>().ToList();
        }

        public Tweet GetTweetById(Guid tweetId)
        {
            var tweet = this.tweetRepository.GetTweets().SingleOrDefault(x => x.Id == tweetId);
            if (tweet == default)
            {
                throw new KeyNotFoundException($"Tweet {tweetId} not found");
            }
            return tweet.Adapt<Tweet>();
        }
    }
}