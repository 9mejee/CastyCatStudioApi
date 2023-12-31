﻿using DAL.Database;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace DAL.Repositeries
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly CatsyCatDbContext _dBContext;
        private readonly IConfiguration _configuration;

        public GenericService(CatsyCatDbContext dbContext, IConfiguration configuration)
        {
            this._dBContext = dbContext;
            this._configuration = configuration;
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dBContext.Set<T>().AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dBContext.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> AddWithSaveAsync(T entity)
        {
            try
            {
                await _dBContext.Set<T>().AddAsync(entity);
                await SaveAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> UpdateWithSaveAsync(T entity)
        {
            try
            {
                _dBContext.Entry(entity).State = EntityState.Modified;
                await SaveAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _dBContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteWithSaveAsync(T entity)
        {
            try
            {
                _dBContext.Set<T>().Remove(entity);
                await SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetAsync(int id)
        {

            try
            {
                return await _dBContext.FindAsync<T>(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {

            try
            {
                var query = _dBContext.Set<T>().AsQueryable<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                return await query.Where(predicate).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> IsAny(Expression<Func<T, bool>> predicate)
        {

            try
            {
                var query = _dBContext.Set<T>().AsQueryable<T>();


                return await query.AnyAsync(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<T> GetAll()
        {
            try
            {
                return _dBContext.Set<T>().AsQueryable<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IQueryable<T> GetAll(List<string> thanInclude, params Expression<Func<T, object>>[] includes)
        {
            try
            {
                var query = _dBContext.Set<T>().AsQueryable<T>();

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                if (thanInclude is not null && thanInclude.Any())
                {
                    foreach (var include in thanInclude)
                    {
                        query = query.Include(include);
                    }
                }

                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}