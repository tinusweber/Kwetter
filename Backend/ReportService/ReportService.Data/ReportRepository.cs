using Mapster;
using MongoDB.Driver;
using ReportService.Data.Context;
using ReportService.DomainModels;
using Report = ReportService.Data.Models.Report;

namespace ReportService.Data
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<Report> _collection;

        public ReportRepository(ReportContext context)
        {
            _collection = context.Reports;
        }

        public IQueryable<Report> GetAll()
        {
            return _collection.AsQueryable();
        }

        public async Task<Report> GetById(Guid id)
        {
            var filter = Builders<Report>.Filter.Eq(x => x.Id, id.ToString());
            var result = await _collection.Find(filter).SingleOrDefaultAsync();

            if (result == null)
            {
                throw new KeyNotFoundException($"Report {id} was not found!");
            }

            return result;
        }

        public async Task DeleteById(Guid id)
        {
            var filter = Builders<Report>.Filter.Eq(x => x.Id, id.ToString());
            var result = await _collection.DeleteOneAsync(filter);

            if (result.DeletedCount == 0)
            {
                throw new KeyNotFoundException($"Report {id} not found!");
            }
        }

        public async Task<Report> Add(DomainModels.Report report)
        {
            var dataModel = report.Adapt<Report>();
            await _collection.InsertOneAsync(dataModel);
            return dataModel;
        }

        public async Task Update(Guid id, ReportStatus status, string closureMessage = "")
        {
            var filter = Builders<Report>.Filter.Eq(x => x.Id, id.ToString());
            var update = Builders<Report>.Update
                .Set(x => x.Status, status)
                .Set(x => x.ClosureMessage, closureMessage);

            await _collection.UpdateOneAsync(filter, update);
        }
        public void Initialize()
        {
            // Add any necessary initialization steps here
            // For example, creating indexes or setting up initial data

            // Create an index on the Id field for faster lookup
            var indexKeys = Builders<Report>.IndexKeys.Ascending(x => x.Id);
            var indexOptions = new CreateIndexOptions { Unique = true };
            var indexModel = new CreateIndexModel<Report>(indexKeys, indexOptions);
            _collection.Indexes.CreateOne(indexModel);

            // Add additional initialization steps as needed
        }
    }
}
