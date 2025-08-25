using MediatR;
using ProductApp.Domain.Entities;
using ProductApp.Application.Products.Commands;
using ProductApp.Infrastructure.Persistence;
using ProductApp.Infrastructure.Messaging;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProductApp.Application.Products.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly AppDbContext _context;
        private readonly ProductEventPublisher _eventPublisher;

        public UpdateProductCommandHandler(AppDbContext context)
        {
            _context = context;
            _eventPublisher = new ProductEventPublisher();
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
                if (product == null) return false;

                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Stock = request.Stock;

                await _context.SaveChangesAsync(cancellationToken);

                // Event fırlat
                _eventPublisher.Publish(new
                {
                    EventType = "ProductUpdated",
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
                    Message = "Ürün güncellenirken hata oluştu",
                    Details = ex.ToString(),
                    Level = "Error"
                };
                await httpClient.PostAsJsonAsync("http://localhost:5000/api/log", logEntry);
                throw;
            }
        }
    }
}