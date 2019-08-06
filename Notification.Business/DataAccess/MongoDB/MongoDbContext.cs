using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Notification.Entities.Configuration;

namespace Notification.Business.DataAccess.MongoDB
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _databaseContext = null;
        public MongoDbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _databaseContext = client.GetDatabase(settings.Value.Database);
        }

        public IMongoDatabase DatabaseContext()
        {
            return _databaseContext;
        } 
    }
}