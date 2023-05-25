using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReportService.DomainModels;

namespace ReportService.Data.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string TweetId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string ReporterUserId { get; set; }

        public string Body { get; set; }

        public ReportStatus Status { get; set; }

        public string ClosureMessage { get; set; }
    }
}
