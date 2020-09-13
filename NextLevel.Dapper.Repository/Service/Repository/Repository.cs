using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public class Repository<TEntity, TKey> : BaseRepository, IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Inserting entity to related table
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(string command, TEntity entity)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }
        /// <summary>
        /// Returning list of entity
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync(string command)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>(command));
        }
        /// <summary>
        /// Geting record by Id
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> GetByIdAsync(string command, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>(command, new { Id = id }));
        }
        /// <summary>
        /// Is in Db
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsInDbAsync(string command, TKey id)
        {
            return await WithConnection(async conn =>
                 await conn.ExecuteAsync(command, new { Id = id })) == 1;
        }
        /// <summary>
        /// Delete, Truncate Table etc
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task ExecuteQuery(string command, TEntity entity)
        {
            await WithConnection(async conn => await conn.ExecuteAsync(command));
        }

        /// <summary>
        /// Multiple Query
        /// </summary>
        /// In return you have to use DTO to seperate the result 
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string command)
        {
            var result = await WithConnection(async conn => await conn.QueryMultipleAsync(command));
            return result;
        }
        /// <summary>
        /// Deeleting record by via Id
        /// </summary>
        /// <param name="command"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string command, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, new { Id = id }));
        }
        /// <summary>
        /// Updating record by via Id
        /// </summary>
        /// <param name="command"></param>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task UpdateAsync(string command, TEntity entity, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }
    }
}