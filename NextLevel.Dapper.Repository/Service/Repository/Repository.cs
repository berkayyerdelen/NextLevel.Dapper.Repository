using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public class Repository<TEntity, TKey> : BaseRepository, IRepository<TEntity, TKey>
    {

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields, string command)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select {fields} from {tableName} {command}"));
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select {fields} from {tableName}"));
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select * from {tableName}"));
        }

        public async ValueTask<TEntity> GetByIdAsync(string tableName, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>($"Select * from {tableName}", new { Id = id }));
        }
        public async ValueTask<TEntity> GetByIdAsync(string tableName, string fields, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>($"Select {fields} from {tableName}", new { Id = id }));
        }
        public async Task<bool> IsInDbAsync(string command, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.ExecuteAsync(command, new { Id = id })) == 1;
        }
        public async Task ExecuteQuery(string command, TEntity entity)
        {
            await WithConnection(async conn => await conn.ExecuteAsync(command));
        }
        public async Task AddAsync(string command, TEntity entity)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string command)
        {
            var result = await WithConnection(async conn => await conn.QueryMultipleAsync(command));
            return result;
        }

        public async Task RemoveAsync(string command, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, new { Id = id }));
        }

        public async Task UpdateAsync(string command, TEntity entity, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }
    }
}