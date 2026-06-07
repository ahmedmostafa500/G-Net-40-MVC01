using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.DAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity :BaseEntity, new()
    {
        private readonly GymDbcontext dbcontext;

        public GenericRepository(GymDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public void Add(TEntity Item)
        {
            dbcontext.Set<TEntity>().Add(Item);
            
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, bool istracked = false, CancellationToken ct = default)
        {
            return await dbcontext.Set<TEntity>().AnyAsync(predicate,ct);
        }

        public async Task<int> CompleteAsync()
        {
         return await dbcontext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var item=dbcontext.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            if (item != null)
                dbcontext.Set<TEntity>().Remove(item);
        }

        public async Task<TEntity?> FiristOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool istracked = false, CancellationToken ct = default)
        {
            var items = istracked ? dbcontext.Set<TEntity>() : dbcontext.Set<TEntity>().AsNoTracking();
            return await items.FirstOrDefaultAsync(predicate,ct);
        }

        public async Task<IEnumerable<TEntity>> GetAll(bool isTracked, CancellationToken ct = default)
        {
            var Items = isTracked ? dbcontext.Set<TEntity>():dbcontext.Set<TEntity>().AsNoTracking();
            return await Items. ToListAsync();
        }

        public async Task<TEntity?> GetById(int id, CancellationToken ct = default)
        {
           var item= await dbcontext.Set<TEntity>().FirstOrDefaultAsync(p=>p.Id==id);
            return item;
        }

        public void Update(TEntity Item)
        {
          dbcontext.Set<TEntity>().Update(Item);
        }
    }
}
