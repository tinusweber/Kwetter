using FluentValidation;
using Helpers;
using Mapster;
using TweetService.DomainModel.Validators;

namespace TweetService.DomainModel
{
    public class Tweet
    {
        public Tweet()
        {

        }
        public Tweet(Guid tweeterId, string body, string imageBase64)
        {
            this.Id = new Guid();
            TweeterId = tweeterId;
            Body = body;
            ImageBase64 = imageBase64;
            this.TweetedAt = DateTime.Now;

            this.Validate<Tweet, TweetValidator>();
        }
        public Guid Id { get; private set; }
        public Guid TweeterId { get; private set; }
        public string Body { get; private set; }
        public string ImageBase64 { get; private set; }
        public DateTime TweetedAt { get; private set; }

        public void SetTweeter(Guid tweeterId)
        {
            this.TweeterId = tweeterId;
        }
    }

}
