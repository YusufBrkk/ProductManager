using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newproject.DTOs;
using Newproject.Models;
using Newproject.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace Newproject.Services
{
    // Ürünlerle ilgili iş mantığını yöneten servis sınıfı
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IDatabase _cache;

        public ProductService(IProductRepository productRepository, IConnectionMultiplexer redis)
        {
            _productRepository = productRepository;
            _cache = redis.GetDatabase();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var cacheKey = "products";
            var cachedProducts = await _cache.StringGetAsync(cacheKey);
            if (cachedProducts.HasValue)
            {
                return JsonSerializer.Deserialize<IEnumerable<ProductDTO>>(cachedProducts)!;
            }

            var products = await _productRepository.GetAllAsync();
            var productDtos = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();

            await _cache.StringSetAsync(cacheKey, JsonSerializer.Serialize(productDtos));
            return productDtos;
        }

        public async Task<ProductDTO?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return null;
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }

        public async Task AddAsync(ProductDTO productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price
            };
            await _productRepository.AddAsync(product);
            await _cache.KeyDeleteAsync("products"); // Cache invalidation
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
            await _cache.KeyDeleteAsync("products"); // Cache invalidation
        }

        public async Task UpdateAsync(ProductDTO productDto)
        {
            var product = await _productRepository.GetByIdAsync(productDto.Id);
            if (product != null)
            {
                product.Name = productDto.Name;
                product.Price = productDto.Price;
                await _productRepository.UpdateAsync(product);
                await _cache.KeyDeleteAsync("products"); // Cache invalidation
            }
        }
    }
}