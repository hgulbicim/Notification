using Notification.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Notification.Business.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        Task<T> Add(T entity);

        Task<bool> Update(T entity);

        Task<T> Get(Expression<Func<T, bool>> filter, bool isRetrievedFirstRow = true);

        Task<List<T>> GetList(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> order = null);
    }
}