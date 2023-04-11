namespace ReportService.DomainModels
{
    public class Report
    {
        public Report(Guid id, Guid tweetId, Guid reporterGuid, string reason, string closureMessage = "")
        {
            this.Id = id;
            this.Status = ReportStatus.AWAIT_RESPONSE;
            this.TweetId = tweetId;
            this.ReporterUserId = reporterGuid;
            this.Body = reason;

            this.ClosureMessage = closureMessage;
        }

        public Guid Id { get; private set; }
        public Guid TweetId { get; private set; }
        public Guid ReporterUserId { get; private set; }
        public string Body { get; private set; }
        public ReportStatus Status { get; private set; }
        public string ClosureMessage { get; private set; }
    }

    public enum ReportStatus
    {
        AWAIT_RESPONSE = 0,
        CLOSED = 1,
        HANDLED = 2
    }
}