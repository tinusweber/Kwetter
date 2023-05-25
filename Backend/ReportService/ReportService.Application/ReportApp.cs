using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using ReportService.Data;
using ReportService.DomainModels;

namespace ReportService.Application
{
    public class ReportApp
    {
        private readonly IReportRepository _repository;

        public ReportApp(IReportRepository repository)
        {
            _repository = repository;
        }
        public List<Report> GetAll()
        {
            return this._repository.GetAll()
                .Select(x => new Report(Guid.Parse(x.Id), Guid.Parse(x.TweetId), Guid.Parse(x.ReporterUserId), x.Body, String.Empty)).ToList();

        }

        public async Task<Report> GetById(Guid id)
        {
            var data = await this._repository.GetById(id);

            return new Report(Guid.Parse(data.Id), Guid.Parse(data.TweetId), Guid.Parse(data.ReporterUserId), data.Body);
        }

        public async Task DeleteById(Guid id)
        {
            await this._repository.DeleteById(id);
        }

        public async Task<Data.Models.Report> Add(Report report)
        {
            return await this._repository.Add(report);
        }

        public async Task Update(Guid id, ReportStatus status, string closureMessage = "")
        {
            await this._repository.Update(id, status, closureMessage);
        }
    }
}