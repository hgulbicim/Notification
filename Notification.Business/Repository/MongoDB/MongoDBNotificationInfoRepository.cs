using Notification.Business.DataAccess.MongoDB;
using Notification.Business.Repository.Abstract;
using Notification.Entities;

namespace Notification.Business.Repository.MongoDB
{
    public class MongoDBNotificationInfoRepository : MongoDBRepositoryBase<NotificationInfo>, INotificationInfoRepository
    {
        private readonly MongoDbContext _mongoDbContext;
        public MongoDBNotificationInfoRepository(MongoDbContext mongoDbContext) : base(mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
    }
}