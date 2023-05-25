using MongoDB.Driver;
using ReportService.Data.Models;

namespace ReportService.Data.Context
{
    public class ReportContext
    {
        private readonly IMongoDatabase _database;

        public IMongoCollection<Report> Reports => _database.GetCollection<Report>("Reports");

        public ReportContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
    }
}
