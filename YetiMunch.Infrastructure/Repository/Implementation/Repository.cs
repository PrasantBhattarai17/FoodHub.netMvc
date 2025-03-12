using Microsoft.EntityFrameworkCore;
using YetiMunch.Data;
using YetiMunch.Repository.IRepository;

namespace YetiMunch.Repository.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FoodContext _db;
        private readonly DbSet<T> _dbSet;

        public Repository(FoodContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
             return await _dbSet.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task Update(T entity)
        {
             _dbSet.Update(entity);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }
    
    }
}
