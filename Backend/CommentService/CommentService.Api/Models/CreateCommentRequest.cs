namespace CommentService.Api.Models
{
    public class CreateCommentRequest
    {
        public string TweetId { get; set; }
        public string Body { get; set; }
    }
}
