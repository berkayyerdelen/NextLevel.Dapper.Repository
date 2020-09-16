using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public interface IRepository<TEntity, in TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields, string command);
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields);
        Task<IEnumerable<TEntity>> GetAllAsync(string tableName);
        ValueTask<TEntity> GetByIdAsync(string tableName, TKey id);
        ValueTask<TEntity> GetByIdAsync(string tableName, string fields, TKey id);
        Task AddAsync(string command, TEntity entity);
        Task UpdateAsync(string command, TEntity entity, TKey id);
        Task RemoveAsync(string command, TKey id);
        Task<bool> IsInDbAsync(string tableName, TKey id);
        Task<bool> IsInDbAsync(string tableName, string param, string key);
        Task ExecuteQuery(string command, TEntity entity);
        Task<SqlMapper.GridReader> QueryMultipleAsync(string command);
    }
}