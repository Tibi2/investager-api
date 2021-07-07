﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Investager.Core.Services
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<IEnumerable<TEntity>> GetAllTracked();

        Task<TEntity> GetByIdWithTracking(int id);

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, int take);

        Task<IEnumerable<TEntity>> FindWithTracking(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int take);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        void Delete(int id);

        void Delete(TEntity entityToDelete);
    }
}
