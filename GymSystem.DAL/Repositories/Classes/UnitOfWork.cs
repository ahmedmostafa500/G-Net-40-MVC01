using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;

namespace GymSystem.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext dbContext;
        private readonly Dictionary<string, object> _Repos = [];

        public UnitOfWork(GymDbcontext dbcontext)
        {
            this.dbContext = dbcontext;
            
        }


        public async Task<int> CompleteAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var TypeName = typeof(TEntity).Name;//String Key

            if (_Repos.TryGetValue(TypeName, out object OldRepository))
                return (IGenericRepository<TEntity>)OldRepository;


            var NewRepository = new GenericRepository<TEntity>(dbContext);

            _Repos[TypeName] = NewRepository;

            return NewRepository;
        }
    }
}
