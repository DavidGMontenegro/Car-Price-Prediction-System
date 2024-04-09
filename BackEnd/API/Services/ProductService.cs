using FinalAPI.Data;
using FinalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext context;

        public ProductService(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            var dbHumans = await context.Products.ToListAsync();

            return dbHumans;
        }

        public async Task<Product> GetSingleProduct(int id)
        {
            return await context.Products.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<List<Product>> Post(Product newHuman)
        {
            var dbHumans = context.Products.Add(newHuman);
            await context.SaveChangesAsync();

            return await context.Products.ToListAsync();
        }
        public async Task<Product> Put(int id, Product updatedHuman)
        {
            var existingProduct = await GetSingleProduct(id);

            existingProduct.Name = updatedHuman.Name;
            existingProduct.Description = updatedHuman.Description;
            existingProduct.Price = updatedHuman.Price;
            existingProduct.AvailableItems = updatedHuman.AvailableItems;

            await context.SaveChangesAsync();
            existingProduct = await GetSingleProduct(id);

            return existingProduct;
        }
        public async Task<List<Product>> Delete(int id)
        {
            var existingHuman = await GetSingleProduct(id);
            context.Products.Remove(existingHuman);
            await context.SaveChangesAsync();

            return await context.Products.ToListAsync();
        }
    }
}
