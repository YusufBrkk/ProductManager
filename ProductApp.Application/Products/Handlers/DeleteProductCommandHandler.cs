using System.Net.Http;
using System.Net.Http.Json;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Products.Commands;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Messaging;
// using ProductApp.Infrastructure.Services;

namespace ProductApp.Application.Products.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly ProductEventPublisher _eventPublisher;

        public DeleteProductCommandHandler(AppDbContext context)
        {
            _context = context;
            _eventPublisher = new ProductEventPublisher();
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
                if (product == null) return false;

                _context.Products.Remove(product);
                await _context.SaveChangesAsync(cancellationToken);

                // Event fırlat
                _eventPublisher.Publish(new
                {
                    EventType = "ProductDeleted",
                    ProductId = product.Id,
                    product.Name,
                    product.Price,
                    product.Stock
                });

                var cache = new RedisCacheService("localhost:6379");
                await cache.RemoveAsync("products:all");

                return true;
            }
            catch (Exception ex)
            {
                var httpClient = new HttpClient();
                var logEntry = new
                {
                    Service = "ProductService",
                    Message = "Ürün silinirken hata oluştu",
                    Details = ex.ToString(),
                    Level = "Error"
                };
                await httpClient.PostAsJsonAsync("http://localhost:5000/api/log", logEntry);
                throw;
            }
        }
    }
}