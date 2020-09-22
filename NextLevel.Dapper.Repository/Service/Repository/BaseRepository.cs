using System.Data.SqlClient;
using System.Threading.Tasks;
using System;
using System.Data;
using NextLevel.Dapper.Repository;


namespace NextLevel.Dapper.Repository.Service.Repository
{
    public abstract class BaseRepository
    {
        /// <summary>
        /// Use for buffered queries that return a type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getData"></param>
        /// <returns></returns>
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            { 
                using var connection = new SqlConnection(Connection.Value);
                await connection.OpenAsync();
                return await getData(connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        /// <summary>
        /// Use for buffered queries that do not return a type
        /// </summary>
        /// <param name="getData"></param>
        /// <returns></returns>
        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            try
            {
                using var connection = new SqlConnection(Connection.Value);
                await connection.OpenAsync();
                await getData(connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }


        /// <summary>
        /// Use for non-buffered queries that return a type
        /// </summary>
        /// <typeparam name="TRead"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="getData"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        protected async Task<TResult> WithConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData, Func<TRead, Task<TResult>> process)
        {
            try
            {
                using var connection = new SqlConnection(Connection.Value);
                await connection.OpenAsync();
                var data = await getData(connection);
                return await process(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (SqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
    }
}
