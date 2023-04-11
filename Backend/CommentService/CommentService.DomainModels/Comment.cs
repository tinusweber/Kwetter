namespace CommentService.DomainModels
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public Guid TweetId { get; private set; }
        public Guid CommenterId { get; private set; }
        public string Body { get; private set; }
    }
}