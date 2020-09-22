using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public class Repository<TEntity, TKey> : BaseRepository, IRepository<TEntity, TKey>
    {
      
        /// <returns></returns>
        /// <summary>
        ///      Executes the GetAllAsync method for retrieve list of entity
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="fields">Fields</param>
        /// <param name="command">Command query such as Where condition</param>
        /// <param name="param">Parameter</param>
        /// <returns>List of entity fields according to the conditions</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields, string command, string param)
        {
            var query = $"Select {fields} from {tableName} where {command}= @{command}";
            var parameters = new DynamicParameters();
            parameters.Add(command, param);
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>(query, parameters));
        }
        /// <summary>
        ///     Executes the GetAllAsync method for retrieve list of entity
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="fields">Fields</param>
        /// <returns>List of entity fields</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName, string fields)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select {fields} from {tableName}"));
        }
        /// <summary>
        ///     Executes the GetAllAsync method for retrieve list of entity
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <returns>List of entity</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(string tableName)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>($"Select * from {tableName}"));
        }

        /// <summary>
        ///     Executes the GetByIdAsync method for retrieving the related record in data
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="id">Generic Id</param>
        /// <returns>Entity according to the where clause</returns>
        public virtual async ValueTask<TEntity> GetByIdAsync(string tableName, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>($"Select * from {tableName}", new { Id = id }));
        }
        /// <summary>
        ///     Executes the GetByIdAsync method for retrieving the related record in data
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="id">Generic Id</param>
        /// <param name="fields">Fields</param>
        /// <returns>Entity fields according to the where clause</returns>
        public virtual async ValueTask<TEntity> GetByIdAsync(string tableName, string fields, TKey id)
        {
            return await WithConnection(async conn =>
                await conn.QueryFirstOrDefaultAsync<TEntity>($"Select {fields} from {tableName}", new { Id = id }));
        }
        /// <summary>
        ///     Executes the RemoveAsync method for removing related record in data
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="id">Generic Id</param>
        public virtual async Task RemoveAsync(string tableName, TKey id)
        {
            await WithConnection(async conn =>
                await conn.ExecuteAsync($"Delete from {tableName} where id = @Id", new { Id = id }));
        }
        /// <summary>
        ///     Executes the RemoveAsync method for removing related record in data 
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="param">Parameter</param>
        /// <param name="command">Command</param>
        public virtual async Task RemoveAsync(string tableName, string param, string key)
        {
            var query = $"Delete from  {tableName}  where {param} =@{param}";
            var parameters = new DynamicParameters();
            parameters.Add(param, key);

            await WithConnection(async conn =>
                await conn.ExecuteAsync(query, parameters));
        }
        /// <summary>
        ///      Executes the IsInDbAsync method for the check data is exist or not
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="id">Generic Id</param>
        /// <returns>True or False in case of data exist or not</returns>
        public virtual async Task<bool> IsInDbAsync(string tableName, TKey id)
        {
            return await WithConnection(async con =>
                await con.QueryFirstOrDefaultAsync<TEntity>($"Select * from {tableName} where id = @Id", new { Id = id })) != null;
        }
        /// <summary>
        ///     Executes the IsInDbAsync method for the check data is exist or not
        /// </summary>
        /// <param name="tableName">Table Name</param>
        /// <param name="param">Parameter</param>
        /// <param name="key">Generic Key</param>
        /// <returns>True or False in case of data exist or not</returns>
        public virtual async Task<bool> IsInDbAsync(string tableName, string param, string key)
        {
            var query = $"Select * from  {tableName}  where {param} =@{param}";
            var parameters = new DynamicParameters();
            parameters.Add(param, key);

            return await WithConnection(async con =>
                       await con.QueryFirstOrDefaultAsync<TEntity>(query, parameters)) != null;
        }
        /// <summary>
        ///     Executes the ExecuteReadQuery method for generic select queries
        /// </summary>
        /// <param name="command">Full Command query</param>
        /// <returns>List of Entity</returns>
        public virtual async Task<IEnumerable<TEntity>> ExecuteReadQuery(string command)
        {
            return await WithConnection(async conn =>
                await conn.QueryAsync<TEntity>(command));
        }
        /// <summary>
        ///     Executes the ExecuteWriteQuery method for non-returning queries
        /// </summary>
        /// <param name="command">Full Command Query</param>
        public virtual async Task ExecuteWriteQuery(string command)
        {
            await WithConnection(async conn => await conn.ExecuteAsync(command));
        }
        /// <summary>
        ///     Executes the AddAsync method for inserting record into related table
        /// </summary>
        /// <param name="table">Table Name</param>
        /// <param name="entity">Entity</param>
        public virtual async Task AddAsync(string table, TEntity entity)
        {
            var parameters = new DynamicParameters();
            var query = $"Insert into {table} values (";
            foreach (var val in RepositoryExtensions<TEntity>.GetProperties(entity))
            {
                parameters.Add(val.Key, val.Value);
                query += $"@{val.Key},";
            }

            query = query.Substring(0, query.Length - 1);
            query += ");";
            await WithConnection(async conn =>
                await conn.ExecuteAsync(query, parameters));
        }
        /// <summary>
        ///     Executes the UpdateAsync method for updating related record
        /// </summary>
        /// <param name="table">Table Name</param>
        /// <param name="entity">Entity</param>
        /// <param name="id">Generic Id</param>
        public virtual async Task UpdateAsync(string table, TEntity entity, TKey id)
        {
            var parameters = new DynamicParameters();
            var query = $"Update {table} set ";
            foreach (var val in RepositoryExtensions<TEntity>.GetProperties(entity))
            {
                parameters.Add(val.Key, val.Value);
                query += $"{val.Key} = @{val.Key},";
            }

            parameters.Add("Id", id);
            query = query.Substring(0, query.Length - 1);
            query += " where Id=@Id;";
            await WithConnection(async conn =>
                await conn.ExecuteAsync(query, parameters));
        }
        /// <summary>
        ///     Executes the UpdateAsync method for updating related record
        /// </summary>
        /// <param name="table">Table Name</param>
        /// <param name="entity">Entity</param>
        public virtual async Task UpdateAsync(string table, TEntity entity)
        {
            var parameters = new DynamicParameters();
            var query = $"Update {table} set ";
            foreach (var val in RepositoryExtensions<TEntity>.GetProperties(entity))
            {
                parameters.Add(val.Key, val.Value);
                query += $"{val.Key} = @{val.Key},";
            }
            query = query.Substring(0, query.Length - 1);
            query += ";";
            await WithConnection(async conn =>
                await conn.ExecuteAsync(query, parameters));
        }
    }
}