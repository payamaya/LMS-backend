using LMS.Contracts;
using LMS.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task CreateAsync(T entity) => await DbSet.AddAsync(entity);

        public void Delete(T entity) => DbSet.Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity) => DbSet.Update(entity);
    }
}
