using MediatR;
using ProductApp.Domain.Entities;
using ProductApp.Application.Products.Queries;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProductApp.Application.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly AppDbContext _context;
        private readonly RedisCacheService _cache;

        public GetAllProductsQueryHandler(AppDbContext context)
        {
            _context = context;
            _cache = new RedisCacheService("localhost:6379"); // Bağlantı adresini ihtiyaca göre değiştir
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = "products:all";
                var cached = await _cache.GetAsync<List<Product>>(cacheKey);
                if (cached != null)
                    return cached;

                var products = await _context.Products.ToListAsync(cancellationToken);
                await _cache.SetAsync(cacheKey, products, TimeSpan.FromMinutes(5));
                return products;
            }
            catch (Exception ex)
            {
                var httpClient = new HttpClient();
                var logEntry = new
                {
                    Service = "ProductService",
                    Message = "Ürünler listelenirken hata oluştu",
                    Details = ex.ToString(),
                    Level = "Error"
                };
                await httpClient.PostAsJsonAsync("http://localhost:5000/api/log", logEntry);
                throw;
            }
        }
    }
}