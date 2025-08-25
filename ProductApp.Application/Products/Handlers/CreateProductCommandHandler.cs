using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApp.Domain.Entities;
using ProductApp.Application.Products.Commands;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Messaging;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProductApp.Application.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly AppDbContext _context;
        private readonly ProductEventPublisher _eventPublisher;

        public CreateProductCommandHandler(AppDbContext context)
        {
            _context = context;
            _eventPublisher = new ProductEventPublisher(); // Basit örnek için doğrudan new'liyoruz
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Stock = request.Stock
                };
                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                // Event fırlat
                _eventPublisher.Publish(new
                {
                    EventType = "ProductCreated",
                    ProductId = product.Id,
                    product.Name,
                    product.Price,
                    product.Stock
                });

                var cache = new RedisCacheService("localhost:6379");
                await cache.RemoveAsync("products:all");

                return product.Id;
            }
            catch (Exception ex)
            {
                var httpClient = new HttpClient();
                var logEntry = new
                {
                    Service = "ProductService",
                    Message = "Ürün eklenirken hata oluştu",
                    Details = ex.ToString(),
                    Level = "Error"
                };
                await httpClient.PostAsJsonAsync("http://localhost:5000/api/log", logEntry); // LogService API portunu kendi ortamına göre ayarla
                throw;
            }
        }
    }
}