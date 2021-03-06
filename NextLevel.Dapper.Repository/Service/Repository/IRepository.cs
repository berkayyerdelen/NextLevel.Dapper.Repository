﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public interface IRepository<TEntity, in TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields,string command, string param);
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields);
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName);
        ValueTask<TEntity> GetByIdAsync(string tableName, TKey id);
        ValueTask<TEntity> GetByIdAsync(string tableName, string fields, TKey id);
        Task RemoveAsync(string tableName, TKey id);
        Task RemoveAsync(string tableName, string param, string key);
        Task<bool> IsInDbAsync(string tableName, TKey id);
        Task<bool> IsInDbAsync(string tableName, string param, string key);
        Task<IEnumerable<TEntity>> ExecuteReadQuery(string command);
        Task ExecuteWriteQuery(string command);
        Task UpdateAsync(string table, TEntity entity, TKey id);
        Task UpdateAsync(string table, TEntity entity);
        Task AddAsync(string command, TEntity entity);
    }
}