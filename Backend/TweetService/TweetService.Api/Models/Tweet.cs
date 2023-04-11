namespace TweetService.Api.Models
{
    public class Tweet
    {
        public string TweeterId { get; set; }
        public string Body { get; set; }
        public DateTime TweetedAt { get; set; }
        public string ImageBase64 { get; set; }
    }
}
