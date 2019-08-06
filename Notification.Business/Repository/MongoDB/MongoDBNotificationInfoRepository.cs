using Notification.Business.DataAccess.MongoDB;
using Notification.Business.Repository.Abstract;
using Notification.Entities;

namespace Notification.Business.Repository.MongoDB
{
    public class MongoDbNotificationInfoRepository : MongoDbRepositoryBase<NotificationInfo>, INotificationInfoRepository
    {
        private readonly MongoDbContext _mongoDbContext;
        public MongoDbNotificationInfoRepository(MongoDbContext mongoDbContext) : base(mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
    }
}