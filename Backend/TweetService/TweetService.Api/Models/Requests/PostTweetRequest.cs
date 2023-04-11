namespace TweetService.Api.Models.Requests
{
    public class PostTweetRequest
    {
        public string Body { get; set; }
        public string? ImageBase64 { get; set; } = "";
    }
}
