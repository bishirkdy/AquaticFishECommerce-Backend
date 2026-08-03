using AquaticFishECommerce.Application.DTOs.Product;
using AquaticFishECommerce.Application.Interfaces.Repositories;
using AquaticFishECommerce.Domain.Entities;
using AquaticFishECommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace AquaticFishECommerce.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByIdWithImagesAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllWithImagesAsync()
        {
            return await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetSixProductAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .Take(6)
                .ToListAsync();
        }

        //Get product with category and without image
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(x => x.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsyncWithImg(ProductQueryDto query)
        {
            IQueryable<Product> products = _context.Products
                .AsNoTracking()
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Where(p => p.IsActive);

            // Search
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                string search = query.Search.Trim().ToLower();

                products = products.Where(p =>
                    p.Name.ToLower().Contains(search) ||
                    p.Description.ToLower().Contains(search) ||
                    p.Category.Name.ToLower().Contains(search));
            }

            // Category
            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                products = products.Where(p =>
                    p.Category.Name == query.Category);
            }

            // Minimum Price
            if (query.MinPrice.HasValue)
            {
                products = products.Where(p =>
                    p.Price >= query.MinPrice.Value);
            }

            // Maximum Price
            if (query.MaxPrice.HasValue)
            {
                products = products.Where(p =>
                    p.Price <= query.MaxPrice.Value);
            }

            // Sorting
            products = query.Sort?.ToLower() 
            switch
            {
                "price-asc" => products.OrderBy(p => p.Price),

                "price-desc" => products.OrderByDescending(p => p.Price),

                "latest" => products.OrderByDescending(p => p.CreatedAt),

                //"rating" => products.OrderByDescending(p =>
                //    p.Reviews.Average(r => r.Rating)),

                _ => products.OrderByDescending(p => p.CreatedAt)
            };

            // Pagination
            products = products
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize);

            return await products.ToListAsync();
        }
    }
}
