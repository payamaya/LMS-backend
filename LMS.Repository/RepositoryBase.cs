using LMS.Contracts;
using LMS.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LMS.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext Context;
        protected DbSet<T> DbSet { get; }

        public RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public IQueryable<T> FindAll(bool trackChanges) =>
                 !trackChanges ? DbSet.AsNoTracking()
                               : DbSet;

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
           !trackChanges ? DbSet.Where(expression).AsNoTracking()
                         : DbSet.Where(expression);


        public async Task CreateAsync(T entity) => await DbSet.AddAsync(entity);

        public void Update(T entity) => DbSet.Update(entity);

        public void Delete(T entity) => DbSet.Remove(entity);
    }
}
