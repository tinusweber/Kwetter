using Microsoft.EntityFrameworkCore;
using ReportService.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using ReportService.DomainModels;
using Report = ReportService.Data.Models.Report;

namespace ReportService.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly ReportContext _context;

        public ReportRepository(ReportContext context)
        {
            _context = context;
        }

        public IQueryable<Report> GetAll()
        {
            return this._context.Reports.AsQueryable();
        }

        public async Task<Report> GetById(Guid id)
        {
            var result =  await this._context.Reports.SingleOrDefaultAsync(x => x.Id == id);

            if (result == default)
            {
                throw new KeyNotFoundException($"Report {id} was not found!");
            }
            return result;
        }

        public async Task DeleteById(Guid id)
        {
            var example = await this._context.Reports.SingleOrDefaultAsync(x => x.Id == id);
            if (example == default)
                throw new KeyNotFoundException($"Report {id} not found!");

            this._context.Reports.Remove(example);
            await this._context.SaveChangesAsync();
        }

        public async Task<Report> Add(DomainModels.Report report)
        {
            var dataModel = report.Adapt<Report>();

            var response = await _context.Reports.AddAsync(dataModel);
            await _context.SaveChangesAsync();
            return response.Entity;
        }

        public async Task Update(Guid id, ReportStatus status, string closureMessage = "")
        {
            var report = await this.GetById(id);
            report.Status = status;
            if (!string.IsNullOrWhiteSpace(closureMessage))
                report.ClosureMessage = closureMessage;

            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
    }
}