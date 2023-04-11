namespace MessagingModels
{
    public interface ITweetTweetedEvent
    {
        public Guid Id { get; set; }
        public Guid Tweeter { get; set; }

        public string Body { get; set; }
    }
}