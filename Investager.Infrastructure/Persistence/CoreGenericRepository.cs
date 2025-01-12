﻿using Investager.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Investager.Infrastructure.Persistence;

public class CoreGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly InvestagerCoreContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public CoreGenericRepository(InvestagerCoreContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAll(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
    {
        var query = _dbSet.AsNoTracking();
        query = include(query);

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetByIdWithTracking(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        return entity ?? throw new Exception("Entity not found.");
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;
        query.AsNoTracking();

        query = query.Where(filter);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(
        Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
        IIncludableQueryable<TEntity, object>> include)
    {
        IQueryable<TEntity> query = _dbSet;
        query.AsNoTracking();

        query = include(query);
        query = query.Where(filter);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> filter, int take)
    {
        IQueryable<TEntity> query = _dbSet;

        query.AsNoTracking();
        query = query.Where(filter);
        query = query.Take(take);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Find(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        int take)
    {
        IQueryable<TEntity> query = _dbSet;

        query.AsNoTracking();
        query = query.Where(filter);
        query = orderBy(query);
        query = query.Take(take);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> FindWithTracking(Expression<Func<TEntity, bool>> filter)
    {
        IQueryable<TEntity> query = _dbSet;

        query = query.Where(filter);

        return await query.ToListAsync();
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
        var entityToDelete = _dbSet.Find(id);
        Delete(entityToDelete);
    }

    public void Delete(Expression<Func<TEntity, bool>> filter)
    {
        _dbSet.RemoveRange(_dbSet.Where(filter));
    }

    public void Delete(TEntity entityToDelete)
    {
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            _dbSet.Attach(entityToDelete);
        }

        _dbSet.Remove(entityToDelete);
    }
}
