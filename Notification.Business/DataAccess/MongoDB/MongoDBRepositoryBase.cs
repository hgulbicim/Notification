using MongoDB.Bson;
using MongoDB.Driver;
using Notification.Entities;
using Notification.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notification.Business.DataAccess.MongoDB
{
    public class MongoDBRepositoryBase<TEntity> : IEntityRepository<TEntity>
       where TEntity : class, IEntity, new()
    {

        private MongoDbContext _mongoDbContext;

        public MongoDBRepositoryBase(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _mongoDbContext.DatabaseContext().GetCollection<TEntity>(entity.GetType().Name).InsertOneAsync(entity);

            return null;
        }

        public Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, bool isRetrievedFirstRow = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> order = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
