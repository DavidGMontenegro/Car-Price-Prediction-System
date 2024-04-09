using FinalAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalAPI.Services
{
    public interface IProductService
    {
        // GET
        Task<List<Product>> GetProducts();

        Task<Product> GetSingleProduct(int id);

        // POST
        Task<List<Product>> Post(Product newHuman);

        // PUT
        Task<Product> Put(int id, Product updatedHuman);

        // DELETE
        Task<List<Product>> Delete(int id);
    }
}
