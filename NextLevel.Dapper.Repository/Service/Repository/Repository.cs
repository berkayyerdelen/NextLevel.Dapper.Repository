using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public class Repository<TEntity, TKey> : BaseRepository, IRepository<TEntity, TKey>
    {
        /// <summary>
        /// Return the Enumrable List by via @tableName, @fields, @command
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields, string command)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select {fields} from {tableName} {command}"));
        }
        /// <summary>
        /// Return the Enumrable List by via @tableName, @fields, @command
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select {fields} from {tableName}"));
        }
        /// <summary>
        /// Return the Enumrable List by via @tableName
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select * from {tableName}"));
        }

        /// <summary>
        /// Get entity by via @tableName, @id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> GetByIdAsync(string tableName, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>($"Select * from {tableName}", new { Id = id }));
        }
        /// <summary>
        /// Get entity by via @tableName, @id, @fields
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async ValueTask<TEntity> GetByIdAsync(string tableName, string fields, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>($"Select {fields} from {tableName}", new { Id = id }));
        }
        public async Task<bool> IsInDbAsync(string tableName, TKey id)
        {
            return await WithConnection(async con =>
                await con.QuerySingleAsync<TEntity>($"Select * from {tableName} where id = @Id", new { Id = id })) != null;
        }
        /// <summary>
        /// Delete entity by via @tableName, @id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string tableName, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync($"Delete from {tableName} where id = @Id", new { Id = id }));
        }
        /// <summary>
        /// Delete entity by via @tableName, @id, @command
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task RemoveAsync(string tableName, string param, string command)
        {
            //TODO: Make sure that where condition should be same as param in annoymous class
            await WithConnection(async conn =>
                await conn.ExecuteAsync($"Delete from {tableName} where {command}", new { param = param }));
        }

        public async Task<bool> IsInDbAsync(string tableName, string param, string key)
        {

            return await WithConnection(async con =>
                       await con.QuerySingleAsync<TEntity>($"Select * from {tableName} where name= @name", new
                       {
                           name = "asdas"
                       }
                           )) != null;

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
        public async Task UpdateAsync(string command, TEntity entity, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }
    }
}