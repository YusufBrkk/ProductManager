using System.Collections.Generic;
using System.Threading.Tasks;
using Newproject.Models;

namespace Newproject.Repositories
{
    // Ürünler için temel veri erişim işlemlerini tanımlar
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id); 
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);
    }
}