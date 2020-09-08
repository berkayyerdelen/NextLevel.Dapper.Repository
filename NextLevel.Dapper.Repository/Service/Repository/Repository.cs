using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public class Repository<TEntity, TKey> : BaseRepository, IRepository<TEntity, TKey>
    {
        public async Task AddAsync(string command, TEntity entity)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(string command)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>(command));
        }

        public async ValueTask<TEntity> GetByIdAsync(string command, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>(command, new { Id = id }));
        }

        public async Task<bool> IsInDbAsync(string command, TKey id)
        {
            var result = await WithConnection(async conn =>
                 await conn.ExecuteAsync(command, new { Id = id }));
            return result == 1;
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