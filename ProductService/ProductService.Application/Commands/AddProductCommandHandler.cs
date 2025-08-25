using ProductService.Core.Entities;
using ProductService.Core.Interfaces;

namespace ProductService.Application.Commands
{
    public class AddProductCommandHandler
    {
        private readonly IProductRepository _productRepository;

        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(Product product)
        {
            await _productRepository.AddAsync(product);
        }
    }
}