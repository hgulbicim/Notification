using MongoDB.Driver;
using Notification.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notification.Business.DataAccess.MongoDB
{
    public class MongoDbRepositoryBase<TEntity> : IEntityRepository<TEntity>
       where TEntity : class, IEntity, new()
    {

        private MongoDbContext _mongoDbContext;

        public MongoDbRepositoryBase(MongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await _mongoDbContext.DatabaseContext().GetCollection<TEntity>(typeof(TEntity).Name).InsertOneAsync(entity);

            return entity;
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            var entity = await _mongoDbContext.DatabaseContext().GetCollection<TEntity>(typeof(TEntity).Name).FindAsync(filter);

            return entity.FirstOrDefault();
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