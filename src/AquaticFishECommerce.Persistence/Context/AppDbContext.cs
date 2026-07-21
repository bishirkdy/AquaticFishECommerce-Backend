using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AquaticFishECommerce.Domain.Entities;

namespace AquaticFishECommerce.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        //OnModelCreating() is a method that EF Core automatically calls once when the application starts.
        //ModelBuilder is an object that helps EF Core build the database model.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Find every class in this project that implements IEntityTypeConfiguration<T> and apply it automatically
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}   
