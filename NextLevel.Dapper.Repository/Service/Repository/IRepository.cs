using System.Collections.Generic;
using System.Threading.Tasks;

namespace NextLevel.Dapper.Repository.Service.Repository
{
    public interface IRepository<TEntity, in TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(string command);
        ValueTask<TEntity> GetByIdAsync(string command, TKey id);
        Task AddAsync(string command,TEntity entity);
        Task UpdateAsync(string command,TEntity entity, TKey id);
        Task RemoveAsync(string command,TKey id);
        Task<bool> IsInDbAsync(string command,TKey id);
    }
}