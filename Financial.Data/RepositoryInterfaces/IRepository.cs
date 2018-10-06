using Financial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Financial.Data.RepositoryInterfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllActive();

        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);

        bool Exists(int id);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        //void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
