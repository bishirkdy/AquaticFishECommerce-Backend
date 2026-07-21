using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Application.Interfaces;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AquaticFishECommerce.Persistence.Repositories
{
    //This is created for all cread operation for every entity that is use GenericRepository
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // Finds an entity using its primary key.
        public async Task<T?> GetByIdAsyn(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        // Finds all entity
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // Add an entity to database
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Update an existing entity
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        // Delete an existing entity
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
