using ProductApp.Application.DTOs;

namespace ProductApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task UpdateAsync(ProductDTO productDto);
        Task DeleteAsync(int id);
        Task AddAsync(ProductDTO productDto); // ← Bunu ekle
        // Diğer servis metotlarını buraya ekleyebilirsin
    }
}