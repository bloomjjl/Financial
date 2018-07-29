using Financial.Core;
using Financial.Data.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Financial.Business.Tests.Fakes.Repositories
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private List<TEntity> _entities;

        public InMemoryRepository(IEnumerable<TEntity> entities)
        {
            _entities = entities as List<TEntity>;
        }

        public TEntity Get(int id)
        {
            return _entities.FirstOrDefault(r => r.Id == id);
        }

        public TEntity GetActive(int id)
        {
            return _entities.Where(r => r.IsActive).FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> GetAllActive()
        {
            return _entities.Where(r => r.IsActive).ToList();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AsQueryable().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.AsQueryable().Where(predicate);
        }

        public bool Exists(int id)
        {
            return _entities.FirstOrDefault(r => r.Id == id) == null ? false : true;
        }

        public void Add(TEntity entity)
        {
            // database sets following values
            entity.Id = _entities.Count + 1;

            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            TEntity oldEntity = _entities.FirstOrDefault(r => r.Id == entity.Id);
            _entities.Remove(oldEntity);
            _entities.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _entities.Remove(entity);
            }
        }

    }
}
