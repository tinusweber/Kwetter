namespace MessagingModels;

public interface IPostReadyForReviewEvent
{
    public Guid PostId { get; set; }
    public string Body { get; set; }
}