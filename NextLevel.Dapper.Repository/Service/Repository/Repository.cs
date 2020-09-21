using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.QueryBuilder;
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
            var query = $"Delete from  {tableName}  where {param} =@{param}";
            var parameters = new DynamicParameters();
            parameters.Add(param, command);

            await WithConnection(async conn =>
                await conn.ExecuteAsync(query, parameters));
        }
        /// <summary>
        /// Check the record is exist in the table with @id
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> IsInDbAsync(string tableName, TKey id)
        {
            return await WithConnection(async con =>
                await con.QueryFirstOrDefaultAsync<TEntity>($"Select * from {tableName} where id = @Id", new { Id = id })) != null;
        }
        /// <summary>
        /// Check the record is exit in the table with @params
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="param"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<bool> IsInDbAsync(string tableName, string param, string key)
        {
            var query = $"Select * from  {tableName}  where {param} =@{param}";
            var parameters = new DynamicParameters();
            parameters.Add(param, key);

            return await WithConnection(async con =>
                       await con.QueryFirstOrDefaultAsync<TEntity>(query, parameters)) != null;
        }

        public async Task<IEnumerable<TEntity>> ExecuteReadQuery(string command)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>(command));
        }
        /// <summary>
        /// Execute query for non-object transactions
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task ExecuteWriteQuery(string command)
        {
            await WithConnection(async conn => await conn.ExecuteAsync(command));
        }
        //TODO:Need to get property names and then bind them to related entity and insert as dynamic param
        public async Task ExecuteWriteQuery(string command, TEntity entity)
        {
            await WithConnection(async conn => await conn.ExecuteAsync(command));
        }
        //TODO: G2 Create extensions for imp
        public async Task AddAsync(string table, TEntity entity)
        {
            var t =RepositoryExtensions<TEntity>.GetProperties(entity);
            var parameters = new DynamicParameters();
            var query = $"Insert into {table} values (";
            foreach (var VARIABLE in t)
            {
                parameters.Add(VARIABLE.Key, VARIABLE.Value);
                query += $"@{VARIABLE.Key},";
            }

            query = query.Substring(0, query.Length - 1);
            query += ")";
            await WithConnection(async conn =>
                await conn.ExecuteAsync(query, parameters));
        }
        //TODO: G2 Create extensions for imp
        public async Task UpdateAsync(string command, TEntity entity, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync(command, entity));
        }
        //TODO: G2 Create extensions for imp 
        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string command)
        {
            var result = await WithConnection(async conn => await conn.QueryMultipleAsync(command));
            return result;
        }
        
    }
    
}