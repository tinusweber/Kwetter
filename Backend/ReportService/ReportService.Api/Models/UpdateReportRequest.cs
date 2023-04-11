namespace ReportService.Api.Models
{
    public class UpdateReportRequest
    {
        public int ReportStatus { get; set; }
        public string Reason { get; set; }
    }
}
