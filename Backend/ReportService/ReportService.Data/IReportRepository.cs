using ReportService.DomainModels;
using Report = ReportService.Data.Models.Report;

namespace ReportService.Data
{
    public interface IReportRepository
    {
        IQueryable<Report> GetAll();
        Task<Report> GetById(Guid id);
        Task DeleteById(Guid id);
        Task<Report> Add(ReportService.DomainModels.Report report);
        Task Update(Guid id, ReportStatus status, string closureMessage = "");
    }
}