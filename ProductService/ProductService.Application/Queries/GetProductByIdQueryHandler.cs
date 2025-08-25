using ProductService.Core.Entities;
using ProductService.Core.Interfaces;

namespace ProductService.Application.Queries
{
    public class GetProductByIdQueryHandler
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> Handle(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}