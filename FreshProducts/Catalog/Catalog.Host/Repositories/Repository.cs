using Catalog.Host.Repositories.Abstractions;
using Catalog.Host.Data;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbset;
        private T _result;

        public Repository(IDbContextWrapper<ApplicationDbContext> context)
        {
            _dbContext = context.DbContext;
            _dbset = context.DbContext.Set<T>();
        }

        public async Task<int?> AddAsync(T entity)
        {
            var item = await _dbset
                .AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return item.Entity.Id;
        }

        public async Task<int?> UpdateAsync(int id, T entity)
        {
            _result = await _dbset
                .FindAsync(id);

            if (_result != null)
            {
                _dbContext.Entry(_result).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
                return _result.Id;
            }

            return null;
        }

        public async Task<string?> DeleteAsync(int id)
        {
            _result = await _dbset
                .FindAsync(id);

            if (_result != null)
            {
                _dbContext.Set<T>().Remove(_result);
                await _dbContext.SaveChangesAsync();
                return $"Object with id: {id} was saccussfully removed";
            }

            return null;
        }
    }
}
