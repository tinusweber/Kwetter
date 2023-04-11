using MassTransit;
using MessagingModels;
using Microsoft.EntityFrameworkCore;
using TweetService.Data.Context;
using TweetService.Data.Models;

namespace TweetService.Data
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetContext _repo;
        private readonly IPublishEndpoint publishEndpoint;

        public TweetRepository(TweetContext repo, MassTransit.IPublishEndpoint publishEndpoint)
        {
            this._repo = repo;
            this.publishEndpoint = publishEndpoint;
        }
        public async Task AddTweetAsync(Tweet tweet)
        {
            await _repo.AddAsync(tweet);
            await _repo.SaveChangesAsync();
            await publishEndpoint.Publish<ITweetTweetedEvent>(new
            {
                Id = tweet.Id,
                TweeterId = tweet.TweeterId,
                Body = tweet.Body
            });
            await publishEndpoint.Publish<IPostReadyForReviewEvent>(new
            {
                PostId = tweet.TweeterId,
                Body = tweet.Body
            });
        }

        public async Task DeleteTweetAsync(Guid guid)
        {
            var tweet = await _repo.Tweets.SingleOrDefaultAsync(x => x.Id == guid);
            if (tweet != default)
            {
                _repo.Tweets.Remove(_repo.Tweets.Single(x=> x.Id == guid));
                await _repo.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Tweet with id '{guid}' not found!");

            }
        }

        public IQueryable<Tweet> GetTweetsByUser(Guid userId)
        {
            return _repo.Tweets.Where(tweet => tweet.TweeterId == userId);
        }

        public IQueryable<Tweet> GetTweets()
        {
            return _repo.Tweets;
        }

        public IQueryable<Tweet> GetFeedByUser(Guid userId)
        {
            var user = _repo.Users.Find(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with id '{userId}' not found!");
            }

            var tweets = _repo.Tweets.Where(tweet => user.Following.Contains(tweet.TweeterId) || tweet.TweeterId == userId);
            return tweets.OrderByDescending(tweet => tweet.TweetedAt);
        }
    }
}