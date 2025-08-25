using System.Collections.Generic;
using System.Threading.Tasks;
using Newproject.DTOs;

namespace Newproject.Services
{
    // Ürünlerle ilgili iş mantığı işlemlerini tanımlar
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(int id); 
        Task AddAsync(ProductDTO productDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(ProductDTO productDto);
    }
}