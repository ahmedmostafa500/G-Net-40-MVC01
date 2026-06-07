using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;

namespace GymSystem.DAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity:BaseEntity,new()
    {
        Task<IEnumerable<TEntity>> GetAll(bool isTracked, CancellationToken ct = default);
        Task<TEntity?> GetById(int id, CancellationToken ct = default);

        void Add(TEntity Item);
        void Update(TEntity Item);
        void Delete(int id);

        Task<int> CompleteAsync();
        Task<TEntity?> FiristOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool istracked = false,CancellationToken ct=default);

        Task<bool> AnyAsync (Expression<Func<TEntity, bool>> predicate, bool istracked = false, CancellationToken ct = default);
    }
}
