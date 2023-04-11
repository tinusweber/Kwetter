using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.DomainModels;

namespace ReportService.Data.Models
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TweetId { get; set; }
        public Guid ReporterUserId { get; set; }
        public string Body { get; set; }
        public ReportStatus Status { get; set; }
        public string ClosureMessage { get; set; }
    }
}