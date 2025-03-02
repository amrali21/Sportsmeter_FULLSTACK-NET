﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRUD_Design;
using CRUD_Design_Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sportsmeter_frontend.Model.Services;
using System.Data;
using System.Linq.Expressions;

namespace CRUD_Design.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await GetAsync(id);
             _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(object id)
        {
            var entity = await GetAsync(id);
            return entity != null; 
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAsync(Expression<Func<T,bool>>expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<T> GetAsync(object id)
        {
            if (id is null)
                return null;

            var entity = await _context.Set<T>().FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity; 
        }

        public async Task<IDbTransaction> BeginTransaction(IsolationLevel isolation = IsolationLevel.Serializable)
        {
            var transaction = await _context.Database.BeginTransactionAsync(isolation);
            return transaction.GetDbTransaction();
        }

        public Object? ExecRawSql(string query)
        {
           return _context.RunInfos.FromSqlRaw(query).AsNoTracking().FirstOrDefault();
        }
    }
}
