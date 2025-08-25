using ProductApp.Application.DTOs;
using ProductApp.Application.Interfaces;

namespace ProductApp.Application.Services
{
    public class ProductService : IProductService
    {
        // Geçici olarak ürünleri bellekte tutan bir liste
        private readonly List<ProductDTO> _products = new();

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            return await Task.FromResult(_products);
        }

        public async Task AddAsync(ProductDTO productDto)
        {
            _products.Add(productDto);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(ProductDTO productDto)
        {
            var existing = _products.FirstOrDefault(p => p.Id == productDto.Id);
            if (existing != null)
            {
                existing.Name = productDto.Name;
                // Diğer alanları da burada güncelleyebilirsin
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);

            await Task.CompletedTask;
        }
    }
}