using ProductService.Core.Entities;
using ProductService.Core.Interfaces;

namespace ProductService.Application.Commands
{
    public class UpdateProductCommandHandler
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
    }
}